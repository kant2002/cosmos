﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.Hardware.Network
{
    public class MACAddress : IComparable
    {
        byte[] bytes= new byte[6];

        public MACAddress(byte[] address)
        {
            bytes = (byte[])address.Clone();
        }

        public MACAddress(MACAddress m) : this(m.bytes)
        {
        }

        
        public bool IsValid()
        {
            return bytes != null && bytes.Length ==6;
        }

        public int CompareTo(object obj)
        {
            if (obj is MACAddress)
            {
                MACAddress other = (MACAddress)obj;
                int i = 0;
                i = bytes[0].CompareTo(other.bytes[0]);
                if (i != 0) return i;
                i = bytes[1].CompareTo(other.bytes[1]);
                if (i != 0) return i;
                i = bytes[2].CompareTo(other.bytes[2]);
                if (i != 0) return i;
                i = bytes[3].CompareTo(other.bytes[3]);
                if (i != 0) return i;
                i = bytes[4].CompareTo(other.bytes[4]);
                if (i != 0) return i;
                i = bytes[5].CompareTo(other.bytes[5]);
                if (i != 0) return i;

                return 0;
            }
            else
                throw new ArgumentException("obj is not a MACAddress", "obj");
        }

        public override bool Equals(object obj)
        {
            if (obj is MACAddress)
            {
                MACAddress other = (MACAddress)obj;

                return bytes[0] == other.bytes[0] &&
                    bytes[1] == other.bytes[1] &&
                    bytes[2] == other.bytes[2] &&
                    bytes[3] == other.bytes[3] &&
                    bytes[4] == other.bytes[4] &&
                    bytes[5] == other.bytes[5];
            }
            else
                throw new ArgumentException("obj is not a MACAddress", "obj");
        }        
    }
}
