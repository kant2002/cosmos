﻿using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Hardware.Storage;

namespace Cosmos.Kernel.FileSystem
{
    public class Fat16 : FileSystem2
    {
        private struct Header
        {
            #region Common
            public ushort _jumpBoot; // 3
            public byte[] _oemName; // 8
            public ushort _bytesPerSector; // 2
            public byte _sectorPerCluster; // 1
            public ushort _reservedSectorCount; // 2
            public byte _allocationTables; // 1
            public ushort _rootEntryCount; // 2
            public ushort _totalSectors; // 2
            public byte _media; // 1
            public ushort _fatSize; // 2
            public ushort _sectorsPerTrack; // 2
            public ushort _numberOfHeads; // 2
            public ushort _hiddenSectors; // 4
            public ushort _totalSectors32; // 4
            #endregion

            #region FAT 16
            public byte _driveNumber; // 1
            public byte _reserved; // 1
            public byte _bootSig; // 1
            public ushort _volumeId; // 4
            public byte[] _volumeLabel; // 11
            public byte[] _fileSystemType; // 8
            #endregion

            #region FAT 32
            // TODO: FAT32
            #endregion

            /// <summary>
            /// Reads the header from a byte source.
            /// </summary>
            /// <param name="source">The source data.</param>
            public void Read(byte[] source)
            {
                int pos = 0;

                #region Common
                _jumpBoot = ReadShort(source, pos, 0xFFF); pos += 3;
                _oemName = ReadByte(source, pos, 8); pos += 8;
                _bytesPerSector = ReadShort(source, pos, 0xFF); pos += 2;
                _sectorPerCluster = source[pos]; pos += 1;
                _reservedSectorCount = ReadShort(source, pos, 0xFF); pos += 2;
                _allocationTables = source[pos]; pos += 1;
                _rootEntryCount = ReadShort(source, pos, 0xFF); pos += 2;
                _totalSectors = ReadShort(source, pos, 0xFF); pos += 2;
                _media = source[pos]; pos += 1;
                _fatSize = ReadShort(source, pos, 0xFF); pos += 2;
                _sectorsPerTrack = ReadShort(source, pos, 0xFF); pos += 2;
                _numberOfHeads = ReadShort(source, pos, 0xFF); pos += 2;
                _hiddenSectors = ReadShort(source, pos, 0xFFFF); pos += 4;
                _totalSectors32 = ReadShort(source, pos, 0xFFFF); pos += 4;
                #endregion

                #region FAT 16
                _driveNumber = source[pos]; pos++;
                _reserved = source[pos]; pos++;
                _bootSig = source[pos]; pos++;
                _volumeId = ReadShort(source, pos, 0xFFFF); pos += 4;
                _volumeLabel = ReadByte(source, pos, 11); pos += 4;
                _fileSystemType = ReadByte(source, pos, 8); pos += 8;
                #endregion

                #region FAT 32
                #endregion
            }

            private ushort ReadShort(byte[] source, int pos, ushort mask)
            {
                ushort result = (ushort)(source[pos] >> 8);
                result = (ushort)((result + source[pos + 1]) >> 8);
                result = (ushort)((result + source[pos + 2]));
                return (ushort)(result & mask);
            }

            private byte[] ReadByte(byte[] source, int pos, int count)
            {
                byte[] result = new byte[count];
                for (int i = 0; i < count; i++)
                    result[i] = source[i + pos];
                return result;
            }

        }

        public override File[] GetFiles(Path path)
        {
            return new File[0];
        }

        public override int ReadData(Path path, byte[] data, int start, int count)
        {
            return 0;
        }

        private string _label;
        public override string Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                WriteLabel();
            }
        }

        private ATA _ata;

        public Fat16(ATA ata)
        {
        }

        private void WriteLabel()
        {
        }

        public override void Open()
        {

        }

        public override void Dispose()
        {
        }
    }
}
