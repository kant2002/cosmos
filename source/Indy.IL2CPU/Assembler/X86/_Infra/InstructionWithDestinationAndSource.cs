﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indy.IL2CPU.Assembler.X86 {
    public abstract class InstructionWithDestinationAndSource : InstructionWithDestination, IInstructionWithSource {
        public ElementReference SourceRef {
            get;
            set;
        }

        public Guid SourceReg {
            get;
            set;
        }

        public uint? SourceValue {
            get;
            set;
        }

        public bool SourceIsIndirect {
            get;
            set;
        }

        public int SourceDisplacement {
            get;
            set;
        }

        protected string GetSourceAsString() {
            string xDest = "";
            if ((SourceValue.HasValue || SourceRef != null) &&
                SourceIsIndirect &&
                SourceReg != Guid.Empty) {
                throw new Exception("[Scale*index+base] style addressing not supported at the moment");
            }
            if (SourceRef != null) {
                xDest = SourceRef.ToString();
            } else {
                if (SourceReg != Guid.Empty) {
                    xDest = Registers.GetRegisterName(SourceReg);
                } else {
                    xDest = "0x" + SourceValue.GetValueOrDefault().ToString("X").ToUpperInvariant();
                }
            }
            if (SourceDisplacement != 0) {
                xDest += " + " + SourceDisplacement;
            }
            if (SourceIsIndirect) {
                return "[" + xDest + "]";
            } else {
                return xDest;
            }
        }

        public override string ToString() {
            return Mnemonic + " " + this.GetDestinationAsString() + ", " + GetSourceAsString();
        }
    }
}