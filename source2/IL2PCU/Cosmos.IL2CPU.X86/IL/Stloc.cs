using System;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Stloc)]
	public class Stloc: ILOp
	{
		public Stloc(ILOpCode aOpCode):base(aOpCode)
		{
		}

		#region Old code
		// using System;
		// using System.Linq;
		// using Indy.IL2CPU.Assembler;
		// 
		// 
		// using CPUx86 = Indy.IL2CPU.Assembler.X86;
		// 
		// namespace Indy.IL2CPU.IL.X86 {
		// 	[OpCode(OpCodeEnum.Stloc)]
		// 	public class Stloc: Op {
		// 		private bool mNeedsGC = false;
		// 		private MethodInformation.Variable mLocal;
		// 		private string mBaseLabel;
		// 
		// 		protected void SetLocalIndex(int aIndex, MethodInformation aMethodInfo) {
		// 			mLocal = aMethodInfo.Locals[aIndex];
		// 			mNeedsGC = aMethodInfo.Locals[aIndex].IsReferenceType;
		// 			mNeedsGC &= aMethodInfo.Locals[aIndex].VariableType.FullName != "System.String";
		// 		}
		// 
		// 		public Stloc(ILReader aReader, MethodInformation aMethodInfo)
		// 			: base(aReader, aMethodInfo) {
		// 			int xLocalIndex;
		// 			mBaseLabel = GetInstructionLabel(aReader);
		// 			xLocalIndex = aReader.OperandValueInt32;
		// 			SetLocalIndex(xLocalIndex, aMethodInfo);
		// 			//VariableDefinition xVarDef = aReader.Operand as VariableDefinition;
		// 			//if (xVarDef != null) {
		// 			//    SetLocalIndex(xVarDef.Index, aMethodInfo);
		// 			//}
		// 		}
		// 
		// 		public sealed override void DoAssemble() {
		// 			if (mNeedsGC) {
		//                 new CPUx86.Push { DestinationReg = CPUx86.Registers.EBP, DestinationIsIndirect = true, DestinationDisplacement = mLocal.VirtualAddresses[0] };
		//                 new CPUx86.Call { DestinationLabel = MethodInfoLabelGenerator.GenerateLabelName(GCImplementationRefs.DecRefCountRef) };
		// 			}
		// 			foreach (int i in mLocal.VirtualAddresses.Reverse()) {
		//                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX }; ;
		//                 new CPUx86.Move { DestinationReg = CPUx86.Registers.EBP, DestinationIsIndirect = true, DestinationDisplacement = i, SourceReg = CPUx86.Registers.EAX };
		// 			}
		// 			// no need to inc again, items on the transient stack are also counted
		// 			Assembler.StackContents.Pop();
		// 		}
		// 	}
		// }
		#endregion Old code
	}
}
