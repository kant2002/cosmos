﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Indy.IL2CPU.Plugs;

namespace Indy.IL2CPU.IL.X86.CustomImplementations.System {
	[Plug(Target=typeof(Enum))]
	public static class EnumImpl {
		[PlugMethod(Signature="System_Void___System_Enum__cctor____")]
		public static void _CCtor() {
		}
	}
}