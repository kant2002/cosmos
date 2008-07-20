using System;
using System.IO;
using CPU = Indy.IL2CPU.Assembler;
using Indy.IL2CPU.Assembler;
using CPUx86 = Indy.IL2CPU.Assembler.X86;

namespace Indy.IL2CPU.IL.X86 {
    [OpCode(OpCodeEnum.Conv_Ovf_I_Un)]
    public class Conv_Ovf_I_Un : Op {
        private readonly string NextInstructionLabel;

        public Conv_Ovf_I_Un(ILReader aReader,
                             MethodInformation aMethodInfo)
            : base(aReader,
                   aMethodInfo) {
            NextInstructionLabel = GetInstructionLabel(aReader.NextPosition);
        }

        public override void DoAssemble() {
            var xSource = Assembler.StackContents.Pop();
            switch (xSource.Size) {
                case 1:
                case 2:
                case 4: {
                    break;
                }
                case 8: {
                    new CPUx86.Pop("eax");
                    new CPUx86.Add("esp",
                                   "4");
                    new CPUx86.Push("eax");
                    //new CPUx86.Pop(CPUx86.Registers.EAX);
                    //new CPUx86.SignExtendAX(4);
                    ////all bits of EDX == sign (EAX)
                    //new CPUx86.Pop("EBX");
                    ////must be equal to EDX
                    //new CPUx86.Xor("EBX",
                    //               "EDX");
                    //new CPUx86.JumpIfZero(NextInstructionLabel);
                    ////equals
                    //new CPUx86.Interrupt(CPUx86.Interrupt.INTO);
                    break;
                }
                default:
                    throw new Exception("SourceSize " + xSource + " not supported!");
            }
            Assembler.StackContents.Push(new StackContent(4,
                                                          true,
                                                          false,
                                                          false));
        }
    }
}