﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.IL2CPU.X86 {
    [OpCode("cdq")]
    public class SignExtendAX : InstructionWithSize {
        public static void InitializeEncodingData(Instruction.InstructionData aData) {
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x98 },
                AllowedSizes = InstructionSizes.Word,
                DefaultSize = InstructionSize.Word
            });
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x66, 0x98 },
                AllowedSizes = InstructionSizes.Byte,
                DefaultSize = InstructionSize.Byte
            });
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x99 },
                AllowedSizes = InstructionSizes.DWord,
                DefaultSize = InstructionSize.DWord
            });
        }

        public override void WriteText( Cosmos.IL2CPU.Assembler aAssembler, System.IO.TextWriter aOutput )
        {
            switch (Size) {
                case 32:
                    aOutput.Write("cdq");
                    return;
                case 16:
                    aOutput.Write("cwde");
                    return;
                case 8:
                    aOutput.Write("cbw");
                    return;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}