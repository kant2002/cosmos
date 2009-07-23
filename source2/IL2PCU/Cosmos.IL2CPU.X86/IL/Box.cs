using System;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Box)]
	public class Box: ILOpX86
	{



		#region Old code
		// using System;
		// using System.Collections.Generic;
		// using System.Linq;
		// using Indy.IL2CPU.Assembler;
		// using CPU = Indy.IL2CPU.Assembler;
		// using CPUx86 = Indy.IL2CPU.Assembler.X86;
		// using Indy.IL2CPU.Compiler;
		// 
		// namespace Indy.IL2CPU.IL.X86 {
		// 	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Box)]
		// 	public class Box: ILOpX86 {
		//         private Type mType;
		// 		private string mTypeId;
		// 
		//         public static void ScanOp(ILReader aReader, MethodInformation aMethodInfo, SortedList<string, object> aMethodData,
		//             IServiceProvider aServiceProvider)
		//         {
		//             Type xTypeRef = aReader.OperandValueType as Type;
		//             if (xTypeRef == null)
		//             {
		//                 throw new Exception("Couldn't determine Type!");
		//             }
		//             aServiceProvider.GetService<IMetaDataInfoService>().GetTypeInfo(xTypeRef);
		//             aServiceProvider.GetService<IMetaDataInfoService>().GetMethodInfo(GCImplementationRefs.AllocNewObjectRef, false);
		//         }
		// 
		// 	    public Box(ILReader aReader, MethodInformation aMethodInfo)
		// 			: base(aReader, aMethodInfo) {
		// 			mType = aReader.OperandValueType;
		// 			if (mType == null) {
		// 				throw new Exception("Couldn't determine Type!");
		// 			}
		// 				
		// 		}
		// 
		// 		public override void DoAssemble() {
		//             var xTheSize = GetService<IMetaDataInfoService>().GetFieldStorageSize(mType);
		//             mTypeId = GetService<IMetaDataInfoService>().GetTypeIdLabel(mType);
		//             uint xSize = xTheSize;
		//             if (xTheSize % 4 != 0)
		//             {
		//                 xSize += 4 - (xTheSize % 4);
		// 			}
		//             new CPUx86.Push { DestinationValue = (ObjectImpl.FieldDataOffset + xSize) };
		//             new CPUx86.Call { DestinationLabel = CPU.MethodInfoLabelGenerator.GenerateLabelName(GCImplementationRefs.AllocNewObjectRef) };
		//             new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
		//             new CPUx86.Move { DestinationReg = CPUx86.Registers.EBX, SourceRef = ElementReference.New(mTypeId), SourceIsIndirect = true };
		//             new CPUx86.Move { DestinationReg = CPUx86.Registers.EAX, DestinationIsIndirect = true, SourceReg = CPUx86.Registers.EBX };
		//             new CPUx86.Move { DestinationReg = CPUx86.Registers.EAX, DestinationIsIndirect = true, DestinationDisplacement = 4, SourceValue = (uint)InstanceTypeEnum.BoxedValueType, Size=32 };
		//             new CPU.Comment("xSize is " + xSize);
		//             for (int i = 0; i < (xSize / 4); i++)
		//             {
		//                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.EDX };
		//                 new CPUx86.Move { DestinationReg = CPUx86.Registers.EAX, DestinationIsIndirect = true, DestinationDisplacement = (ObjectImpl.FieldDataOffset + (i * 4)), SourceReg = CPUx86.Registers.EDX, Size=32 };
		//             }
		//             new CPUx86.Push { DestinationReg = CPUx86.Registers.EAX };
		//             Assembler.StackContents.Pop();
		//             Assembler.StackContents.Push(new StackContent(4, false, false, false));
		// 		} 
		// 	}
		// }
		#endregion
	}
}
