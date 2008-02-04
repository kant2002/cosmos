using System;

using CPUx86 = Indy.IL2CPU.Assembler.X86;
using Indy.IL2CPU.Assembler;

namespace Indy.IL2CPU.IL.X86 {
	[OpCode(OpCodeEnum.Ldind_U1)]
	public class Ldind_U1: Op {
		public Ldind_U1(ILReader aReader, MethodInformation aMethodInfo)
			: base(aReader, aMethodInfo) {
		}
		public override void DoAssemble() {
			Assembler.StackContents.Pop();
			new CPUx86.Pop(CPUx86.Registers.ECX);
			new CPUx86.Move(CPUx86.Registers.EAX, "0");
			new CPUx86.Move(CPUx86.Registers.AL, CPUx86.Registers.AtECX);
			new CPUx86.Push(CPUx86.Registers.EAX);
			Assembler.StackContents.Push(new StackContent(1, typeof(byte)));
		}
	}
}