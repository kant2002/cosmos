﻿using System;
using System.Collections.Generic;
using Cosmos.IL2CPU.Plugs;
using CPUx86 = Cosmos.Compiler.Assembler.X86;

namespace Cosmos.Kernel.Plugs.Assemblers {
	public class GetCurrentESP: AssemblerMethod {
    public override void AssembleNew(object aAssembler, object aMethodInfo) {
      new CPUx86.Push {
        DestinationReg = CPUx86.Registers.ESP
      };
    }
	}
}
