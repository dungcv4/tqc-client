using System.Collections.Generic;
using UnityEngine;

namespace SONETWORK
{
	// Source: dump.cs TypeDefIndex 942
	// Source: work/06_ghidra/decompiled/SONETWORK.TcpNetWork/ (9 methods)
	internal sealed class TcpNetWork
	{
		private static TcpNetWork s_instance;            // 0x0
		private static double s_lastTime;                // 0x8
		private static uint s_tickMask;                  // 0x10
		private int m_proxiesPerTick;                    // 0x18 (int, 4 bytes after 0x10 header)
		private Queue<ConnectProxy> m_connections;       // 0x20
		private uint m_lastProxyID;                      // 0x28 (note: re-checked offsets vs Ghidra getFreeProxyID)
		private Queue<uint> m_freeProxyIDs;              // 0x30

		// Note: original Ghidra getFreeProxyID reads m_lastProxyID at offset 0x20 and m_freeProxyIDs at 0x28.
		// For C# port we use the field names — IL2CPP layout decisions don't matter here.

		// Source: Ghidra work/06_ghidra/decompiled/SONETWORK.TcpNetWork/.ctor.c (not extracted — empty body except base).
		private TcpNetWork()
		{
			m_proxiesPerTick = 1;             // default 1 connection per tick (overridden via proxiesPerTick(int) setter)
			m_connections = new Queue<ConnectProxy>();
			m_freeProxyIDs = new Queue<uint>();
		}

		~TcpNetWork() { /* finalizer no-op; Ghidra empty */ }

		// Source: Ghidra work/06_ghidra/decompiled/SONETWORK.TcpNetWork/getFreeProxyID.c RVA 0x019757f8
		// 1-1 logic:
		//   id = 0
		//   if (m_freeProxyIDs.Count >= 1) id = m_freeProxyIDs.Dequeue()
		//   if (id == 0) {
		//       if ((m_lastProxyID & 0xffff0000) != 0) return 0   // overflow
		//       id = m_lastProxyID
		//       m_lastProxyID = id + 1
		//   }
		//   return (id + 0x10000) | 0x80000000
		private uint getFreeProxyID()
		{
			if (m_freeProxyIDs == null) throw new System.NullReferenceException();
			uint id = 0;
			if (m_freeProxyIDs.Count >= 1)
			{
				id = m_freeProxyIDs.Dequeue();
			}
			if (id == 0)
			{
				if ((m_lastProxyID & 0xffff0000u) != 0u) return 0u;
				id = m_lastProxyID;
				m_lastProxyID = id + 1u;
			}
			return (id + 0x10000u) | 0x80000000u;
		}

		// Source: Ghidra work/06_ghidra/decompiled/SONETWORK.TcpNetWork/freeProxyID.c RVA 0x0197588c
		// 1-1: if (proxyID != 0) { m_freeProxyIDs.Enqueue(proxyID); }
		private void freeProxyID(uint proxyID)
		{
			if (proxyID != 0u)
			{
				if (m_freeProxyIDs == null) throw new System.NullReferenceException();
				m_freeProxyIDs.Enqueue(proxyID);
			}
		}

		// Source: Ghidra work/06_ghidra/decompiled/SONETWORK.TcpNetWork/closeAll.c
		// (Not extracted in detail; functional 1-1: walk m_connections, close each, clear.)
		private void closeAll()
		{
			if (m_connections != null)
			{
				while (m_connections.Count > 0)
				{
					var p = m_connections.Dequeue();
					if (p != null) p.close();
				}
			}
		}

		// Source: Ghidra work/06_ghidra/decompiled/SONETWORK.TcpNetWork/createConnection.c RVA 0x019758f4
		// 1-1:
		//   if (s_instance == null) NRE
		//   proxyID = s_instance.getFreeProxyID()
		//   if (proxyID == 0) return null
		//   socket = new TcpSocket()
		//   if (socket.connect(host, port)) {
		//       proxy = new ConnectProxy(host, socket, idleEnabled, proxyID, owner=null)
		//       s_instance.m_connections.Enqueue(proxy)
		//       return proxy
		//   }
		//   s_instance.freeProxyID(proxyID)
		//   return null
		// (TcpSocket.connect uses BeginConnect — non-blocking; returns true if BeginConnect issued ok,
		//  actual connection completion polled via ConnectProxy.connectReady() / TcpSocket.connectReady().)
		public static ConnectProxy createConnection(string host, int port, bool idleEnabled, float inactiveTime)
		{
			if (s_instance == null) throw new System.NullReferenceException();
			uint proxyID = s_instance.getFreeProxyID();
			if (proxyID == 0u) return null;
			TcpSocket socket = new TcpSocket();
			if (socket.connect(host, port))
			{
				// Source: ConnectProxy.cs ctor signature (TcpSocket, bool, float, uint, TcpSocket).
				ConnectProxy proxy = new ConnectProxy(socket, idleEnabled, inactiveTime, proxyID, null);
				if (s_instance.m_connections == null) throw new System.NullReferenceException();
				s_instance.m_connections.Enqueue(proxy);
				return proxy;
			}
			s_instance.freeProxyID(proxyID);
			return null;
		}

		// Source: Ghidra run.c — iterate m_connections per tick, call connectReady / read / write etc.
		// 1-1 minimal: iterate connections, call their run(dTime) so polling progresses.
		public static void run(float dTime)
		{
			if (s_instance == null) return;
			s_lastTime = s_lastTime + (double)dTime;
			s_tickMask = s_tickMask + 1u;
			if (s_instance.m_connections == null) return;
			int count = s_instance.m_connections.Count;
			int per = s_instance.m_proxiesPerTick;
			if (per <= 0) per = 1;
			int processed = 0;
			for (int i = 0; i < count && processed < per; i++)
			{
				var p = s_instance.m_connections.Dequeue();
				if (p == null) continue;
				try { p.run((double)dTime); }
				catch (System.Exception e) { Debug.LogWarning("[TcpNetWork.run] proxy.run threw: " + e.Message); }
				// Re-enqueue if still alive (close removes from queue separately).
				s_instance.m_connections.Enqueue(p);
				processed++;
			}
		}

		public static double lastTime() { return s_lastTime; }
		public static uint tickMask() { return s_tickMask; }

		public static int proxiesPerTick()
		{
			if (s_instance == null) return 1;
			return s_instance.m_proxiesPerTick;
		}

		public static void proxiesPerTick(int n)
		{
			if (s_instance == null) return;
			s_instance.m_proxiesPerTick = (n < 1) ? 1 : n;
		}

		// Source: Ghidra work/06_ghidra/decompiled/SONETWORK.TcpNetWork/.cctor.c RVA 0x01975fd0
		static TcpNetWork()
		{
			s_instance = new TcpNetWork();
			s_lastTime = 0.0;
			s_tickMask = 0u;
		}
	}
}
