﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.IL2CPU {
  public abstract class ILOp {
		public readonly ILOpCode OpCode;
		protected ILOp(ILOpCode aOpCode)
		{
			if (aOpCode == null)
			{
				throw new ArgumentNullException("aOpCode");
			}
			OpCode = aOpCode;
		}
  }
}
