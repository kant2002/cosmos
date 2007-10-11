using System;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Cil;
using CPU = Indy.IL2CPU.Assembler.X86;

namespace Indy.IL2CPU.IL.X86 {
	[OpCode(Code.Rem)]
	public class Rem: Op {
		public Rem(Mono.Cecil.Cil.Instruction aInstruction, MethodInformation aMethodInfo)
			: base(aInstruction, aMethodInfo) {
		}
		public override void DoAssemble() {
			int xSize = Assembler.StackSizes.Peek();
			Pop("ecx");
			Pop("eax");
			Assembler.Add(new CPU.Divide("ecx"));
			Pushd(xSize, "edx");
		}
	}
}