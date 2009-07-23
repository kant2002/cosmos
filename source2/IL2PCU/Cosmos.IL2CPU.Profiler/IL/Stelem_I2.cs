using System;

namespace Cosmos.IL2CPU.Profiler.IL
{
	[Cosmos.IL2CPU.OpCode(ILOp.Code.Stelem_I2)]
	public class Stelem_I2: ILOpProfiler
	{



		#region Old code
		// using System;
		// using System.IO;
		// 
		// 
		// using CPU = Indy.IL2CPU.Assembler;
		// using CPUx86 = Indy.IL2CPU.Assembler.X86;
		// 
		// namespace Indy.IL2CPU.IL.X86 {
		// 	[Cosmos.IL2CPU.OpCode(ILOp.Code.Stelem_I2)]
		// 	public class Stelem_I2: ILOpProfiler {
		// 		private string mNextLabel;
		// 	    private string mCurLabel;
		// 	    private uint mCurOffset;
		// 	    private MethodInformation mMethodInformation;
		//         
		//         public Stelem_I2(ILReader aReader, MethodInformation aMethodInfo)
		// 			: base(aReader, aMethodInfo) {
		//              mMethodInformation = aMethodInfo;
		// 		    mCurOffset = aReader.Position;
		// 		    mCurLabel = IL.Op.GetInstructionLabel(aReader);
		//             mNextLabel = IL.Op.GetInstructionLabel(aReader.NextPosition);
		// 		}
		// 		
		// 		public override void DoAssemble() {
		//             Stelem_Ref.Assemble(Assembler, 2, GetServiceProvider(), mCurLabel, mMethodInformation, mCurOffset, mNextLabel);
		// 		}
		// 	}
		// }
		#endregion
	}
}
