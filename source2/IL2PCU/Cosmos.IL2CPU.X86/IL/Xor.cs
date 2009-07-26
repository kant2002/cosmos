using System;
using CPUx86 = Indy.IL2CPU.Assembler.X86;
using Indy.IL2CPU.Assembler;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Xor)]
	public class Xor: ILOp
	{
		public Xor(ILOpCode aOpCode):base(aOpCode)
		{
		}

    public override void  Execute(uint aMethodUID) {
      if (Asmblr.StackContents.Peek().IsFloat) {
        throw new Exception("Floats not supported");
      }
      int xSize = Math.Max(Asmblr.StackContents.Pop().Size, Asmblr.StackContents.Pop().Size);
      new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
      new CPUx86.Pop { DestinationReg = CPUx86.Registers.EDX };
      new CPUx86.Xor { DestinationReg = CPUx86.Registers.EAX, SourceReg = CPUx86.Registers.EDX };
      new CPUx86.Push { DestinationReg = CPUx86.Registers.EAX };
      Asmblr.StackContents.Push(new StackContent(xSize));
    }

	}
}
