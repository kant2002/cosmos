using System;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Cil;
using CPU = Indy.IL2CPU.Assembler.X86;

namespace Indy.IL2CPU.IL.X86 {
	[OpCode(Code.Ldind_U2)]
	public class Ldind_U2: Op {
		public Ldind_U2(Mono.Cecil.Cil.Instruction aInstruction, MethodInformation aMethodInfo)
			: base(aInstruction, aMethodInfo) {
		}
		public override void DoAssemble() {
			Assembler.StackSizes.Pop();
			new CPU.Pop("eax");
			new CPU.Move("edx", "0");
			new CPU.Move("word dx", "[eax]");
			new CPU.Pushd("edx");
			Assembler.StackSizes.Push(2);
		}
	}
}