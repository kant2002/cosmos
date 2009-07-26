﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.IL2CPU.ILOpCodes {
  public class OpInt : ILOpCode {
    public readonly UInt32 Value;

    public OpInt(Code aOpCode, int aPos, UInt32 aValue)
      : base(aOpCode, aPos) {
      Value = aValue;
    }
  }
}
