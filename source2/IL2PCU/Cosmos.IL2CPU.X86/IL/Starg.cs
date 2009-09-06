using System;
using System.Collections.Generic;
using System.IO;
using CPU = Indy.IL2CPU.Assembler;
using CPUx86 = Indy.IL2CPU.Assembler.X86;
using System.Reflection;
using Indy.IL2CPU.Assembler;
using Indy.IL2CPU.Compiler;
using Cosmos.IL2CPU.ILOpCodes;
using Indy.IL2CPU;
namespace Cosmos.IL2CPU.X86.IL
{
    [Cosmos.IL2CPU.OpCode( ILOpCode.Code.Starg )]
    public class Starg : ILOp
    {
        public Starg( Cosmos.IL2CPU.Assembler aAsmblr )
            : base( aAsmblr )
        {
        }

        public override void Execute( MethodInfo aMethod, ILOpCode aOpCode )
        {
            //throw new NotImplementedException();
 
            OpVar xOpVar = ( OpVar )aOpCode;
            //mAddresses = aMethodInfo.Arguments[ xOpVar.Value ].VirtualAddresses;

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

            //if( mAddresses == null || mAddresses.Length == 0 )
            //{
            //    throw new Exception( "No Address Specified!" );
            //}
            //for( int i = ( mAddresses.Length - 1 ); i >= 0; i -= 1 )
            //{
            //    new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
            //    new CPUx86.Move { DestinationReg = CPUx86.Registers.EBP, DestinationIsIndirect = true, DestinationDisplacement = mAddresses[ i ], SourceReg = CPUx86.Registers.EAX };
            //}
            Assembler.Stack.Pop();
        }


        // using System;
        // 
        // 
        // using CPUx86 = Indy.IL2CPU.Assembler.X86;
        // 
        // namespace Indy.IL2CPU.IL.X86 {
        // 	[OpCode(OpCodeEnum.Starg)]
        // 	public class Starg: Op {
        // 		private int[] mAddresses;
        // 		protected void SetArgIndex(int aIndex, MethodInformation aMethodInfo) {
        // 			mAddresses = aMethodInfo.Arguments[aIndex].VirtualAddresses;
        // 
        // 		}
        // 		public Starg(ILReader aReader, MethodInformation aMethodInfo)
        // 			: base(aReader, aMethodInfo) {
        // 			SetArgIndex(aReader.OperandValueInt32, aMethodInfo);
        // 		}
        // 		public override void DoAssemble() {
        // 			if (mAddresses == null || mAddresses.Length == 0) {
        // 				throw new Exception("No Address Specified!");
        // 			}
        // 			for (int i = (mAddresses.Length - 1); i >= 0; i -= 1) {
        // 				new CPUx86.Pop{DestinationReg=CPUx86.Registers.EAX};
        //                 new CPUx86.Move { DestinationReg = CPUx86.Registers.EBP, DestinationIsIndirect = true, DestinationDisplacement = mAddresses[i], SourceReg = CPUx86.Registers.EAX };
        // 			}
        // 			Assembler.Stack.Pop();
        // 		}
        // 	}
        // }

    }
}
