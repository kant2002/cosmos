using System;
using System.Linq;

using CPU = Indy.IL2CPU.Assembler;
using CPUx86 = Indy.IL2CPU.Assembler.X86;
using Indy.IL2CPU.Assembler;

namespace Indy.IL2CPU.IL.X86 {
	[OpCode(OpCodeEnum.Ceq)]
	public class Ceq: Op {
		private readonly string NextInstructionLabel;
		private readonly string CurInstructionLabel;
		public Ceq(ILReader aReader, MethodInformation aMethodInfo)
			: base(aReader, aMethodInfo) {
			NextInstructionLabel = GetInstructionLabel(aReader.NextPosition);
			CurInstructionLabel = GetInstructionLabel(aReader);
		}
		private void Assemble4Byte() {
			Assembler.StackContents.Push(new StackContent(4, typeof(bool)));
			string BaseLabel = CurInstructionLabel + "__";
			string LabelTrue = BaseLabel + "True";
			string LabelFalse = BaseLabel + "False";
			new CPUx86.Pop(CPUx86.Registers.EAX);
			new CPUx86.Compare(CPUx86.Registers.EAX, CPUx86.Registers.AtESP);
			new CPUx86.JumpIfEquals(LabelTrue);
			new CPUx86.Jump(LabelFalse);
			new CPU.Label(LabelTrue);
			new CPUx86.Add(CPUx86.Registers.ESP, "4");
			new CPUx86.Push("01h");
			new CPUx86.Jump(NextInstructionLabel);
			new CPU.Label(LabelFalse);
			new CPUx86.Add(CPUx86.Registers.ESP, "4");
			new CPUx86.Push("00h");
			new CPUx86.Jump(NextInstructionLabel);
		}

		private void Assemble8Byte() {
			Assembler.StackContents.Push(new StackContent(4, typeof(bool)));
			string BaseLabel = CurInstructionLabel + "__";
			string LabelTrue = BaseLabel + "True";
			string LabelFalse = BaseLabel + "False";

			new CPUx86.Pop(CPUx86.Registers.EAX);
			new CPUx86.Compare(CPUx86.Registers.EAX, "[esp + 4]");

			new CPUx86.Pop(CPUx86.Registers.EAX);
			new CPUx86.JumpIfNotEquals(LabelFalse);

			new CPUx86.Xor(CPUx86.Registers.EAX, "[esp + 4]");
			new CPUx86.JumpIfNotZero(LabelFalse);

			//they are equal, eax == 0
			new CPUx86.Add(CPUx86.Registers.ESP, "8");
			new CPUx86.Add(CPUx86.Registers.EAX, "1");
			new CPUx86.Push(CPUx86.Registers.EAX);
			new CPUx86.Jump(NextInstructionLabel);

			new CPU.Label(LabelFalse);
			//eax = 0
			new CPUx86.Add(CPUx86.Registers.ESP, "8");
			new CPUx86.Xor(CPUx86.Registers.EAX, CPUx86.Registers.EAX);
			new CPUx86.Push(CPUx86.Registers.EAX);
			new CPUx86.Jump(NextInstructionLabel);
		}

		public override void DoAssemble() {
			int xSize = Math.Max(Assembler.StackContents.Pop().Size, Assembler.StackContents.Pop().Size);
			if (xSize > 8) {
				throw new Exception("StackSizes>8 not supported");
			}
			if (xSize <= 4) {
				Assemble4Byte();
				return;
			}
			if (xSize > 4) {
				Assemble8Byte();
				return;
			}
			throw new Exception("Case not handled!");
		}
	}
}