﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indy.IL2CPU.Assembler {
    public interface IIfNotDefined {
        string Symbol {
            get;
            set;
        }
    }
}
