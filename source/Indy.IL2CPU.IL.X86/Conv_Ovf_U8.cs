using System;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Cil;
using CPU = Indy.IL2CPU.Assembler.X86;

namespace Indy.IL2CPU.IL.X86 {
	[OpCode(Code.Conv_Ovf_U8)]
	public class Conv_Ovf_U8: Op {
		public override void Assemble(Instruction aInstruction) {
			throw new NotImplementedException("This file has been autogenerated and not been changed afterwards!");
		}
	}
}