﻿using System;
using Indy.IL2CPU.Assembler.X86;
using Indy.IL2CPU.Plugs;
using Assembler = Indy.IL2CPU.Assembler.Assembler;
using CPUAll = Indy.IL2CPU.Assembler;
using CPUx86 = Indy.IL2CPU.Assembler.X86;

namespace Cosmos.Kernel.Plugs.Assemblers {
	public class ZeroFill: AssemblerMethod {

		//		public static void ZeroFill(uint aStartAddress, uint aLength) {}
		public override void Assemble(Assembler aAssembler) {
            new ClrDirFlag();
            new CPUx86.Move { DestinationReg = Registers.EDI, SourceReg = Registers.EBP, SourceIsIndirect = true, SourceDisplacement = 0xC }; //address
            new CPUx86.Move { DestinationReg = Registers.ECX, SourceReg = Registers.EBP, SourceIsIndirect = true, SourceDisplacement = 0x8 }; //length
            new CPUx86.Move { DestinationReg = Registers.EAX, SourceValue = 0 };
            new CPUx86.ShiftRight { DestinationReg = Registers.ECX, SourceValue = 1 };
            new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.Below, DestinationLabel = ".step2" };
			new CPUx86.StoreByteInString();
			new CPUAll.Label(".step2");
            new CPUx86.ShiftRight { DestinationReg = Registers.ECX, SourceValue= 1 };
            new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.Below, DestinationLabel = ".step3" };
			new CPUx86.StoreWordInString();
			new CPUAll.Label(".step3");
			new CPUx86.RepeatStos{Size=32};						
		}
	}
}
