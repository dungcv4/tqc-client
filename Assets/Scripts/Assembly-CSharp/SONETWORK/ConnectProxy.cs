// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x1972EB4, 0x1972EBC, 0x1972F04, 0x1972F20, 0x1972F3C, 0x1972F44, 0x1972F4C,
//       0x1972F54, 0x1972F5C, 0x1972F64, 0x1973034, 0x197303C, 0x1973044, 0x197304C,
//       0x1973054, 0x197305C, 0x1973060, 0x19731D0, 0x1973388, 0x19735A4, 0x1973644,
//       0x197365C, 0x197370C, 0x19737DC, 0x1973894
// Ghidra dir: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace SONETWORK
{
    // Source: Il2CppDumper-stub  TypeDefIndex: 931
    public sealed class ConnectProxy
    {
        // Source: Il2CppDumper-stub  TypeDefIndex: 930 (nested in ConnectProxy)
        public interface IConnectProxyCB
        {
            bool vOnConnected(ConnectProxy proxy);
            int vOnStream(ConnectProxy proxy, ref proto_COMM header, byte[] data);
            void vOnClosed(ConnectProxy proxy, int nReason);
        }

        private const float LOGIN_TIME_OUT = 5f;
        private const float CONNECT_TIME_OUT = 10f;
        private static uint s_lastTickMask;          // static 0x0
        private static uint s_tickCloseCounter;      // static 0x4
        private TcpSocket m_owner;                   // 0x10
        private TcpSocket m_socket;                  // 0x18
        private int m_errcode;                       // 0x20
        private double m_waitSendDeadTime;           // 0x28
        private bool m_idleEnabled;                  // 0x30
        private float m_inactiveTime;                // 0x34
        private double m_lastActiveTime;             // 0x38
        private bool m_waitConnect;                  // 0x40
        private bool m_waitClose;                    // 0x41
        private double m_waitCloseTime;              // 0x48
        private ConnectProxy.IConnectProxyCB m_callback; // 0x50
        private object m_selfObject;                 // 0x58
        private IPacketReader m_reader;              // 0x60
        private IPacketWriter m_writer;              // 0x68
        private uint m_proxyID;                      // 0x70
        private uint m_typeID;                       // 0x74
        private byte[] m_temp;                       // 0x78

        // RVA: 0x1972EB4  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/close.c
        public void close()
        {
            // Ghidra: close(0) — same body as close(int) when nReason==0 falls through
            // and skips the `if (param_2 != 0)` branch.  Net effect: set m_waitClose,
            // reset m_waitCloseTime, but DO NOT touch m_errcode and DO NOT close m_socket.
            if (!this.m_waitClose)
            {
                this.m_waitClose = true;
                this.m_waitCloseTime = -1.0; // 0xbff0000000000000
            }
        }

        // RVA: 0x1972EBC  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/close.c
        public void close(int nReason)
        {
            if (!this.m_waitClose)
            {
                this.m_waitClose = true;
                this.m_waitCloseTime = -1.0; // 0xbff0000000000000
                if (nReason != 0)
                {
                    this.m_errcode = nReason;
                    if (this.m_socket != null)
                    {
                        this.m_socket.close();
                        return;
                    }
                    // FUN_015cb8fc — il2cpp null-deref trap
                    throw new NullReferenceException();
                }
            }
        }

        // RVA: 0x1972F04  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/good.c
        public bool good()
        {
            if (this.m_socket != null)
            {
                return this.m_socket.good();
            }
            // FUN_015cb8fc — il2cpp null-deref trap
            throw new NullReferenceException();
        }

        // RVA: 0x1972F20  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/ready.c
        public bool ready()
        {
            if (this.m_socket != null)
            {
                // Ghidra: returns *(socket + 0x20), which is TcpSocket.m_connected.
                // We expose this via TcpSocket.ready() since the field is private.
                return this.m_socket.ready();
            }
            // FUN_015cb8fc — il2cpp null-deref trap
            throw new NullReferenceException();
        }

        // RVA: 0x1972F3C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/errcode.c
        public int errcode()
        {
            return this.m_errcode;
        }

        // RVA: 0x1972F44  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/callback.c
        public void callback(ConnectProxy.IConnectProxyCB callback)
        {
            this.m_callback = callback;
        }

        // RVA: 0x1972F4C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/callback.c
        public ConnectProxy.IConnectProxyCB callback()
        {
            return this.m_callback;
        }

        // RVA: 0x1972F54  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/selfObject.c
        public void selfObject(object obj)
        {
            this.m_selfObject = obj;
        }

        // RVA: 0x1972F5C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/selfObject.c
        public object selfObject()
        {
            return this.m_selfObject;
        }

        // RVA: 0x1972F64  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/idleEnabled.c
        public void idleEnabled(bool b)
        {
            // TODO: confidence:medium — Ghidra references PTR_DAT_034625a0 (UnityEngine.Time
            //   static class) at +0xb8 +8 which is realtimeSinceStartup. When toggling
            //   idleEnabled true, lastActiveTime is bumped by realtimeSinceStartup +
            //   inactiveTime. Verify against observed behavior.
            byte newVal = (byte)(b ? 1 : 0);
            byte oldVal = (byte)(this.m_idleEnabled ? 1 : 0);
            if (oldVal != newVal)
            {
                this.m_idleEnabled = (newVal != 0);
                if (newVal != 0)
                {
                    double dVar3 = this.m_lastActiveTime;
                    this.m_lastActiveTime = dVar3 + (double)Time.realtimeSinceStartup + (double)this.m_inactiveTime;
                }
            }
        }

        // RVA: 0x1973034  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/inactiveTime.c
        public void inactiveTime(float t)
        {
            this.m_inactiveTime = t;
        }

        // RVA: 0x197303C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/inactiveTime.c
        public float inactiveTime()
        {
            return this.m_inactiveTime;
        }

        // RVA: 0x1973044  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/proxyID.c
        public uint proxyID()
        {
            return this.m_proxyID;
        }

        // RVA: 0x197304C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/typeID.c
        public uint typeID()
        {
            return this.m_typeID;
        }

        // RVA: 0x1973054  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/typeID.c
        public void typeID(uint id)
        {
            this.m_typeID = id;
        }

        // RVA: 0x197305C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/write.c
        public int write(ushort protoco, object obj)
        {
            // TODO: confidence:low — Ghidra .c shared with the (ushort,object,uint) overload;
            //   forwards with UID=0.
            return this.write(protoco, obj, 0u);
        }

        // RVA: 0x1973060  Ghidra: work/06_ghidra/decompiled_rva/ConnectProxy__write_3arg_obj.c
        // Body is identical to 0x197305C (write(ushort,object)) — UID param ignored at runtime.
        // 1. If m_socket == null: NullReference.
        // 2. If !m_socket.good() OR m_writer == null: return -1.
        // 3. nLen = StructConverter.StructureToByteArray(m_temp, 6, obj); if 0 → return 0xffffff9a.
        // 4. proto_COMM.blockEncrypt(m_temp, 6, nLen); build local header {protoco, nLen, 0}; writeToByteArray(m_temp, 0).
        // 5. tail call: m_writer.vWrite(m_temp, 0, nLen+6).
        public int write(ushort protoco, object obj, uint UID)
        {
            if (this.m_socket == null)
            {
                // FUN_015cb8fc — il2cpp null-deref trap
                throw new NullReferenceException();
            }
            bool isGood = this.m_socket.good();
            int uVar7;
            if (!isGood || this.m_writer == null)
            {
                uVar7 = -1;
            }
            else
            {
                int iVar1 = StructConverter.StructureToByteArray(this.m_temp, 6, obj);
                if (iVar1 == 0)
                {
                    uVar7 = unchecked((int)0xffffff9a);
                }
                else
                {
                    proto_COMM.blockEncrypt(this.m_temp, 6, iVar1);
                    proto_COMM local_28 = default(proto_COMM);
                    local_28.m_pcProtoco = protoco;
                    local_28.m_pcSize = (ushort)iVar1;
                    local_28.m_pcCompressSize = 0;
                    local_28.writeToByteArray(this.m_temp, 0);
                    if (this.m_writer == null)
                    {
                        throw new NullReferenceException();
                    }
                    uVar7 = this.m_writer.vWrite(this.m_temp, 0, iVar1 + 6);
                }
            }
            return uVar7;
        }

        // RVA: 0x19731D0  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/write.c
        public int write(ushort protoco, byte[] bArray, uint UID = 0)
        {
            if (this.m_socket == null)
            {
                // FUN_015cb8fc — il2cpp null-deref trap
                throw new NullReferenceException();
            }
            bool isGood = this.m_socket.good();
            int uVar7;
            if (!isGood || this.m_writer == null)
            {
                uVar7 = -1;
            }
            else
            {
                int iVar6;
                if (bArray == null)
                {
                    iVar6 = 0;
                }
                else
                {
                    iVar6 = bArray.Length;
                    if (0 < iVar6)
                    {
                        if (this.m_temp == null)
                        {
                            // FUN_015cb8fc trap
                            throw new NullReferenceException();
                        }
                        if (this.m_temp.Length - 6 < iVar6)
                        {
                            return unchecked((int)0xffffff9a);
                        }
                        System.Array.Copy(bArray, 0, this.m_temp, 6, iVar6);
                        proto_COMM.blockEncrypt(this.m_temp, 6, iVar6);
                    }
                }
                proto_COMM local_28 = default(proto_COMM);
                local_28.m_pcProtoco = protoco;
                local_28.m_pcSize = (ushort)iVar6;
                local_28.m_pcCompressSize = 0;
                local_28.writeToByteArray(this.m_temp, 0);
                if (this.m_writer == null)
                {
                    // FUN_015cb8fc trap
                    throw new NullReferenceException();
                }
                uVar7 = this.m_writer.vWrite(this.m_temp, 0, iVar6 + 6);
            }
            return uVar7;
        }

        // RVA: 0x1973388  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/.ctor.c
        public ConnectProxy(TcpSocket socket, bool idleEnabled, float inactiveTime, uint proxyID, TcpSocket owner)
        {
            // TODO: confidence:low — Ghidra .c not generated for ctor; field assignments
            //   inferred from dump.cs offsets and TcpNetWork.createConnection usage.
            this.m_owner = owner;
            this.m_socket = socket;
            this.m_inactiveTime = inactiveTime;
            this.m_proxyID = proxyID;
            this.m_reader = new SangoPacketReader();
            this.m_writer = new SangoPacketWriter();
            this.m_temp = new byte[65006]; // napiPACKET.PACKAGE_TEMP_SIZE
            this.m_waitConnect = true;
            this.m_waitCloseTime = -1.0;
            this.m_waitSendDeadTime = -1.0;
            this.idleEnabled(idleEnabled);
        }

        // RVA: 0x19735A4  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/Finalize.c
        // Original signature: protected override void Finalize() — C# expresses this via destructor.
        ~ConnectProxy()
        {
            if (this.m_socket != null)
            {
                this.m_socket.close();
            }
        }

        // RVA: 0x1973644  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/lastActiveTime.c
        public double lastActiveTime()
        {
            if (this.m_idleEnabled)
            {
                return -1.0; // 0xbff0000000000000
            }
            return this.m_lastActiveTime;
        }

        // RVA: 0x197365C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/onConnected.c
        public bool onConnected()
        {
            // Ghidra: virtual interface dispatch on m_callback for IConnectProxyCB.vOnConnected.
            if (this.m_callback == null)
            {
                return false;
            }
            return this.m_callback.vOnConnected(this);
        }

        // RVA: 0x197370C  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/onStream.c
        public int onStream(ref proto_COMM header, byte[] data)
        {
            // Ghidra: virtual interface dispatch on m_callback for IConnectProxyCB.vOnStream.
            if (this.m_callback == null)
            {
                return 0;
            }
            return this.m_callback.vOnStream(this, ref header, data);
        }

        // RVA: 0x19737DC  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/onClosed.c
        public void onClosed()
        {
            // Ghidra: virtual interface dispatch on m_callback for IConnectProxyCB.vOnClosed.
            if (this.m_callback == null)
            {
                return;
            }
            this.m_callback.vOnClosed(this, this.m_errcode);
        }

        // RVA: 0x1973894  Ghidra: work/06_ghidra/decompiled_full/SONETWORK.ConnectProxy/run.c
        public void run(double runTime)
        {
            // TODO: confidence:medium — large dispatcher over m_socket / m_reader / m_writer
            //   with virtual interface calls; preserved control flow from Ghidra.
            //   Goto labels from Ghidra translated into nested if/return + RunWriteLoop helper
            //   to satisfy C# scoping rules.

            // ---- Wait-connect path ----
            if (this.m_waitConnect)
            {
                if (this.m_socket == null)
                {
                    // FUN_015cb8fc — il2cpp null-deref trap
                    throw new NullReferenceException();
                }
                int iVar3 = this.m_socket.connectReady();
                if (iVar3 == 1)
                {
                    bool ok = this.onConnected();
                    if (ok)
                    {
                        this.m_lastActiveTime = runTime + 3.0;
                        this.m_waitConnect = false;
                        return;
                    }
                }
                else if (iVar3 != -1)
                {
                    if (runTime <= this.m_lastActiveTime)
                    {
                        return;
                    }
                    if (this.m_socket == null)
                    {
                        throw new NullReferenceException();
                    }
                    this.m_socket.close();
                    this.m_errcode = unchecked((int)0xfffffff4);
                    return;
                }
                if (this.m_socket == null)
                {
                    throw new NullReferenceException();
                }
                this.m_socket.close();
                this.m_errcode = unchecked((int)0xfffffff5);
                return;
            }

            // ---- Owner liveness check (s_tickCloseCounter / s_lastTickMask vs frameCount) ----
            // If we early-return here, skip the read/write path. Otherwise fall through.
            bool skipToWaitClose = false;
            if (this.m_owner != null && !this.m_owner.good())
            {
                int frameNow = Time.frameCount;
                uint cnt;
                if (frameNow == (int)s_lastTickMask)
                {
                    cnt = s_tickCloseCounter;
                    if (0x13 < cnt)
                    {
                        skipToWaitClose = true;
                    }
                }
                else
                {
                    s_lastTickMask = (uint)frameNow;
                    s_tickCloseCounter = 0;
                    cnt = 0;
                }
                if (!skipToWaitClose)
                {
                    s_tickCloseCounter = cnt + 1;
                    if (this.m_socket != null)
                    {
                        this.m_socket.close();
                        this.m_errcode = unchecked((int)0xfffffffa);
                        return;
                    }
                    throw new NullReferenceException();
                }
            }

            // ---- LAB_01a739d8: wait-close vs normal read ----
            int iv3 = 0;
            bool jumpToWriteLoop = false;
            bool jumpToCloseDown = false;
            if (this.m_waitClose)
            {
                if (this.m_writer == null)
                {
                    throw new NullReferenceException();
                }
                bool isEmpty = this.m_writer.vEmpty();
                if (isEmpty)
                {
                    double dVar16 = this.m_waitCloseTime;
                    if (dVar16 < 0.0)
                    {
                        dVar16 = runTime + 1.0;
                        this.m_waitCloseTime = dVar16;
                    }
                    if (dVar16 <= runTime)
                    {
                        jumpToCloseDown = true;
                    }
                }
                if (!jumpToCloseDown)
                {
                    jumpToWriteLoop = true;
                }
            }
            else
            {
                // Normal read path: vBuffer → TcpSocket.read → vRecvLength
                if (this.m_reader == null)
                {
                    throw new NullReferenceException();
                }
                int rdOffset;
                int rdSize;
                byte[] rdBuf = this.m_reader.vBuffer(out rdOffset, out rdSize);
                if (rdSize < 1)
                {
                    this.m_errcode = unchecked((int)0xfffffff1);
                    jumpToCloseDown = true;
                }
                else
                {
                    if (this.m_socket == null)
                    {
                        throw new NullReferenceException();
                    }
                    iv3 = this.m_socket.read(rdBuf, rdOffset, rdSize);
                    if (-1 < iv3)
                    {
                        if (iv3 != 0)
                        {
                            this.m_lastActiveTime = (double)this.m_inactiveTime + runTime;
                            if (this.m_reader == null)
                            {
                                throw new NullReferenceException();
                            }
                            int iVar4 = this.m_reader.vRecvLength(iv3, this);
                            this.m_errcode = iVar4;
                            if (iVar4 != 0 || iv3 < 0)
                            {
                                jumpToCloseDown = true;
                            }
                        }
                        if (!jumpToCloseDown)
                        {
                            jumpToWriteLoop = true;
                        }
                    }
                    else
                    {
                        this.m_errcode = iv3;
                        jumpToCloseDown = true;
                    }
                }
            }

            if (jumpToWriteLoop)
            {
                if (RunWriteLoop(runTime, ref iv3))
                {
                    // Caller-side return: write loop indicated normal return.
                    return;
                }
                // Otherwise fall through to LAB_01a73d98.
            }

            // ---- LAB_01a73d98: close-down ----
            if (this.m_socket != null)
            {
                this.m_socket.close();
                return;
            }
            // FUN_015cb8fc trap
            throw new NullReferenceException();
        }

        // Inner write-drain loop (LAB_01a73b78). Returns true if the caller should `return`
        // immediately (mid-loop early-exit path); false if it should fall through to the
        // close-down sequence (LAB_01a73d98).
        private bool RunWriteLoop(double runTime, ref int iVar3)
        {
            IPacketWriter wr = this.m_writer;
            while (wr != null)
            {
                int wrOffset;
                int wrSize;
                byte[] wrBuf = wr.vBuffer(out wrOffset, out wrSize);
                if (wrSize < 1)
                {
                    if (-1 < iVar3)
                    {
                        return true;
                    }
                    return false; // → close-down
                }
                this.m_lastActiveTime = (double)this.m_inactiveTime + runTime;
                if (this.m_socket == null) break;
                iVar3 = this.m_socket.write(wrBuf, wrOffset, wrSize);
                if (iVar3 < 1)
                {
                    if (-1 < iVar3)
                    {
                        return true;
                    }
                    if (iVar3 != -7)
                    {
                        this.m_errcode = iVar3;
                        return false; // → close-down
                    }
                    double dVar16 = this.m_waitSendDeadTime;
                    if (dVar16 < 0.0)
                    {
                        dVar16 = runTime + 10.0;
                        this.m_waitSendDeadTime = dVar16;
                    }
                    if (runTime <= dVar16)
                    {
                        return true;
                    }
                    this.m_errcode = unchecked((int)0xfffffff9);
                    return false; // → close-down
                }
                wr = this.m_writer;
                this.m_waitSendDeadTime = -1.0;
                if (wr == null) break;
                wr.vSendLength(iVar3);
                wr = this.m_writer;
            }
            // FUN_015cb8fc trap
            throw new NullReferenceException();
        }
    }
}
