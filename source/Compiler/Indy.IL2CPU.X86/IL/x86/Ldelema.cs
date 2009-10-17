//using System;
//using System.IO;


//using CPU = Indy.IL2CPU.Assembler;
//using CPUx86 = Indy.IL2CPU.Assembler.X86;
//using Indy.IL2CPU.Assembler;
//using Indy.IL2CPU.Compiler;

//namespace Indy.IL2CPU.IL.X86 {
//    [OpCode(OpCodeEnum.Ldelema)]
//    public class Ldelema: Op {
//        private Type mType;
//        public Ldelema(ILReader aReader, MethodInformation aMethodInfo)
//            : base(aReader, aMethodInfo) {
//            mType= aReader.OperandValueType;
//        }

//        public static void Assemble(CPU.Assembler aAssembler, uint aElementSize) {
//            aAssembler.StackContents.Pop();
//            aAssembler.StackContents.Pop();
//            aAssembler.StackContents.Push(new StackContent(4, typeof(uint)));
//            new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
//            new CPUx86.Move { DestinationReg = CPUx86.Registers.EDX, SourceValue = aElementSize };
//            new CPUx86.Multiply{DestinationReg=CPUx86.Registers.EDX};
//            new CPUx86.Add { DestinationReg = CPUx86.Registers.EAX, SourceValue = (uint)(ObjectImpl.FieldDataOffset + 4) };
//            new CPUx86.Pop { DestinationReg = CPUx86.Registers.EDX };
//            new CPUx86.Add { DestinationReg = CPUx86.Registers.EDX, SourceReg = CPUx86.Registers.EAX };
//            new CPUx86.Push { DestinationReg = CPUx86.Registers.EDX };
//        }

//        public override void DoAssemble() {
//            var xElementSize = GetService<IMetaDataInfoService>().SizeOfType(mType);
//            Assemble(Assembler, xElementSize);
//        }
//    }
//}