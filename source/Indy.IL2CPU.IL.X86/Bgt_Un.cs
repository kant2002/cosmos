using System;
using System.IO;


using CPU = Indy.IL2CPU.Assembler;
using CPUx86 = Indy.IL2CPU.Assembler.X86;

namespace Indy.IL2CPU.IL.X86 {
	[OpCode(OpCodeEnum.Bgt_Un)]
	public class Bgt_Un: Op {
		public readonly string TargetLabel;
		public readonly string CurInstructionLabel;
		public Bgt_Un(ILReader aReader, MethodInformation aMethodInfo)
			: base(aReader, aMethodInfo) {
			TargetLabel = GetInstructionLabel(aReader.OperandValueBranchPosition);
			CurInstructionLabel = GetInstructionLabel(aReader);
		}
		public override void DoAssemble() {
			if (Assembler.StackContents.Peek().IsFloat)
			{
				throw new Exception("Floats not yet supported!");
			}
			var rightTop = Assembler.StackContents.Pop();
			var leftBottom = Assembler.StackContents.Pop();
			int xSize = Math.Max(rightTop.Size, leftBottom.Size);
			if (xSize > 8)
			{
				throw new Exception("StackSize>8 not supported");
			}

			string BaseLabel = CurInstructionLabel + "__";
			string LabelTrue = BaseLabel + "True";
			string LabelFalse = BaseLabel + "False";

			if (xSize <= 4)
			{
				new CPUx86.Pop(CPUx86.Registers.EAX);
				new CPUx86.Compare(CPUx86.Registers.EAX, CPUx86.Registers.AtESP);
				new CPUx86.JumpIfGreater(LabelTrue);
				new CPUx86.JumpAlways(LabelFalse);
				new CPU.Label(LabelTrue);
				new CPUx86.Add(CPUx86.Registers.ESP, "4");
				new CPUx86.JumpAlways(TargetLabel);
				new CPU.Label(LabelFalse);
				new CPUx86.Add(CPUx86.Registers.ESP, "4");
				return;
			}

			if (xSize == 8)
			{
				if (rightTop.Size < 8)
					new CPUx86.Xor("ebx", "ebx");
				else
					new CPUx86.Pop("ebx");
				new CPUx86.Pop("eax");

				if (leftBottom.Size < 8)
					new CPUx86.Xor("edx", "edx");
				else
					new CPUx86.Pop("edx");
				new CPUx86.Pop("ecx");

				new CPUx86.Sub("eax", "ecx");
				new CPUx86.SubWithCarry("ebx", "edx");
				new CPUx86.JumpIfCarry(LabelTrue);
				return;
			}

			throw new NotSupportedException();
		}
	}
}