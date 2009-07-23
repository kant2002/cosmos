﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.IL2CPU {

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class OpCodeAttribute : Attribute {
      public readonly ILOpCode.Code OpCode;

      public OpCodeAttribute(ILOpCode.Code aOpCode) {
        OpCode = aOpCode;
      }
    }
}