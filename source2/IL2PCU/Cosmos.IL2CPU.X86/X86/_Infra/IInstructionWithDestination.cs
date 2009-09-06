﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.IL2CPU.X86 {
    public interface IInstructionWithDestination {
        ElementReference DestinationRef {
            get;
            set;
        }

        RegistersEnum? DestinationReg
        {
            get;
            set;
        }

        uint? DestinationValue {
            get;
            set;
        }

        bool DestinationIsIndirect {
            get;
            set;
        }

        int DestinationDisplacement {
            get;
            set;
        }
    }
}
