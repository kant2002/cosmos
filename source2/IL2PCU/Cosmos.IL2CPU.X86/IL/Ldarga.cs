using System;
using CPUx86 = Indy.IL2CPU.Assembler.X86;
using Indy.IL2CPU;
namespace Cosmos.IL2CPU.X86.IL
{
    [Cosmos.IL2CPU.OpCode( ILOpCode.Code.Ldarga )]
    public class Ldarga : ILOp
    {
        public Ldarga( Cosmos.IL2CPU.Assembler aAsmblr )
            : base( aAsmblr )
        {
        }

        public override void Execute( MethodInfo aMethod, ILOpCode aOpCode )
        {
            Cosmos.IL2CPU.ILOpCodes.OpVar xOpVar = ( Cosmos.IL2CPU.ILOpCodes.OpVar )aOpCode;
            var xMethodInfo = aMethod.MethodBase as System.Reflection.MethodInfo;
            uint xReturnSize = 0;
            if( xMethodInfo != null )
            {
                xReturnSize = Align( SizeOfType( xMethodInfo.ReturnType ), 4 );
            }
            uint xOffset = 12;
            var xCorrectedOpValValue = xOpVar.Value;
            if( !aMethod.MethodBase.IsStatic && xOpVar.Value > 0 )
            {
                // if the method has a $this, the OpCode value includes the this at index 0, but GetParameters() doesnt include the this
                xCorrectedOpValValue -= 1;
            }
            var xParams = aMethod.MethodBase.GetParameters();
            for( int i = xParams.Length - 1; i > xCorrectedOpValValue; i-- )
            {
                var xSize = Align( SizeOfType( xParams[ i ].ParameterType ), 4 );
                xOffset += xSize;
            }
            var xCurArgSize = Align( SizeOfType( xParams[ xCorrectedOpValValue ].ParameterType ), 4 );
            uint xArgSize = 0;
            foreach( var xParam in xParams )
            {
                xArgSize += Align( SizeOfType( xParam.ParameterType ), 4 );
            }
            xReturnSize = 0;
            uint xExtraSize = 0;
            if( xReturnSize > xArgSize )
            {
                xExtraSize = xArgSize - xReturnSize;
            }
            xOffset += xExtraSize;

            new CPUx86.Push { DestinationReg = CPUx86.Registers.EBP };

            for( int i = 0; i < ( xCurArgSize / 4 ); i++ )
            {
                new CPUx86.Push { DestinationValue = ( uint )( xCurArgSize - ( ( i + 1 ) * 4 ) ) };
            }
            Assembler.Stack.Push( ( int )xCurArgSize, xParams[ xCorrectedOpValValue ].ParameterType );

            //for( int i = 0; i < ( mSize / 4 ); i++ )
            //{
            //    mVirtualAddresses[ i ] = ( mOffset + ( ( i + 1 ) * 4 ) + 4 );
            //}
            //mAddress = aMethodInfo.Arguments[ aIndex ].VirtualAddresses.First();


            Assembler.Stack.Push( new StackContents.Item( 4, typeof( uint ) ) );

            //new CPUx86.Push { DestinationValue = ( uint )mAddress };
            //
            Assembler.Stack.Push( new StackContents.Item( 4, typeof( uint ) ) );

            new Add( Assembler ).Execute( aMethod, aOpCode );
        }


        // using System;
        // using System.Linq;
        // 
        // 
        // using CPU = Indy.IL2CPU.Assembler.X86;
        // using Indy.IL2CPU.Assembler;
        // 
        // namespace Indy.IL2CPU.IL.X86 {
        // 	[OpCode(OpCodeEnum.Ldarga)]
        // 	public class Ldarga: Op {
        // 		private int mAddress;
        //         private string mNextLabel;
        // 	    private string mCurLabel;
        // 	    private uint mCurOffset;
        // 	    private MethodInformation mMethodInformation;
        // 		protected void SetArgIndex(int aIndex, MethodInformation aMethodInfo) {
        // 			mAddress = aMethodInfo.Arguments[aIndex].VirtualAddresses.First();
        // 		}
        //         public Ldarga(MethodInformation aMethodInfo, int aIndex, string aCurrentLabel, uint aCurrentOffset, string aNextLabel)
        // 			: base(null, aMethodInfo) {
        // 			SetArgIndex(aIndex, aMethodInfo);
        // 
        //             mMethodInformation = aMethodInfo;
        // 		    mCurOffset = aCurrentOffset;
        // 		    mCurLabel = aCurrentLabel;
        //             mNextLabel = aNextLabel;
        // 		}
        // 
        // 		public Ldarga(ILReader aReader, MethodInformation aMethodInfo)
        // 			: base(aReader, aMethodInfo) {
        // 			if (aReader != null) {
        // 				SetArgIndex(aReader.OperandValueInt32, aMethodInfo);
        // 				//ParameterDefinition xParam = aReader.Operand as ParameterDefinition;
        // 				//if (xParam != null) {
        // 				//    SetArgIndex(xParam.Sequence - 1, aMethodInfo);
        // 				//}
        // 			}
        //             mMethodInformation = aMethodInfo;
        // 		    mCurOffset = aReader.Position;
        // 		    mCurLabel = IL.Op.GetInstructionLabel(aReader);
        //             mNextLabel = IL.Op.GetInstructionLabel(aReader.NextPosition);
        // 		}
        // 		public override void DoAssemble() {
        //             new CPU.Push { DestinationReg = CPU.Registers.EBP };
        // 			Assembler.Stack.Push(new StackContent(4, typeof(uint)));
        //             new CPU.Push { DestinationValue = (uint)mAddress };
        // 			Assembler.Stack.Push(new StackContent(4, typeof(uint)));
        // 			Add(Assembler, GetServiceProvider(), mCurLabel, mMethodInformation, mCurOffset, mNextLabel);
        // 		}
        // 	}
        // }

    }
}
