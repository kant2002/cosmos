using System;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Ldarg)]
	public class Ldarg: ILOp
	{
		public Ldarg(Cosmos.IL2CPU.Assembler aAsmblr):base(aAsmblr)
		{
		}

    public override void Execute(uint aMethodUID, ILOpCode aOpCode) {
      throw new Exception("TODO:");
    }

    #region Old code
		// using System;
		// using System.Collections.Generic;
		// using System.IO;
		// 
		// using CPU = Indy.IL2CPU.Assembler.X86;
		// 
		// namespace Indy.IL2CPU.IL.X86 {
		// 	[OpCode(OpCodeEnum.Ldarg)]
		// 	public class Ldarg: Op {
		// 		private MethodInformation.Argument mArgument;
		// 		protected void SetArgIndex(int aIndex, MethodInformation aMethodInfo) {
		// 			mArgument = aMethodInfo.Arguments[aIndex];
		// 		}
		// 
		// 		public Ldarg(MethodInformation aMethodInfo, int aIndex)
		// 			: base(null, aMethodInfo) {
		// 			SetArgIndex(aIndex, aMethodInfo);
		// 		}
		// 
		// 		public Ldarg(ILReader aReader, MethodInformation aMethodInfo)
		// 			: base(aReader, aMethodInfo) {
		// 			int xArgIndex;
		// 			if (aReader != null) {
		// 				xArgIndex = aReader.OperandValueInt32;
		// 				SetArgIndex(xArgIndex, aMethodInfo);
		// 				//ParameterDefinition xParam = aReader.Operand as ParameterDefinition;
		// 				//if (xParam != null) {
		// 				//    SetArgIndex(xParam.Sequence - 1, aMethodInfo);
		// 				//}
		// 			}
		// 		}
		// 
		// 		public override void DoAssemble() {
		// 			Ldarg(Assembler, mArgument);
		// 		}
		// 	}
		// }
		#endregion Old code
	}
}
