﻿using System;
using Indy.IL2CPU.Assembler;
using Indy.IL2CPU.Assembler.X86;
using Indy.IL2CPU.Plugs;
using Assembler=Indy.IL2CPU.Assembler.Assembler;
using CPUx86 = Indy.IL2CPU.Assembler.X86;
using CPUNative = Indy.IL2CPU.Assembler.X86;
using System.Collections.Generic;

namespace Cosmos.Kernel.Plugs.Assemblers {
	public class CreateGDT : AssemblerMethod {
		public override void Assemble(Assembler aAssembler) {
			string xFieldName = "_NATIVE_GDT_Contents";
		    string xFieldData // Null Segment
		        = "0,0,0,0,0,0,0,0" // Code Segment
		          + ", 0xFF, 0xFF, 0, 0, 0, 0x99, 0xCF, 0" // Data Segment
		          + ", 0xFF,0xFF,0,0,0,0x93,0xCF,0";
			//aAssembler.DataMembers.Add(new KeyValuePair<string, DataMember> (aAssembler.CurrentGroup,new DataMember(xFieldName, "db", xFieldData)));
			//xFieldName = "_NATIVE_GDT_Pointer";
			////xFieldData = "0x17, (_NATIVE_GDT_Contents and 0xFFFF), (_NATIVE_GDT_Contents shr 16)";
			//aAssembler.DataMembers.Add(new KeyValuePair<string, DataMember> (aAssembler.CurrentGroup,new DataMember(xFieldName, "dw", "0x37,0,0")));

            aAssembler.DataMembers.Add(new DataMember(xFieldName, "db", xFieldData));
            aAssembler.DataMembers.Add(new DataMember("_NATIVE_GDT_Pointer", "dw", "0x17,0,0"));

			new CPUx86.Move(Registers.EAX, "_NATIVE_GDT_Pointer");
			new CPUx86.Move("dword [_NATIVE_GDT_Pointer + 2]", "_NATIVE_GDT_Contents");
            
			new Label(".RegisterGDT");
            new CPUx86.Move(Registers.EAX, "_NATIVE_GDT_Pointer");
			new CPUNative.Lgdt(Registers.AtEAX);
			new CPUx86.Move(Registers.AX, "0x10");
			new CPUx86.Move("ds", Registers.AX);
            new CPUx86.Move("es", Registers.AX);
            new CPUx86.Move("fs", Registers.AX);
            new CPUx86.Move("gs", Registers.AX);
            new CPUx86.Move("ss", Registers.AX);				 
            // Force reload of code segement
			new CPUx86.Jump("0x8:flush__GDT__table");
			new Label("flush__GDT__table");
		}
	}
}
