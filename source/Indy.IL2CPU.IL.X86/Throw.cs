using System;
using System.IO;
using System.Linq;


using CPU = Indy.IL2CPU.Assembler;
using CPUx86 = Indy.IL2CPU.Assembler.X86;

namespace Indy.IL2CPU.IL.X86 {
	[OpCode(OpCodeEnum.Throw, false)]
	public class Throw: Op {
		private MethodInformation mMethodInfo;
		private int mCurrentILOffset;
		public Throw(ILReader aReader, MethodInformation aMethodInfo)
			: base(aReader, aMethodInfo) {
			mMethodInfo = aMethodInfo;
			mCurrentILOffset = (int)aReader.Position;
		}

		public static void Assemble(Assembler.Assembler aAssembler, MethodInformation aMethodInfo, int aCurrentILOffset) {
			new CPUx86.Pop("eax");
			new CPUx86.Move("[" + CPU.DataMember.GetStaticFieldName(CPU.Assembler.CurrentExceptionRef) + "]", "eax");
			Engine.QueueMethod(CPU.Assembler.CurrentExceptionOccurredRef);
			new CPUx86.Call(CPU.Label.GenerateLabelName(CPU.Assembler.CurrentExceptionOccurredRef));
			new CPUx86.Move("ecx", "3");
			Call.EmitExceptionLogic(aAssembler, aCurrentILOffset, aMethodInfo, null, false, null);
			aAssembler.StackContents.Pop();
		}
	
		public override void DoAssemble() {
			Assemble(Assembler, mMethodInfo, mCurrentILOffset);
		}
	}
}