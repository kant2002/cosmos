using System;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Mkrefany)]
	public class Mkrefany: ILOp
	{
		public Mkrefany(Cosmos.Compiler.Assembler.Assembler aAsmblr):base(aAsmblr)
		{
		}

    public override void Execute(MethodInfo aMethod, ILOpCode aOpCode) {
      throw new NotImplementedException();
    }

    
		// using System;
		// using System.IO;
		// 
		// 
		// using CPU = Cosmos.Compiler.Assembler.X86;
		// 
		// namespace Cosmos.IL2CPU.IL.X86 {
		// 	[OpCode(OpCodeEnum.Mkrefany)]
		// 	public class Mkrefany: Op {
		// 		public Mkrefany(ILReader aReader, MethodInformation aMethodInfo)
		// 			: base(aReader, aMethodInfo) {
		// 		}
		// 		public override void DoAssemble() {
		// 			// todo: implement correctly
		// 		}
		// 	}
		// }
		
	}
}
