﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.Common.Extensions {
  // This class duplicates BitConvertor. BitConvertor uses ulong though and currently does not
  // work in Cosmos. But even when it does work, this syntax is more convenient and we use
  // these conversions frequently.
  // TODO: In the future we should find a way to inline and asm these, or maybe use way to map
  // a record structure on top of a byte array for speed.
  //
  // BitConverter also uses platform specific endianness and cannot be changed.
  // Since we read from disk, network etc we must be able to specify and change endianness.
  //
  // Default methods are LittleEndian
  static public class ByteConvertor {

    static public UInt16 ToUInt16(this byte[] n, int aPos) {
      return (UInt16)(n[aPos + 1] << 8 | n[aPos]);
    }

    static public UInt32 ToUInt32(this byte[] n, int aPos) {
      return (UInt32)(n[aPos + 3] << 24 | n[aPos + 2] << 16 | n[aPos + 1] << 8 | n[aPos]);
    }

    static public string GetAsciiString(this byte[] n, int aStart, int aSize) {
      var xChars = new char[aSize];
      for (int i = aStart; i < aStart + aSize; i++) {
        xChars[i] = (char)n[i];
      }
      return new string(xChars);
    }


  }
}
