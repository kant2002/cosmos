﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indy.IL2CPU.Assembler.X86 {
	[OpCode(0xFFFFFFFF, "and")]
	public class And: InstructionWithDestinationAndSourceAndSize {
	}
}
