﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.Hardware {
    public class TextScreen {
        public static int CurrentRow = 0;
        public static int CurrentChar = 0;
        protected const int VideoAddr = 0xB8000;
        //protected const byte DefaultColor = 7; //Gray
        protected const byte DefaultColor = 15; //White
        protected static bool mInitialized = false;
        protected static byte Color;

        protected static void CheckInit() {
			if (!mInitialized) {
				Color = DefaultColor;
				mInitialized = true;
			}
		}

        public static int Rows {
            get { return 24; }
        }
		
		public static int Columns {
            get { return 80; }
		}

        public static void NewLine() {
            CurrentRow += 1;
			CurrentChar = 0;
            if (CurrentRow > Rows) {
				ScrollUp();
                CurrentRow -= 1;
				CurrentChar = 0;
			}

            SetCursor();
        }

		public static unsafe void Clear() {
			CheckInit();

            byte* xScreenPtr = (byte*)VideoAddr;
            for (int i = 0; i < Columns * (Rows + 1); i++) {				
				*xScreenPtr = 0;
				xScreenPtr++;
				*xScreenPtr = Color;
                xScreenPtr++;
			}

            CurrentChar = 0;
            CurrentRow = 0;

            SetCursor();
        }

        public static void WriteChar(char aChar) {
            PutChar(CurrentRow, CurrentChar, aChar);
            CurrentChar += 1;
            if (CurrentChar == Columns) {
				NewLine();
            }

            SetCursor();
        }

		protected static unsafe void ScrollUp() {
			CheckInit();
            int Columns2 = Columns * 2;
            byte* xScreenPtr = (byte*)(VideoAddr );
            for (int i = 0; i < Columns * Rows; i++) {
				*xScreenPtr = *(xScreenPtr + Columns2);
				xScreenPtr ++;
				*xScreenPtr = *(xScreenPtr + Columns2);
                xScreenPtr++;				
			}

            xScreenPtr = (byte*)(VideoAddr + Rows * Columns * 2);	
			for (int i = 0; i < Columns; i++) {
				*xScreenPtr = 0;
				xScreenPtr ++;
				*xScreenPtr = Color;
                xScreenPtr++;
			}

            SetCursor();
		}

		public unsafe static void PutChar(int aLine, int aRow, char aChar) {
			CheckInit();
            int xScreenOffset = ((aRow + (aLine * Columns)) * 2);
            byte* xScreenPtr = (byte*)(VideoAddr + xScreenOffset);
			byte xVal = (byte)aChar;
			*xScreenPtr = xVal;
			xScreenPtr ++;
			*xScreenPtr = Color;

            SetCursor();
		}

		public static void SetColors(ConsoleColor aForeground, ConsoleColor aBackground) {
			CheckInit();
            Color = (byte)((byte)(aForeground ) | ((byte)(aBackground ) << 4));
		}

        public static void SetCursor()
        {
            CheckInit();

            // TODO:
            // Set AH = 0x02
            // Set BH = 0
            // Set DH = CurrentRow
            // Set DL = CurrentChar 
            // Call interrupt 0x10

            //Store a backup of the color so that we can make sure the cursor is white
            byte tempColor = Color;

            Color = DefaultColor;

            char position = (char)((CurrentRow * 80) + CurrentChar);

            // cursor low byte to VGA index register
            Cosmos.Kernel.CPUBus.Write8(0x3D4, 0x0F);
            Cosmos.Kernel.CPUBus.Write8(0x3D5, (byte)(position & 0xFF));
            // cursor high byte to vga index register
            Cosmos.Kernel.CPUBus.Write8(0x3D4, 0x0E);
            Cosmos.Kernel.CPUBus.Write8(0x3D5, (byte)((position >> 8) & 0xFF));

            Color = tempColor;
        }
    }
}
