using System;
using CPUx86 = Cosmos.Compiler.Assembler.X86;
using CPU = Cosmos.Compiler.Assembler.X86;
using Cosmos.Compiler.Assembler.X86;
using Cosmos.Compiler.Assembler;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode( ILOpCode.Code.Cgt )]
	public class Cgt : ILOp
	{
		public Cgt( Cosmos.Compiler.Assembler.Assembler aAsmblr )
			: base( aAsmblr )
		{
		}

		public override void Execute( MethodInfo aMethod, ILOpCode aOpCode )
		{
			var xStackItem = Assembler.Stack.Pop();
			if( xStackItem.Size > 8 )
			{
				//EmitNotImplementedException( Assembler, GetServiceProvider(), "Cgt: StackSizes>8 not supported", CurInstructionLabel, mMethodInfo, mCurrentOffset, NextInstructionLabel );
				throw new NotImplementedException("StackSizes>8 not supported");
				//return;
			}
			Assembler.Stack.Push( new StackContents.Item( 4, typeof( bool ) ) );
			string BaseLabel = GetLabel( aMethod, aOpCode ) + "__";
			string LabelTrue = BaseLabel + "True";
			string LabelFalse = BaseLabel + "False";
		  var xNextLabel = GetLabel(aMethod, aOpCode.NextPosition);
			if( xStackItem.Size > 4 )
			{
				if (xStackItem.IsFloat)
				{
					new CPUx86.x87.FloatLoad { DestinationReg = Registers.ESP, Size = 64, DestinationIsIndirect = true };
					new CPUx86.Add { SourceValue = 8, DestinationReg = Registers.ESP };
					new CPUx86.x87.FloatCompare { DestinationReg = Registers.ESP , DestinationIsIndirect=true};
					new CPUx86.Add { SourceValue = 8, DestinationReg = Registers.ESP };
					new CPUx86.x87.FloatDecTopPointer();
				}
				else
				{
					new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
					new CPUx86.Pop { DestinationReg = CPUx86.Registers.EDX };
					//value2: EDX:EAX
					new CPUx86.Pop { DestinationReg = CPUx86.Registers.EBX };
					new CPUx86.Pop { DestinationReg = CPUx86.Registers.ECX };
					//value1: ECX:EBX
					new CPUx86.Compare { DestinationReg = CPUx86.Registers.EBX, SourceReg = CPUx86.Registers.EAX };
					new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.GreaterThan, DestinationLabel = LabelTrue };
					new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.LessThan, DestinationLabel = LabelFalse };
					new CPUx86.Compare { DestinationReg = CPUx86.Registers.ECX, SourceReg = CPUx86.Registers.EDX };
				}
				new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.GreaterThan, DestinationLabel = LabelTrue };
				new Label(LabelFalse);
				new CPUx86.Push { DestinationValue = 0 };
				new CPUx86.Jump { DestinationLabel = xNextLabel };
				new Label( LabelTrue );
				new CPUx86.Push { DestinationValue = 1 };
			}
			else
			{
				if (xStackItem.IsFloat){
					new CPUx86.SSE.MoveSS { DestinationReg = CPUx86.Registers.XMM0, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
					new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
					new CPUx86.SSE.MoveSS { DestinationReg = CPUx86.Registers.XMM1, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
					new CPUx86.SSE.CompareSS { DestinationReg = CPUx86.Registers.XMM1, SourceReg = CPUx86.Registers.XMM0, pseudoOpcode = (byte)CPUx86.SSE.ComparePseudoOpcodes.NotLessThanOrEqualTo };
					new CPUx86.MoveD { DestinationReg = CPUx86.Registers.EBX, SourceReg = CPUx86.Registers.XMM1 };
					new CPUx86.And { DestinationReg = CPUx86.Registers.EBX, SourceValue = 1 };
					new CPUx86.Move { SourceReg = CPUx86.Registers.EBX, DestinationReg = CPUx86.Registers.ESP, DestinationIsIndirect = true };
				}

				else{
					new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
					new CPUx86.Compare { DestinationReg = CPUx86.Registers.EAX, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
					new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.LessThan, DestinationLabel = LabelTrue };
					new CPUx86.Jump { DestinationLabel = LabelFalse };
					new Label( LabelTrue );
					new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
					new CPUx86.Push { DestinationValue = 1 };
					new CPUx86.Jump { DestinationLabel = xNextLabel };
					new Label( LabelFalse );
					new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
					new CPUx86.Push { DestinationValue = 0 };
				}
			}
		}
	}
}