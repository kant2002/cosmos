//using System;
//using System.IO;


//using CPU = Indy.IL2CPU.Assembler;
//using CPUx86 = Indy.IL2CPU.Assembler.X86;

//namespace Indy.IL2CPU.IL.X86 {
//    [OpCode(OpCodeEnum.Stelem_I4)]
//    public class Stelem_I4: Op {
//        private string mNextLabel;
//        private string mCurLabel;
//        private uint mCurOffset;
//        private MethodInformation mMethodInformation;
//        public Stelem_I4(ILReader aReader, MethodInformation aMethodInfo)
//            : base(aReader, aMethodInfo) {
//             mMethodInformation = aMethodInfo;
//            mCurOffset = aReader.Position;
//            mCurLabel = IL.Op.GetInstructionLabel(aReader);
//            mNextLabel = IL.Op.GetInstructionLabel(aReader.NextPosition);
//        }

//        public override void DoAssemble() {
//            Stelem_Ref.Assemble(Assembler, 4, GetServiceProvider(), mCurLabel, mMethodInformation, mCurOffset, mNextLabel);
//        }
//    }
//}