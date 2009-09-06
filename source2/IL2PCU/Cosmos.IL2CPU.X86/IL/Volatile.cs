using System;
using CPU = Cosmos.IL2CPU.X86;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Volatile)]
	public class Volatile: ILOp
	{
		public Volatile(Cosmos.IL2CPU.Assembler aAsmblr):base(aAsmblr)
		{
		}

    public override void Execute(MethodInfo aMethod, ILOpCode aOpCode) {
      //TODO: Volatile
    }

	}
}
