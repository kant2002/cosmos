﻿using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Kernel;

namespace Cosmos.Hardware {
	public class PIT: Hardware {
		public const int TicksPerSecond = 1000;
		public static void Wait(uint aMSecs) {
            for(int i = 0; i < (aMSecs/54);i++)
		    {
                InternalWait(54);
		    }
		    byte xRestWait = (byte)(aMSecs % 54);
            InternalWait(xRestWait);
		}

        private static void InternalWait(byte aMSecs) {
            mTicked = false;
            int xDivisor = aMSecs * 1193;
            IOWriteByte(0x43, 0x30);
            IOWriteByte(0x40, (byte)(xDivisor & 0xFF));
            IOWriteByte(0x40, (byte)(xDivisor >> 8));
            while(!mTicked){CPU.Halt();}
        }

	    //private static void SetInterval(ushort hz) {
        //    ushort xDivisor = (ushort)(1193180 / hz);       /* Calculate our divisor */
        //    SetDivisor(xDivisor);
        //}

        //public static void Initialize(EventHandler aTick) {
        //    mTick = aTick;
        //    //SetInterval(1); // interval 1 is slowest, mostly useful for debugging..
        //    //SetInterval(1193); // interval 1193 is aprox 1 millisecond
        //    SetInterval(1);
        //}

		//private static EventHandler mTick;
	    private static bool mTicked;

		public static void HandleInterrupt() {
			//mTick(null, null);
		    mTicked = true;
		}

        public static void PlaySound(int aFrequency)
        {
            // http://www.ifi.uio.no/~inf3150/grupper/1/pcspeaker.html
            int xFrequency = (1193180 / aFrequency);
            IOWriteByte(0x61, IOReadByte((0x61) | 3)); //Enable speaker
            IOWriteByte(0x43, 0xB6); //Init sound
            IOWriteByte(0x42, (byte)((byte)0xff & xFrequency)); //LSB
            IOWriteByte(0x42, (byte)(xFrequency >> 8)); //MSB
        }

        public static void MuteSound()
        {
            //Note; Disabling speaker doesn't work??
            IOWriteByte(0x61, (byte)(IOReadByte(0x61) & 0xFC)); //Disable speaker
            
        }
	}
}