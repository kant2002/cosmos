﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Compiler.Assembler.X86;
using Cosmos.Compiler.Debug;
using Cosmos.Compiler.XSharp;

namespace Cosmos.Compiler.DebugStub {
    public class DebugStub : CodeGroup {
        protected const uint VidBase = 0xB8000;
        static protected int mComNo = 0;

        public DebugStub(int aComNo) {
            mComNo = aComNo;
        }

        // Called before Kernel runs. Inits debug stub, etc
        public class Init : CodeBlock {
            public override void Assemble() {
                Call<Cls>();
                Call<DisplayWaitMsg>();
                Call<InitSerial>();
                Call<WaitForDbgHandshake>();
                Call<Cls>();
            }
        }

        public class WaitForDbgHandshake : CodeBlock {
            public override void Assemble() {
                // "Clear" the UART out
                AL = 0;
                Call<DebugStub.WriteALToComPort>();

                // QEMU (and possibly others) send some garbage across the serial line first.
                // Actually they send the garbage inbound, but garbage could be inbound as well so we 
                // keep this.
                // To work around this we send a signature. DC then discards everything before the signature.
                // QEMU has other serial issues too, and we dont support it anymore, but this signature is a good
                // feature so we kept it.
                Push(Consts.SerialSignature);
                ESI = ESP;
                Call("WriteByteToComPort");
                Call("WriteByteToComPort");
                Call("WriteByteToComPort");
                Call("WriteByteToComPort");
                // Restore ESP, we actually dont care about EAX or the value on the stack anymore.
                EAX.Pop();

                // We could use the signature as the start signal, but I prefer
                // to keep the logic separate, especially in DC.
                AL = (int)MsgType.Started; // Send the actual started signal
                Call<DebugStub.WriteALToComPort>();

                Call("DebugStub_WaitForSignature");
                Call("DebugStub_ProcessCommandBatch");
            }
        }

        public class InitSerial : CodeBlock {
            public override void Assemble() {
                UInt16[] xComPortAddresses = { 0x3F8, 0x2F8, 0x3E8, 0x2E8 };
                UInt16 xComAddr = xComPortAddresses[mComNo - 1];
                // http://www.nondot.org/sabre/os/files/Communication/ser_port.txt

                // disable interrupts for serial stuff
                DX = (UInt16)(xComAddr + 1);
                AL = 0;
                Port[DX] = AL;

                // Enable DLAB (set baud rate divisor)
                DX = (UInt16)(xComAddr + 3);
                AL = 0x80;
                Port[DX] = AL;

                // 0x01 - 0x00 - 115200
                // 0x02 - 0x00 - 57600
                // 0x03 - 0x00 - 38400
                //
                // Set divisor (lo byte)
                DX = xComAddr;
                AL = 0x01;
                Port[DX] = AL;
                // hi byte
                DX = (UInt16)(xComAddr + 1);
                AL = 0x00;
                Port[DX] = AL;
                
                // 8N1
                DX = (UInt16)(xComAddr + 3);
                AL = 0x03;
                Port[DX] = AL;

                // Enable FIFO, clear them, with 14-byte threshold
                DX = (UInt16)(xComAddr + 2);
                AL = 0xC7;
                Port[DX] = AL;
                
                // 0x20 AFE Automatic Flow control Enable - 16550 (VMWare uses 16550A) is most common and does not support it
                // 0x02 RTS
                // 0x01 DTR
                // Send 0x03 if no AFE
                DX = (UInt16)(xComAddr + 4);
                AL = 0x03;
                Port[DX] = AL;
            }
        }

        public class DisplayWaitMsg : CodeBlock {
            // http://wiki.osdev.org/Text_UI
            // Later can cycle for x changes of second register:
            // http://wiki.osdev.org/Time_And_Date
            public override void Assemble() {
                ESI = AddressOf("DebugWaitMsg");
                // 10 lines down, 20 cols in
                EDI = DebugStub.VidBase + (10 * 80 + 20) * 2;

                // Read and copy string till 0 terminator
                Label = "DebugStub_Init_ReadChar";
                AL = Memory[ESI, 8];
                AL.Compare(0);
                JumpIf(Flags.Equal, "DebugStub_Init_AfterMsg");
                ESI++;
                Memory[EDI, 8] = AL;
                EDI++;
                EDI++;
                Jump("DebugStub_Init_ReadChar");
                //TODO: Local labels in X#
                Label = "DebugStub_Init_AfterMsg";
            }
        }

        public class WriteALToComPort : CodeBlock {
            // Input: AL
            // Output: None
            // Modifies: EAX, EDX, ESI
            public override void Assemble() {
                EAX.Push();
                ESI = ESP;
                Call("WriteByteToComPort");
                EAX.Pop(); // Is a local, cant use Return(4)
            }
        }

        public class Cls : CodeBlock {
            public override void Assemble() {
                ESI = DebugStub.VidBase;
                // TODO: X# upgrade this
                Label = "DebugStub_Cls_More";
                    //TODO: Fix to direct memory write after we fix the X# bug with Memory[ESI, 8] = 0x0A;
                    AL = 0x00;
                    Memory[ESI, 8] = AL; // Text
                    ESI++;

                    AL = 0x0A;
                    Memory[ESI, 8] = AL; // Colour
                    ESI++;

                    ESI.Compare(DebugStub.VidBase + 25 * 80 * 2);
                JumpIf(Flags.LessThan, "DebugStub_Cls_More");
            }
        }

    }
}
