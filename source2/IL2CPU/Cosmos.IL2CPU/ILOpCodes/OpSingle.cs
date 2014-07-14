﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cosmos.IL2CPU.ILOpCodes {
  public class OpSingle : ILOpCode {
    public readonly Single Value;

    public OpSingle(Code aOpCode, int aPos, int aNextPos, Single aValue, System.Reflection.ExceptionHandlingClause aCurrentExceptionHandler)
      : base(aOpCode, aPos, aNextPos, aCurrentExceptionHandler) {
      Value = aValue;
    }

    public override int NumberOfStackPops
    {
      get
      {
        switch (OpCode)
        {
            case Code.Ldc_R4:
            return 0;
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
          case Code.Ldc_R4:
            return 1;
          default:
            throw new NotImplementedException("OpCode '" + OpCode + "' not implemented!");
        }
      }
    }

    protected override void DoInitStackAnalysis(MethodBase aMethod)
    {
      base.DoInitStackAnalysis(aMethod);

      switch (OpCode)
      {
        case Code.Ldc_R4:
          StackPushTypes[0] = typeof (Single);
          return;
        default:
          break;
      }
    }
  }
}
