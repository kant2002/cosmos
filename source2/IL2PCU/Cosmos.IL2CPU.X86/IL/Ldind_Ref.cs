using System;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Ldind_Ref)]
	public class Ldind_Ref: ILOp
	{
		public Ldind_Ref(Cosmos.IL2CPU.Assembler aAsmblr):base(aAsmblr)
		{
		}

    public override void Execute(uint aMethodUID, ILOpCode aOpCode) {
      //TODO: Implement this Op
    }

    
		// using System;
		// using System.IO;
		// 
		// 
		// using CPU = Indy.IL2CPU.Assembler.X86;
		// 
		// namespace Indy.IL2CPU.IL.X86 {
		// 	[OpCode(OpCodeEnum.Ldind_Ref)]
		// 	public class Ldind_Ref: Op {
		// 		public Ldind_Ref(ILReader aReader, MethodInformation aMethodInfo)
		// 			: base(aReader, aMethodInfo) {
		// 		}
		// 		public override void DoAssemble() {
		//             new CPU.Pop { DestinationReg = CPU.Registers.EAX };
		//             new CPU.Push { DestinationReg = CPU.Registers.EAX, DestinationIsIndirect = true };
		// 		}
		// 	}
		// }
		
	}
}
