// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x1976048, 0x197622C, 0x197639C, 0x19763AC, 0x19762C0, 0x1975A8C, 0x19763B4,
//       0x1976544, 0x197657C, 0x1976748, 0x19768BC, 0x1976A48, 0x1975A84
// Ghidra dir: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace SONETWORK
{
    // Source: Il2CppDumper-decompiled  TypeDefIndex: 943
    public sealed class TcpSocket
    {
        private Socket m_socket;                 // 0x10
        private IAsyncResult m_asyncResult;      // 0x18
        private bool m_connected;                // 0x20

        // RVA: 0x1976048  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/ipAddreeConver.c
        public static IPEndPoint ipAddreeConver(string host, int port)
        {
            IPAddress addr = IPAddress.Parse(host);
            IPEndPoint ep = new IPEndPoint(addr, port);
            if (ep == null)
            {
                IPHostEntry entry = Dns.GetHostEntry(host);
                if (entry == null)
                {
                    throw new NullReferenceException();
                }
                IPAddress[] list = entry.AddressList;
                if (list == null)
                {
                    throw new NullReferenceException();
                }
                if (list.Length == 0)
                {
                    throw new IndexOutOfRangeException();
                }
                IPAddress first = list[0];
                ep = new IPEndPoint(first, port);
            }
            return ep;
        }

        // RVA: 0x197622C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/Finalize.c
        ~TcpSocket()
        {
            try
            {
                this.close();
            }
            finally
            {
                // base.Finalize() emitted by compiler
            }
        }

        // RVA: 0x197639C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/good.c
        public bool good()
        {
            return this.m_socket != null;
        }

        // RVA: 0x19763AC  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/ready.c
        public bool ready()
        {
            return this.m_connected;
        }

        // RVA: 0x19762C0  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/close.c
        public void close()
        {
            if (this.m_socket != null)
            {
                this.m_socket.Close();
                if (this.m_asyncResult != null)
                {
                    if (this.m_socket == null)
                    {
                        throw new NullReferenceException();
                    }
                    this.m_socket.EndConnect(this.m_asyncResult);
                }
            }
            this.m_socket = null;
            this.m_asyncResult = null;
            this.m_connected = false;
        }

        // RVA: 0x1975A8C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/connect.c
        public bool connect(string host, int port)
        {
            if (this.m_socket != null)
            {
                return false;
            }
            IPEndPoint ep = TcpSocket.ipAddreeConver(host, port);
            if (ep == null)
            {
                return false;
            }
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.m_socket = s;
            this.m_connected = false;
            if (this.m_socket == null)
            {
                throw new NullReferenceException();
            }
            IAsyncResult ar = this.m_socket.BeginConnect(ep, null, null);
            this.m_asyncResult = ar;
            return true;
        }

        // RVA: 0x19763B4  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/listen.c
        public bool listen(string host, int port)
        {
            if (this.m_socket != null)
            {
                return false;
            }
            IPEndPoint ep = TcpSocket.ipAddreeConver(host, port);
            if (ep == null)
            {
                return false;
            }
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.m_socket = s;
            if (this.m_socket == null)
            {
                throw new NullReferenceException();
            }
            this.m_socket.Bind(ep);
            if (this.m_socket == null)
            {
                throw new NullReferenceException();
            }
            this.m_socket.Listen(5);
            if (this.m_socket == null)
            {
                throw new NullReferenceException();
            }
            this.m_socket.NoDelay = true;
            if (this.m_socket == null)
            {
                throw new NullReferenceException();
            }
            this.m_socket.Blocking = false;
            return true;
        }

        // RVA: 0x1976544  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/socket.c
        public bool socket(Socket socket)
        {
            Socket existing = this.m_socket;
            if (existing == null)
            {
                this.m_socket = socket;
                this.m_connected = true;
            }
            return existing == null;
        }

        // RVA: 0x197657C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/connectReady.c
        public int connectReady()
        {
            if (this.m_socket == null)
            {
                return -1;
            }
            if (this.m_asyncResult != null)
            {
                if (!this.m_asyncResult.IsCompleted)
                {
                    return 0;
                }
                if (this.m_socket == null)
                {
                    throw new NullReferenceException();
                }
                this.m_socket.EndConnect(this.m_asyncResult);
                if (this.m_socket == null)
                {
                    throw new NullReferenceException();
                }
                EndPoint remote = this.m_socket.RemoteEndPoint;
                this.m_connected = remote != null;
                if (this.m_socket == null)
                {
                    throw new NullReferenceException();
                }
                this.m_socket.NoDelay = true;
                if (this.m_socket == null)
                {
                    throw new NullReferenceException();
                }
                this.m_socket.Blocking = false;
                this.m_asyncResult = null;
            }
            int result = 1;
            if (!this.m_connected)
            {
                result = -1;
            }
            return result;
        }

        // RVA: 0x1976748  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/accept.c
        public TcpSocket accept(out int errcode)
        {
            errcode = 0;
            if (this.m_socket == null)
            {
                errcode = -1;
                return null;
            }
            Socket accepted = this.m_socket.Accept();
            if (accepted != null)
            {
                accepted.NoDelay = true;
                accepted.Blocking = false;
                TcpSocket inst = new TcpSocket();
                if (inst == null)
                {
                    throw new NullReferenceException();
                }
                if (inst.m_socket == null)
                {
                    inst.m_socket = accepted;
                    inst.m_connected = true;
                    return inst;
                }
                inst.close();
            }
            return null;
        }

        // RVA: 0x19768BC  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/read.c
        public int read(byte[] data, int offset, int size)
        {
            if (this.m_socket == null)
            {
                return -1;
            }
            if (0 < size && this.m_socket.Poll(0, SelectMode.SelectRead))
            {
                if (this.m_socket == null)
                {
                    throw new NullReferenceException();
                }
                int avail = this.m_socket.Available;
                if (avail != 0)
                {
                    if (this.m_socket == null)
                    {
                        throw new NullReferenceException();
                    }
                    return this.m_socket.Receive(data, offset, size, SocketFlags.None);
                }
                return -2;
            }
            return 0;
        }

        // RVA: 0x1976A48  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/write.c
        public int write(byte[] data, int offset, int size)
        {
            if (this.m_socket == null)
            {
                return -1;
            }
            if (size < 1)
            {
                return 0;
            }
            return this.m_socket.Send(data, offset, size, SocketFlags.None);
        }

        // RVA: 0x1975A84  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.TcpSocket/.ctor.c
        public TcpSocket()
        {
        }

    }
}
