﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indy.IL2CPU.Assembler.X86
{
	/// <summary>
	/// CMP DEST, SOURCE
	/// if (DEST &lt; SOURCE) jump (unsigned)
	/// </summary>
    [OpCode("jb")]
	public class JumpIfBelow : JumpBase
	{
	}
}