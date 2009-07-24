using System;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Unbox_Any)]
	public class Unbox_Any: ILOpX86
	{
		public Unbox_Any(ILOpCode aOpCode):base(aOpCode)
		{
		}

		#region Old code
		// using System;
		// using System.IO;
		// 
		// 
		// using CPU = Indy.IL2CPU.Assembler.X86;
		// 
		// namespace Indy.IL2CPU.IL.X86 {
		// 	[OpCode(OpCodeEnum.Unbox_Any)]
		// 	public class Unbox_Any: Unbox {
		// 		public Unbox_Any(ILReader aReader, MethodInformation aMethodInfo)
		// 			: base(aReader, aMethodInfo) {
		// 		}
		// 	}
		// }
		#endregion Old code
	}
}
