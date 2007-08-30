﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indy.IL2CPU.Assembler {
	public class Invoke: Instruction {
		public readonly string ProcedureName;
		public readonly object[] Params;

		public Invoke(string aProcedureName)
			: this(aProcedureName, new object[0]) {
		}

		public Invoke(string aProcedureName, object[] aParams) {
			ProcedureName = aProcedureName;
			Params = aParams;
		}

		public override string ToString() {
			string xResult = "invoke " + ProcedureName;
			foreach(object o in Params) {
				xResult += ",";
				if(o !=null) {
					xResult += o;
				}
			}
			return xResult;
		}
	}
}
