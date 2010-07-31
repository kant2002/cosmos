﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Debugger.Interop;
using System.Diagnostics;
using Microsoft.VisualStudio;

namespace Cosmos.Debug.VSDebugEngine {
    // This class represents a breakpoint that has been bound to a location in the debuggee. It is a child of the pending breakpoint
    // that creates it. Unless the pending breakpoint only has one bound breakpoint, each bound breakpoint is displayed as a child of the
    // pending breakpoint in the breakpoints window. Otherwise, only one is displayed.
    class AD7BoundBreakpoint : IDebugBoundBreakpoint2 {
        protected AD7PendingBreakpoint m_pendingBreakpoint;
        protected AD7BreakpointResolution m_breakpointResolution;
        protected AD7Engine mEngine;
        protected bool mEnabled = true;
        protected bool mDeleted = false;
        public uint mAddress;

        public AD7BoundBreakpoint(AD7Engine aEngine, uint aAddress, AD7PendingBreakpoint pendingBreakpoint, AD7BreakpointResolution breakpointResolution) {
            mEngine = aEngine;
            mAddress = aAddress;
            m_pendingBreakpoint = pendingBreakpoint;
            m_breakpointResolution = breakpointResolution;
            mEngine.mProcess.SetBreakpointAddress(aAddress);
        }

        // Called when the breakpoint is being deleted by the user.
        int IDebugBoundBreakpoint2.Delete() {
            if (!mDeleted) {
                mDeleted = true;
                m_pendingBreakpoint.OnBoundBreakpointDeleted(this);
            }
            return VSConstants.S_OK;
        }

        // Called by the debugger UI when the user is enabling or disabling a breakpoint.
        int IDebugBoundBreakpoint2.Enable(int aEnable) {
            bool xEnabled = aEnable != 0;
            if (mEnabled != xEnabled) {
                // A production debug engine would remove or add the underlying int3 here. The sample engine does not support true disabling
                // of breakpionts.
            }
            mEnabled = aEnable != 0;
            return VSConstants.S_OK;
        }

        // Return the breakpoint resolution which describes how the breakpoint bound in the debuggee.
        int IDebugBoundBreakpoint2.GetBreakpointResolution(out IDebugBreakpointResolution2 ppBPResolution) {
            ppBPResolution = m_breakpointResolution;
            return VSConstants.S_OK;
        }

        // Return the pending breakpoint for this bound breakpoint.
        int IDebugBoundBreakpoint2.GetPendingBreakpoint(out IDebugPendingBreakpoint2 ppPendingBreakpoint) {
            ppPendingBreakpoint = m_pendingBreakpoint;
            return VSConstants.S_OK;
        }

        int IDebugBoundBreakpoint2.GetState(out uint pState) {
            pState = 0;

            if (mDeleted) {
                pState = (uint)enum_BP_STATE.BPS_DELETED;
            } else if (mEnabled) {
                pState = (uint)enum_BP_STATE.BPS_ENABLED;
            } else {
                pState = (uint)enum_BP_STATE.BPS_DISABLED;
            }

            return VSConstants.S_OK;
        }

        // The sample engine does not support hit counts on breakpoints. A real-world debugger will want to keep track 
        // of how many times a particular bound breakpoint has been hit and return it here.
        int IDebugBoundBreakpoint2.GetHitCount(out uint pdwHitCount) {
            throw new NotImplementedException();
        }

        // The sample engine does not support conditions on breakpoints.
        // A real-world debugger will use this to specify when a breakpoint will be hit
        // and when it should be ignored.
        int IDebugBoundBreakpoint2.SetCondition(BP_CONDITION bpCondition) {
            throw new NotImplementedException();
        }

        // The sample engine does not support hit counts on breakpoints. A real-world debugger will want to keep track 
        // of how many times a particular bound breakpoint has been hit. The debugger calls SetHitCount when the user 
        // resets a breakpoint's hit count.
        int IDebugBoundBreakpoint2.SetHitCount(uint dwHitCount) {
            throw new NotImplementedException();
        }

        // The sample engine does not support pass counts on breakpoints.
        // This is used to specify the breakpoint hit count condition.
        int IDebugBoundBreakpoint2.SetPassCount(BP_PASSCOUNT bpPassCount) {
            throw new NotImplementedException();
        }

    }
}
