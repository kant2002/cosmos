﻿using System;
using System.Runtime.InteropServices;

namespace Cosmos.Kernel.Boot.Glue {
	[StructLayout(LayoutKind.Sequential)]
	public struct IDTEntryStruct {
		public enum FlagsEnum: byte {
			NotPresent = 0xE,
			Present = 0x5E
		}
		public ushort BaseLow;
		public ushort Sel;
		public byte AlwaysZero;
		public byte Flags;
		public ushort BaseHi;
	}
}
