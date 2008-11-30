﻿using Indy.IL2CPU.Plugs;
using Assembler=Indy.IL2CPU.Assembler.Assembler;
using CPUAll = Indy.IL2CPU.Assembler;
using CPUx86 = Indy.IL2CPU.Assembler.X86;
using Indy.IL2CPU.Assembler.X86;

namespace Cosmos.Sys.Plugs.Assemblers
{
    public class Reboot : AssemblerMethod
    {
        public override void Assemble(Assembler aAssembler)
        {
        	new CPUx86.ClrInterruptFlag();
            new CPUx86.Move { DestinationReg = Registers.DX, SourceValue = 0x60 };
    /* Clear all keyboard buffers (output and command buffers) */
        	new CPUAll.Label(".clearBuffer");
            new CPUx86.In { Size = 16, DestinationValue = 0x64 };
            new CPUx86.Move { DestinationReg = Registers.AH, SourceReg = Registers.AL };
            new CPUx86.Test { DestinationReg = Registers.AH, SourceValue = 1 };
            new CPUx86.JumpIfZero { DestinationLabel = ".skipClearIO" };
            new CPUx86.In { Size = 16 };
        	new CPUAll.Label(".skipClearIO");
            new CPUx86.Test { DestinationReg = Registers.AH, SourceValue = 2 };
            new JumpIfNotZero { DestinationLabel = ".clearBuffer" };
            new Move { DestinationReg = Registers.DX, SourceValue = 0x64 };
            new Move { DestinationReg = Registers.AL, SourceValue = 0xfe };
            new Out { Size = 8 };
            new CPUAll.Label(".loop");//failed... halt
            new CPUx86.Halt();
            new CPUx86.Jump { DestinationLabel = ".loop" };
        }
    }
}
