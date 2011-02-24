﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.Hardware.BlockDevice {
  public class Partition : BlockDevice {
    BlockDevice mHost;
    //TODO:UInt64
    UInt32 mStartingSector;

    //TODO:UInt64 x 2
    public Partition(BlockDevice aHost, UInt32 aStartingSector, UInt32 aSectorCount) {
      mHost = aHost;
      mStartingSector = aStartingSector;
      mBlockCount = aSectorCount;
    }

    public override void ReadBlock(UInt32 aBlockNo, byte[] aData) {
      // TODO:UInt64
      UInt32 xHostBlockNo = mStartingSector + aBlockNo;
      CheckBlockNo(xHostBlockNo);
      mHost.ReadBlock(xHostBlockNo, aData);
    }

    public override void WriteBlock(UInt32 aBlockNo, byte[] aData) {
      // TODO:UInt64
      UInt32 xHostBlockNo = mStartingSector + aBlockNo;
      CheckBlockNo(xHostBlockNo);
      mHost.WriteBlock(xHostBlockNo, aData);
    }

  }
}
