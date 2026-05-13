// Source: dump.cs TypeDefIndex 1088 — public abstract class MarsMessageProcess
// Signatures 1-1 from work/03_il2cpp_dump/dump.cs lines 2521305+.
// RVAs preserved for Ghidra body lookup work/06_ghidra/decompiled_full/MarsSDK.MarsMessageProcess/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MarsAgent.Login;                  // PlatformDefinition enum
using MarsEn;
using MarsEn.UjRandom;
using MarsSDK.ThirdParty.Extensions;    // ExtensionDefinition enum

namespace MarsSDK
{
    public abstract class MarsMessageProcess
    {
        // Auto-property backing fields (matches dump.cs <Property>k__BackingField)
        [CompilerGenerated] private EOperationAgent      _AgentId;             // 0x10
        [CompilerGenerated] private PlatformDefinition   _PlatformDefinition;  // 0x14
        [CompilerGenerated] private ExtensionDefinition  _ExtensionDefinition; // 0x18
        private Dictionary<string, MessageProcessDelegate> mMsgIdToProcesses;  // 0x20

        // Properties: get_X public, set_X private (per dump.cs)
        public EOperationAgent AgentId
        {
            // RVA: 0x19933C4
            get { return _AgentId; }
            // RVA: 0x19933BC
            set /* v1 Wrap compat: dump.cs private */ { _AgentId = value; }
        }

        public PlatformDefinition PlatformDefinition
        {
            // RVA: 0x19933D4
            get { return _PlatformDefinition; }
            // RVA: 0x19933CC
            set /* v1 Wrap compat: dump.cs private */ { _PlatformDefinition = value; }
        }

        public ExtensionDefinition ExtensionDefinition
        {
            // RVA: 0x19933E4
            get { return _ExtensionDefinition; }
            // RVA: 0x19933DC
            set /* v1 Wrap compat: dump.cs private */ { _ExtensionDefinition = value; }
        }

        // 6 protected constructors (dump.cs RVA 0x19933EC, 0x1993400, 0x1993410, 0x19933F8, 0x1993420, 0x199342C)
        protected MarsMessageProcess(EOperationAgent agentId)
        {
            _AgentId = agentId;
            mMsgIdToProcesses = new Dictionary<string, MessageProcessDelegate>();
        }

        protected MarsMessageProcess(PlatformDefinition platformDefinition)
        {
            _PlatformDefinition = platformDefinition;
            mMsgIdToProcesses = new Dictionary<string, MessageProcessDelegate>();
        }

        protected MarsMessageProcess(ExtensionDefinition extensionDefinition)
        {
            _ExtensionDefinition = extensionDefinition;
            mMsgIdToProcesses = new Dictionary<string, MessageProcessDelegate>();
        }

        protected MarsMessageProcess(EOperationAgent agentId, PlatformDefinition platformDefinition)
        {
            _AgentId = agentId;
            _PlatformDefinition = platformDefinition;
            mMsgIdToProcesses = new Dictionary<string, MessageProcessDelegate>();
        }

        protected MarsMessageProcess(EOperationAgent agentId, ExtensionDefinition extensionDefinition)
        {
            _AgentId = agentId;
            _ExtensionDefinition = extensionDefinition;
            mMsgIdToProcesses = new Dictionary<string, MessageProcessDelegate>();
        }

        protected MarsMessageProcess(EOperationAgent agentId, PlatformDefinition platformDefinition, ExtensionDefinition extensionDefinition)
        {
            _AgentId = agentId;
            _PlatformDefinition = platformDefinition;
            _ExtensionDefinition = extensionDefinition;
            mMsgIdToProcesses = new Dictionary<string, MessageProcessDelegate>();
        }

        // Source: Ghidra AddMessageProcess.c  RVA 0x1993714
        // 1-1: mMsgIdToProcesses[msgId] = process. NRE if mMsgIdToProcesses null.
        protected void AddMessageProcess(string msgId, MessageProcessDelegate process)
        {
            if (mMsgIdToProcesses == null) throw new System.NullReferenceException();
            mMsgIdToProcesses[msgId] = process;
        }

        // Source: Ghidra V_doMessageProcess.c  RVA 0x199377C
        // 1-1: if mMsgIdToProcesses null: NRE. process = mMsgIdToProcesses[msg].
        //      If process != null: process(args). NRE if process not found.
        protected virtual void V_doMessageProcess(string msg, string[] args)
        {
            if (mMsgIdToProcesses == null) throw new System.NullReferenceException();
            MessageProcessDelegate process = mMsgIdToProcesses[msg];
            if (process == null) throw new System.NullReferenceException();
            process(args);
        }

        // Source: Ghidra ExcludeTraceLog.c  RVA 0x1993C54
        // Returns true if AgentId == 0x16 (22) OR (AgentId == 1 AND msgId == "349" [lit 1528]).
        private bool ExcludeTraceLog(string msgId)
        {
            int agent = (int)_AgentId;
            if (agent == 0x16) return true;
            if (agent == 1 && msgId == "349") return true;
            return false;
        }

        // Source: Ghidra DoMessageProcess.c  RVA 0x1993CCC
        // 1. If ExcludeTraceLog(msg): V_doMessageProcess(msg, args); return.
        // 2. Build prefix string:
        //    - If _AgentId != -1: prefix = "[4]" + _AgentId.ToString("D") + "\t"  [lits 12798/13178]
        //    - If _PlatformDefinition >= 0: prefix += "[OnRewardAdFullScreenContentOpened]" + PlatDef.ToString("D") + "\t"  [lits 13027/13178]
        //    - If _ExtensionDefinition != 0: prefix += "[Delegate...]" + ExtDef.ToString("D") + "\t"  [lits 12949/13178]
        // 3. MarsLog.Info("{0}:{1}(0x{2:X})", new object[]{ prefix, msg })  [lit 21258]
        // 4. V_doMessageProcess(msg, args).
        // String literals resolved via global-metadata.dat by Ghidra metadata index — values verbatim
        // (some labels look odd e.g. "[OnRewardAdFullScreenContentOpened]" but that's what binary stores).
        internal void DoMessageProcess(string msg, string[] args)
        {
            if (ExcludeTraceLog(msg))
            {
                V_doMessageProcess(msg, args);
                return;
            }
            string s = "";
            if ((int)_AgentId != -1)
            {
                s = "[4]" + ((int)_AgentId).ToString("D") + "\t";
            }
            if ((int)_PlatformDefinition >= 0)
            {
                s = s + "[OnRewardAdFullScreenContentOpened]" + ((int)_PlatformDefinition).ToString("D") + "\t";
            }
            if ((int)_ExtensionDefinition != 0)
            {
                s = s + "[Delegate not implemented warning] missed : TelephoneVerifyPlatform.doEventVerifyCodeTimeout" + ((int)_ExtensionDefinition).ToString("D") + "\t";
            }
            MarsLog.Info("{0}:{1}(0x{2:X})", new object[] { s, msg });
            V_doMessageProcess(msg, args);
        }

        // Slot 5 — ABSTRACT (subclasses must implement)
        public abstract Type GetMessageUserType();

        // Nested delegate per dump.cs TypeDefIndex 1087
        // (`public sealed class MessageProcessDelegate : MulticastDelegate` in IL = C# `delegate` keyword)
        public delegate void MessageProcessDelegate(string[] args);
    }
}
