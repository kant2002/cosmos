﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.IL2CPU.ILOpCodes {
  public class OpSwitch : ILOpCode {
    public readonly int[] BranchLocations;

    public OpSwitch(Code aOpCode, int[] aBranchLocations)
      : base(aOpCode) {
      BranchLocations = aBranchLocations;
    }
  }
}
