﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;

namespace Indy.IL2CPU.Assembler {
	public class Label: Instruction {
		public static string GetFullName(MethodBase aMethod) {
			StringBuilder xBuilder = new StringBuilder();
			string[] xParts = aMethod.ToString().Split(' ');
			string[] xParts2 = xParts.Skip(1).ToArray();
			MethodInfo xMethodInfo = aMethod as MethodInfo;
			if (xMethodInfo != null) {
				xBuilder.Append(xMethodInfo.ReturnType.FullName);
			} else {
				ConstructorInfo xCtor = aMethod as ConstructorInfo;
				if (xCtor != null) {
					xBuilder.Append(typeof(void).FullName);
				} else {
					xBuilder.Append(xParts[0]);
				}
			}
			xBuilder.Append("  ");
			xBuilder.Append(aMethod.DeclaringType.FullName);
			xBuilder.Append(".");
			xBuilder.Append(aMethod.Name);
			xBuilder.Append("(");
			ParameterInfo[] xParams = aMethod.GetParameters();
			for (int i = 0; i < xParams.Length; i++) {
				if (xParams[i].Name == "aThis" && i == 0) {
					continue;
				}
				xBuilder.Append(xParams[i].ParameterType.FullName);
				if (i < (xParams.Length - 1)) {
					xBuilder.Append(", ");
				}
			}
			xBuilder.Append(")");
			return xBuilder.ToString();
		}
		private static MD5 mHash = MD5.Create();
		private string mName;
		public string Name {
			get {
				return mName;
			}
		}

		public static string GetLabel(object aObject) {
			Label xLabel = aObject as Label;
			if (xLabel == null)
				return "";
			return xLabel.Name;
		}

		public static string LastFullLabel {
			get;
			private set;
		}

		public Label(string aName) {
			mName = aName;
			if (!aName.StartsWith(".")) {
				LastFullLabel = aName;
			}
		}

		public override string ToString() {
			return Name + ":";
		}

		public static string GenerateLabelName(MethodBase aMethod) {
			string xResult = DataMember.FilterStringForIncorrectChars(GetFullName(aMethod));
			if (xResult.Length > 245) {
				xResult = mHash.ComputeHash(Encoding.Default.GetBytes(xResult)).Aggregate("_", (r, x) => r + x.ToString("X2"));
			}
			return xResult;
		}

		public Label(MethodBase aMethod)
			: this(GenerateLabelName(aMethod)) {
		}
	}
}
