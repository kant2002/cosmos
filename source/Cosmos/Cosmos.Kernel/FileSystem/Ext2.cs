﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.Kernel.FileSystem {
	public partial class Ext2: FileSystem {
		//public static unsafe byte[] ReadFileContents(byte aController, byte aDrive, string[] aPath) {
		//    ushort* xBuffer = (ushort*)Heap.MemAlloc(512);
		//    if (!Hardware.Storage.ATA.ReadDataNew(aController, aDrive, 2, xBuffer)) {
		//        Console.WriteLine("[Ext2|SuperBlock] Error while reading SuperBlock data");
		//        return null;
		//    }
		//    SuperBlock xSuperBlock = *(SuperBlock*)xBuffer;
		//    DebugUtil.SendExt2_SuperBlock("ReadFileContents", &xSuperBlock);
		//    int xBlockSize = (int)(1024 << (byte)(xSuperBlock.LogBlockSize));
		//    uint xGroupDescriptorCount = xSuperBlock.BlockCount / xSuperBlock.BlocksPerGroup;
		//    DebugUtil.SendNumber("Ext2", "GroupDescriptorCount", xGroupDescriptorCount, 32);
		//    GroupDescriptor[] xGroupDescriptors = ReadGroupDescriptorsOfBlock(aController, aDrive, xSuperBlock.FirstDataBlock + 1, xSuperBlock, xBuffer);
		//    if (xGroupDescriptors == null) {
		//        return null;
		//    }
		//    for (uint i = 0; i < xGroupDescriptors.Length; i++) {
		//        GroupDescriptor xGroupDescriptor = xGroupDescriptors[i];
		//        DebugUtil.SendExt2_GroupDescriptor("ReadFileContents", xSuperBlock.FirstDataBlock + 1, i, &xGroupDescriptor);
		//    }
		//    return null;
		//}

		private static unsafe GroupDescriptor[] ReadGroupDescriptorsOfBlock(byte aController, byte aDrive, uint aBlockGroup, SuperBlock aSuperBlock, ushort* aBuffer) {
			uint xGroupDescriptorCount = aSuperBlock.INodesCount / aSuperBlock.INodesPerGroup;
			GroupDescriptor[] xResult = new GroupDescriptor[xGroupDescriptorCount];
			GroupDescriptor* xDescriptorPtr = (GroupDescriptor*)aBuffer;
			for (int i = 0; i < xGroupDescriptorCount; i++) {
				int xATABlock = (int)((8));
				xATABlock += (i / 16);
				if ((i % 16) == 0) {
					if (!Hardware.Storage.ATA.ReadDataNew(aController, aDrive, xATABlock, aBuffer)) {
						Console.WriteLine("[Ext2|GroupDescriptors] Error while reading GroupDescriptor data");
						return null;
					}
				}
				xResult[i] = xDescriptorPtr[i % 16];
				DebugUtil.SendExt2_GroupDescriptor("ReadGroupDescriptorsOfBlock", xATABlock, i, 0, &xDescriptorPtr[i % 16]);
			}
			return xResult;
		}

		public static unsafe void PrintAllFilesAndDirectories(byte aController, byte aDrive) {
			ushort* xBuffer = (ushort*)Heap.MemAlloc(512);
			if (!Hardware.Storage.ATA.ReadDataNew(aController, aDrive, 2, xBuffer)) {
				Console.WriteLine("[Ext2|SuperBlock] Error while reading SuperBlock data");
				return;
			}
			SuperBlock xSuperBlock = *(SuperBlock*)xBuffer;
			DebugUtil.SendExt2_SuperBlock("ReadFileContents", &xSuperBlock);
			int xBlockSize = (int)(1024 << (byte)(xSuperBlock.LogBlockSize));
			uint xGroupsCount = xSuperBlock.INodesCount / xSuperBlock.INodesPerGroup;
			uint xGroupDescriptorsPerBlock = (uint)(xBlockSize / sizeof(GroupDescriptor));
			GroupDescriptor[] xGroupDescriptors = ReadGroupDescriptorsOfBlock(aController, aDrive, xSuperBlock.FirstDataBlock + 1, xSuperBlock, xBuffer);
			if (xGroupDescriptors == null) {
				return;
			}
			bool[] xUsedINodes = new bool[32];
			uint xCount = 0;
			INode* xINodeTable = (INode*)xBuffer;
			for (uint g = 0; g < xGroupDescriptors.Length; g++) {
				GroupDescriptor xGroupDescriptor = xGroupDescriptors[g];
				if (!Hardware.Storage.ATA.ReadDataNew(aController, aDrive, (int)((xGroupDescriptor.INodeBitmap) * 8), xBuffer)) {
					return;
				}
				if (!ConvertBitmapToBoolArray((uint*)xBuffer, xUsedINodes)) {
					return;
				}
				for (int i = 0; i < 32; i++) {
					if ((i % 4) == 0) {
						uint index = (uint)((i % xSuperBlock.INodesPerGroup) * sizeof(INode));
						uint offset = index % (uint)(1024 << xSuperBlock.LogBlockSize);
						if (!Hardware.Storage.ATA.ReadDataNew(aController, aDrive, (int)((xGroupDescriptor.INodeTable+(index/(uint)(1024 << xSuperBlock.LogBlockSize)))*8), xBuffer)) {
							Console.WriteLine("[Ext2] Error reading INode table entries");
							return;
						}
					}					
					if (xUsedINodes[i]) {
						DebugUtil.SendExt2_INode(g, xINodeTable[i % 4]);
						xCount++;
					}
				}
			}
			DebugUtil.SendNumber("Ext2", "Used INode count", xCount, 32);
		}

		private static unsafe bool ConvertBitmapToBoolArray(uint* aBitmap, bool[] aArray) {
			if (aArray == null || aArray.Length != 32) {
				Console.WriteLine("[ConvertBitmapToBoolArray] Incorrect Array");
				return false;
			}
			uint xValue = *aBitmap;
			for (byte b = 0; b < 32; b++) {
				uint xCheckBit = (uint)(1 << b);
				aArray[b] = (xValue & xCheckBit) != 0;
			}
			return true;
		}
	}
}
