﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cosmos.IL2CPU.ILOpCodes {
  public class OpMethod: ILOpCode {
    public MethodBase Value;
    public uint ValueUID;
    public MethodBase BaseMethod;
    public uint BaseMethodUID;

    public OpMethod(Code aOpCode, int aPos, int aNextPos, MethodBase aValue, System.Reflection.ExceptionHandlingClause aCurrentExceptionHandler)
      : base(aOpCode, aPos, aNextPos, aCurrentExceptionHandler) {
      Value = aValue;
    }

    public override int NumberOfStackPops
    {
      get
      {
        switch (OpCode)
        {
          case Code.Call:
          case Code.Callvirt:
            if (Value.IsStatic)
            {
              return Value.GetParameters().Length;
            }
            else
            {
              return Value.GetParameters().Length + 1;
            }
          case Code.Newobj:
            return Value.GetParameters().Length;
          case Code.Ldftn:
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
          case Code.Call:
          case Code.Callvirt:
            var methodInfo = Value as System.Reflection.MethodInfo;
            if (methodInfo != null && methodInfo.ReturnType!=typeof(void))
            {
              return 1;
            }
            return 0;
          case Code.Newobj:
            return 1;
          case Code.Ldftn:
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
        case Code.Call:
        case Code.Callvirt:
          var xMethodInfo = Value as System.Reflection.MethodInfo;
          if (xMethodInfo != null && xMethodInfo.ReturnType != typeof (void))
          {
            StackPushTypes[0] = xMethodInfo.ReturnType;
          }
          var xExtraOffset = 0;
          if (!Value.IsStatic)
          {
            StackPopTypes[0] = Value.DeclaringType;
            xExtraOffset++;
          }
          var xParams = Value.GetParameters();
          for (int i = 0; i < xParams.Length; i++)
          {
            StackPopTypes[i + xExtraOffset] = xParams[i].ParameterType;
          }
          break;
        case Code.Newobj:
          StackPushTypes[0] = Value.DeclaringType;
          xParams = Value.GetParameters();
          for (int i = 0; i < xParams.Length; i++)
          {
            StackPopTypes[i] = xParams[i].ParameterType;
          }
          break;
        case Code.Ldftn:
          StackPushTypes[0] = typeof (IntPtr);
          return;

        default:
          break;
      }
    }
  }
}
