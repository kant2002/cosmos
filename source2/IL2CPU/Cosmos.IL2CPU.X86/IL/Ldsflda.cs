using System;
using CPUx86 = Cosmos.Compiler.Assembler.X86;
using Cosmos.Compiler.Assembler;
using System.Reflection;
using System.Linq;

namespace Cosmos.IL2CPU.X86.IL
{
    [Cosmos.IL2CPU.OpCode( ILOpCode.Code.Ldsflda )]
    public class Ldsflda : ILOp
    {
        public Ldsflda( Cosmos.Compiler.Assembler.Assembler aAsmblr )
            : base( aAsmblr )
        {
        }

        public override void Execute( MethodInfo aMethod, ILOpCode aOpCode )
        {
            var xOpCode = ( ILOpCodes.OpField )aOpCode;
            System.Reflection.FieldInfo xField = xOpCode.Value;
            // call cctor:
            var xCctor = (xField.DeclaringType.GetConstructors(BindingFlags.Static | BindingFlags.NonPublic) ?? new ConstructorInfo[0]).SingleOrDefault();
            if (xCctor != null)
            {
                new CPUx86.Call { DestinationLabel = MethodInfoLabelGenerator.GenerateLabelName(xCctor) };
                ILOp.EmitExceptionLogic(Assembler, aMethod, aOpCode, true, null, ".AfterCCTorExceptionCheck");
                new Label(".AfterCCTorExceptionCheck");
            }
            string xDataName =DataMember.GetStaticFieldName(xField);
            new CPUx86.Push { DestinationRef = ElementReference.New( xDataName ) };
            Assembler.Stack.Push( new StackContents.Item(4, typeof(uint) ) );
        }
    }
}