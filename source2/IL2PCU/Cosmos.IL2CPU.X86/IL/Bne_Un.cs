using System;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Bne_Un)]
	public class Bne_Un: ILOpX86
	{
		public Bne_Un(ILOpCode aOpCode):base(aOpCode)
		{
		}

		#region Old code
		// using System;
		// using System.IO;
		// 
		// 
		// using CPU = Indy.IL2CPU.Assembler;
		// using CPUx86 = Indy.IL2CPU.Assembler.X86;
		// 
		// namespace Indy.IL2CPU.IL.X86 {
		// 	[OpCode(OpCodeEnum.Bne_Un)]
		// 	public class Bne_Un: Op {
		// 		public readonly string TargetLabel;
		// 		public readonly string CurInstructionLabel;
		//         private string mNextLabel;
		// 	    private string mCurLabel;
		// 	    private uint mCurOffset;
		// 	    private MethodInformation mMethodInformation;
		// 		public Bne_Un(ILReader aReader, MethodInformation aMethodInfo)
		// 			: base(aReader, aMethodInfo) {
		// 			TargetLabel = GetInstructionLabel(aReader.OperandValueBranchPosition);
		// 			CurInstructionLabel = GetInstructionLabel(aReader);
		//              mMethodInformation = aMethodInfo;
		// 		    mCurOffset = aReader.Position;
		// 		    mCurLabel = IL.Op.GetInstructionLabel(aReader);
		//             mNextLabel = IL.Op.GetInstructionLabel(aReader.NextPosition);
		// 		}
		// 		public override void DoAssemble() {
		// 			if (Assembler.StackContents.Peek().IsFloat) {
		//                 EmitNotImplementedException(Assembler, GetServiceProvider(), "Bne_Un: Float support not yet implemented!", mCurLabel, mMethodInformation, mCurOffset, mNextLabel);
		//                 return;
		// 			}
		// 			int xSize = Math.Max(Assembler.StackContents.Pop().Size, Assembler.StackContents.Pop().Size);
		// 			if (xSize > 8) {
		//                 EmitNotImplementedException(Assembler, GetServiceProvider(), "Bne_Un: StackSize>8 not supported yet", mCurLabel, mMethodInformation, mCurOffset, mNextLabel);
		//                 return;
		// 			}
		// 			string BaseLabel = CurInstructionLabel + "__";
		// 			string LabelTrue = BaseLabel + "True";
		// 			string LabelFalse = BaseLabel + "False";
		// 			if (xSize > 4)
		// 			{
		//                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
		//                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.EBX };
		// 				//value2 = EBX:EAX
		//                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.ECX };
		//                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.EDX };
		// 				//value1 = EDX:ECX
		//                 new CPUx86.Xor { DestinationReg = CPUx86.Registers.EAX, SourceReg = CPUx86.Registers.ECX };
		//                 new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.NotZero, DestinationLabel = TargetLabel };
		//                 new CPUx86.Xor { DestinationReg = CPUx86.Registers.EBX, SourceReg = CPUx86.Registers.EDX };
		//                 new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.NotZero, DestinationLabel = TargetLabel };
		// 			} else
		// 			{
		//                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
		//                 new CPUx86.Compare { DestinationReg = CPUx86.Registers.EAX, SourceReg=CPUx86.Registers.ESP, SourceIsIndirect=true};
		//                 new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.Equal, DestinationLabel = LabelTrue };
		//                 new CPUx86.Jump { DestinationLabel = LabelFalse };
		// 				new CPU.Label(LabelFalse);
		//                 new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
		//                 new CPUx86.Jump { DestinationLabel = TargetLabel };
		// 				new CPU.Label(LabelTrue);
		// 				new CPUx86.Add{DestinationReg = CPUx86.Registers.ESP, SourceValue=4};
		// 			}
		// 		}
		// 	}
		// }
		#endregion Old code
	}
}
