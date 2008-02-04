using System;
using System.IO;


using CPU = Indy.IL2CPU.Assembler.X86;

namespace Indy.IL2CPU.IL.X86 {
	[OpCode(OpCodeEnum.Mul)]
	public class Mul: Op {
		public Mul(ILReader aReader, MethodInformation aMethodInfo)
			: base(aReader, aMethodInfo) {
		}
		public override void DoAssemble() {
			Multiply(Assembler);
		}
	}
}