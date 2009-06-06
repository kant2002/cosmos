﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indy.IL2CPU.Assembler.X86 {
    [OpCode("xchg")]
    public class Xchg : InstructionWithDestinationAndSourceAndSize {
        public static void InitializeEncodingData(Instruction.InstructionData aData) {
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x90 },
                AllowedSizes = InstructionSizes.DWord | InstructionSizes.Word,
                DefaultSize = InstructionSize.DWord,
                DestinationReg = Registers.EAX,
                SourceRegAny = true,
                SourceRegByte = 0,
                ReverseRegisters=false
            }); // (E)AX with reg
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x90 },
                AllowedSizes = InstructionSizes.DWord | InstructionSizes.Word,
                DefaultSize = InstructionSize.DWord,
                DestinationRegAny = true,
                DestinationRegByte=0,
                SourceReg = Registers.EAX,
                ReverseRegisters = false
            }); // (E)AX with reg
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x86, 0xC0 },
                OperandSizeByte = 0,
                DestinationRegAny = true,
                DestinationRegByte = 1,
                DestinationRegBitShiftLeft = 3,
                SourceRegAny = true,
                SourceRegByte = 1,
                DefaultSize = InstructionSize.DWord,
                ReverseRegisters=true
            }); // register with register
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x86 },
                NeedsModRMByte = true,
                OperandSizeByte = 0,
                DestinationMemory = true,
                SourceRegAny = true,
                ReverseRegisters=true,
                DefaultSize = InstructionSize.DWord
            }); // memory with reg
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x86 },
                NeedsModRMByte = true,
                OperandSizeByte = 0,
                SourceMemory = true,
                DestinationRegAny = true,
                ReverseRegisters = false,
                DefaultSize = InstructionSize.DWord
            }); // memory with reg
        }
    }
}