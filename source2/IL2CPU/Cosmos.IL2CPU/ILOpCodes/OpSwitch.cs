﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.IL2CPU.ILOpCodes {
  public class OpSwitch : ILOpCode {
    public readonly int[] BranchLocations;

    public OpSwitch(Code aOpCode, int aPos, int aNextPos, int[] aBranchLocations, System.Reflection.ExceptionHandlingClause aCurrentExceptionHandler)
      : base(aOpCode, aPos, aNextPos, aCurrentExceptionHandler) {
      BranchLocations = aBranchLocations;
    }

    public override int NumberOfStackPops
    {
      get
      {
        switch (OpCode)
        {
          case Code.Switch:
            return 1;
          default:
            throw new NotImplementedException("OpCode '" + OpCode + "' not implemented!");
        }
      }
    }

    public override int NumberOfStackPushes
    {
      get
      {
        switch (OpCode)
        {
          case Code.Switch:
            return 0;
          default:
            throw new NotImplementedException("OpCode '" + OpCode + "' not implemented!");
        }
      }
    }
    
    protected override void DoInitStackAnalysis()
    {
      base.DoInitStackAnalysis();

      switch (OpCode)
      {
        case Code.Switch:
          StackPopTypes[0] = typeof(uint);
          break;
        default:
          break;
      }
    }
  }
}