using System;

namespace Cosmos.IL2CPU.Profiler.IL
{
	[Cosmos.IL2CPU.OpCode(ILOp.Code.Stind_R8)]
	public class Stind_R8: ILOpProfiler
	{



		#region Old code
		// using System;
		// using System.IO;
		// 
		// 
		// using CPU = Indy.IL2CPU.Assembler.X86;
		// 
		// namespace Indy.IL2CPU.IL.X86 {
		// 	[Cosmos.IL2CPU.OpCode(ILOp.Code.Stind_R8)]
		// 	public class Stind_R8: ILOpProfiler {
		// 		public Stind_R8(ILReader aReader, MethodInformation aMethodInfo)
		// 			: base(aReader, aMethodInfo) {
		// 		}
		// 		public override void DoAssemble() {
		// 			Stind_I.Assemble(Assembler, 8);
		// 		}
		// 	}
		// }
		#endregion
	}
}
