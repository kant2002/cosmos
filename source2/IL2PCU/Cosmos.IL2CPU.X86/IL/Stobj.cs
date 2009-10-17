using System;
using CPUx86 = Cosmos.IL2CPU.X86;
namespace Cosmos.IL2CPU.X86.IL
{
    [Cosmos.IL2CPU.OpCode( ILOpCode.Code.Stobj )]
    public class Stobj : ILOp
    {
        public Stobj( Cosmos.IL2CPU.Assembler aAsmblr )
            : base( aAsmblr )
        {
        }

        public override void Execute( MethodInfo aMethod, ILOpCode aOpCode )
        {
            int xFieldSize = Assembler.Stack.Pop().Size;
            Assembler.Stack.Pop();
            new CPUx86.Move { DestinationReg = CPUx86.Registers.ECX, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true, SourceDisplacement = xFieldSize };
            for( int i = 0; i < ( xFieldSize / 4 ); i++ )
            {
                new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
                new CPUx86.Move { DestinationReg = CPUx86.Registers.ECX, DestinationIsIndirect = true, DestinationDisplacement = i * 4, SourceReg = CPUx86.Registers.EAX };
            }
            switch( xFieldSize % 4 )
            {
                case 1:
                    {
                        new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
                        new CPUx86.Move { DestinationReg = CPUx86.Registers.ECX, DestinationIsIndirect = true, DestinationDisplacement = ( ( xFieldSize / 4 ) * 4 ), SourceReg = CPUx86.Registers.AL };
                        break;
                    }
                case 2:
                    {
                        new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
                        new CPUx86.Move { DestinationReg = CPUx86.Registers.ECX, DestinationIsIndirect = true, DestinationDisplacement = ( ( xFieldSize / 4 ) * 4 ), SourceReg = CPUx86.Registers.AX };
                        break;
                    }
                case 0:
                    {
                        break;
                    }
                default:
                    throw new Exception( "Remainder size " + ( xFieldSize % 4 ) + " not supported!" );
            }
            new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
        }


        // using System;
        // 
        // using CPUx86 = Cosmos.IL2CPU.X86;
        // 
        // namespace Cosmos.IL2CPU.IL.X86 {
        // 	[OpCode(OpCodeEnum.Stobj)]
        // 	public class Stobj: Op {
        // 		public Stobj(ILReader aReader, MethodInformation aMethodInfo)
        // 			: base(aReader, aMethodInfo) {
        // 		}
        // 		public override void DoAssemble() {
        // 			int xFieldSize = Assembler.Stack.Pop().Size;
        // 			Assembler.Stack.Pop();
        //             new CPUx86.Move { DestinationReg = CPUx86.Registers.ECX, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true, SourceDisplacement = xFieldSize };
        // 			for (int i = 0; i < (xFieldSize / 4); i++) {
        //                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
        //                 new CPUx86.Move { DestinationReg = CPUx86.Registers.ECX, DestinationIsIndirect = true, DestinationDisplacement = i * 4, SourceReg = CPUx86.Registers.EAX };
        // 			}
        // 			switch (xFieldSize % 4) {
        // 				case 1: {
        //                         new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
        //                         new CPUx86.Move { DestinationReg = CPUx86.Registers.ECX, DestinationIsIndirect = true, DestinationDisplacement = ((xFieldSize / 4) * 4), SourceReg = CPUx86.Registers.AL };
        // 						break;
        // 					}
        // 				case 2: {
        //                         new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
        //                         new CPUx86.Move { DestinationReg = CPUx86.Registers.ECX, DestinationIsIndirect = true, DestinationDisplacement = ((xFieldSize / 4) * 4), SourceReg = CPUx86.Registers.AX };
        // 						break;
        // 					}
        // 				case 0: {
        // 						break;
        // 					}
        // 				default:
        // 					throw new Exception("Remainder size " + (xFieldSize % 4) + " not supported!");
        // 			}
        //             new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
        // 		}
        // 	}
        // }

    }
}
