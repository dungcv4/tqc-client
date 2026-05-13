using System.Collections.Generic;
using UnityEngine;

namespace SONETWORK
{
	// Source: dump.cs TypeDefIndex 942
	// Source: work/06_ghidra/decompiled/SONETWORK.TcpNetWork/ (9 methods)
	internal sealed class TcpNetWork
	{
		// Field offsets per dump.cs (TypeDefIndex 942):
		private static TcpNetWork s_instance;            // static 0x0
		private static double s_lastTime;                // static 0x8
		private static uint s_tickMask;                  // static 0x10
		private int m_proxiesPerTick;                    // instance 0x10
		private Queue<ConnectProxy> m_connections;       // instance 0x18
		private uint m_lastProxyID;                      // instance 0x20
		private Queue<uint> m_freeProxyIDs;              // instance 0x28

		// Source: Ghidra work/06_ghidra/decompiled/SONETWORK.TcpNetWork/.ctor.c RVA 0x01975600
		// 1-1:
		//   *(int*)(this + 0x10) = 200            // m_proxiesPerTick = 200 (NOT 1 — prior port bug)
		//   *(Queue<obj>*)(this + 0x18) = new Queue<ConnectProxy>()
		//   *(Queue<uint>*)(this + 0x28) = new Queue<uint>()
		//   base.ctor()
		private TcpNetWork()
		{
			m_proxiesPerTick = 200;
			m_connections = new Queue<ConnectProxy>();
			m_freeProxyIDs = new Queue<uint>();
		}

		// Source: Ghidra work/06_ghidra/decompiled/SONETWORK.TcpNetWork/Finalize.c RVA 0x019756e4
		// 1-1: closeAll(); base.Finalize().
		~TcpNetWork()
		{
			try { closeAll(); } catch { /* finalizer must not throw */ }
		}

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

		// Source: Ghidra work/06_ghidra/decompiled/SONETWORK.TcpNetWork/run.c RVA 0x01975be8
		// 1-1 port (previous version skipped the good()/onClosed dispatch — fatal bug:
		//   proxy.close() set m_waitClose=true and TCP socket closed; next TcpNetWork tick
		//   should detect good()==false and call onClosed() → vOnClosed cb → BaseProcLua.
		//   OnServerDisconnect → Lua SGCNetReceiverLobby.OnServerDisconnect → connectStep=None.
		//   Without that, Lua's connectStep stayed SecretLogin_OK forever; subsequent
		//   LoginCheckStart() calls hit `elseif connectStep==SecretLogin_OK then ForceCloseConnect; return`
		//   and login flow never progressed to CheckLoginStatus → no CHECKACC packet sent).
		//
		// Algorithm (matches Ghidra):
		//   lastTime += dTime; tickMask++
		//   per = min(proxiesPerTick, m_connections.Count)
		//   loop per times:
		//     proxy = m_connections.Dequeue()
		//     if (!proxy.good()) {
		//         proxy.onClosed(0)            // vOnClosed dispatch
		//         s_instance.freeProxyID(proxy.proxyID)
		//         // DO NOT re-enqueue — proxy is dead
		//     } else {
		//         proxy.run(s_lastTime)        // cumulative time, NOT dTime (Ghidra line 86)
		//         if (proxy.lastActiveTime() > 0 && proxy.lastActiveTime() < s_lastTime)
		//             proxy.close(-14)         // -14 = inactive timeout
		//         m_connections.Enqueue(proxy) // re-enqueue alive proxy
		//     }
		public static void run(float dTime)
		{
			if (s_instance == null) return;
			s_lastTime = s_lastTime + (double)dTime;
			s_tickMask = s_tickMask + 1u;
			if (s_instance.m_connections == null) return;

			int iVar8 = s_instance.m_proxiesPerTick;
			int queueCount = s_instance.m_connections.Count;
			if (queueCount > 0 && queueCount < iVar8) iVar8 = queueCount;
			if (iVar8 <= 0) return;

			while (iVar8 > 0)
			{
				iVar8--;
				if (s_instance.m_connections.Count == 0) return;
				var p = s_instance.m_connections.Dequeue();
				if (p == null) continue;

				if (!p.good())
				{
					// Socket closed/errored → dispatch close callback + free proxy slot.
					// onClosed → IConnectProxyCB.vOnClosed → BaseConnect.OnServerDisconnect → Lua.
					try { p.onClosed(); }
					catch (System.Exception e) { Debug.LogWarning("[TcpNetWork.run] proxy.onClosed threw: " + e.Message); }
					s_instance.freeProxyID(p.proxyID());
					// proxy NOT re-enqueued — dead.
				}
				else
				{
					// Ghidra run.c line 86: passes *(*(TcpNetWork class + 0xb8) + 8) = s_lastTime
					// (per dump.cs field offsets: s_instance@0, s_lastTime@8). The previous port
					// passed (double)dTime here, which broke ConnectProxy's wait-close timer:
					// m_waitCloseTime = runTime + 1.0 stayed ~1.016s while runTime was ~0.016s
					// per frame → close-down branch unreachable → socket never freed → onClosed
					// never dispatched → Lua's connectStep stuck at SecretLogin_OK forever.
					try { p.run(s_lastTime); }
					catch (System.Exception e) { Debug.LogWarning("[TcpNetWork.run] proxy.run threw: " + e.Message); }
					// Inactive-timeout check per Ghidra (lines 87-98):
					// if (lastActiveTime > 0 && lastActiveTime < s_lastTime) close(-14)
					double lat = p.lastActiveTime();
					if (lat > 0.0 && lat < s_lastTime)
					{
						p.close(unchecked((int)0xfffffff2));  // -14
					}
					s_instance.m_connections.Enqueue(p);
				}
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
