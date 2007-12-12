﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.Hardware {
	public static class DebugUtil {
		public static void Initialize() {
		}

		public static void StartLogging() {
			// placeholder, later on, we will need some kind of locking
		}

		public static void EndLogging() {
			// placeholder, later on, we will need some kind of locking
		}

		public static void SendMessage(string aModule, string aData) {
			StartLogging();
			WriteSerialString("<Message Type=\"Info\" Module=\"");
			WriteSerialString(aModule);
			WriteSerialString("\" String=\"");
			WriteSerialString(aData);
			WriteSerialString("\"/>\r\n");
			EndLogging();
		}

		public static void WriteNumber(uint aNumber, byte aBits) {
			WriteNumber(aNumber, aBits, true);
		}
		public static void WriteNumber(uint aNumber, byte aBits, bool aWritePrefix) {
			uint xValue = aNumber;
			byte xCurrentBits = aBits;
			if (aWritePrefix) {
				WriteSerialString("0x");
			}
			while (xCurrentBits >= 4) {
				xCurrentBits -= 4;
				byte xCurrentDigit = (byte)((xValue >> xCurrentBits) & 0xF);
				string xDigitString = null;
				switch (xCurrentDigit) {
					case 0:
						xDigitString = "0";
						goto default;
					case 1:
						xDigitString = "1";
						goto default;
					case 2:
						xDigitString = "2";
						goto default;
					case 3:
						xDigitString = "3";
						goto default;
					case 4:
						xDigitString = "4";
						goto default;
					case 5:
						xDigitString = "5";
						goto default;
					case 6:
						xDigitString = "6";
						goto default;
					case 7:
						xDigitString = "7";
						goto default;
					case 8:
						xDigitString = "8";
						goto default;
					case 9:
						xDigitString = "9";
						goto default;
					case 10:
						xDigitString = "A";
						goto default;
					case 11:
						xDigitString = "B";
						goto default;
					case 12:
						xDigitString = "C";
						goto default;
					case 13:
						xDigitString = "D";
						goto default;
					case 14:
						xDigitString = "E";
						goto default;
					case 15:
						xDigitString = "F";
						goto default;
					default:
						WriteSerialString(xDigitString);
						break;
				}
			}
		}

		public static unsafe void LogInterruptOccurred(Interrupts.InterruptContext* aContext) {
			uint aInterrupt = aContext->Interrupt;
			StartLogging();
			WriteSerialString("<InterruptOccurred Interrupt=\"");
			WriteNumber(aContext->Interrupt, 32);
			WriteSerialString("\" SS=\"");
			WriteNumber(aContext->SS, 32);
			WriteSerialString("\" GS=\"");
			WriteNumber(aContext->GS, 32);
			WriteSerialString("\" FS=\"");
			WriteNumber(aContext->FS, 32);
			WriteSerialString("\" ES=\"");
			WriteNumber(aContext->ES, 32);
			WriteSerialString("\" DS=\"");
			WriteNumber(aContext->DS, 32);
			WriteSerialString("\" CS=\"");
			WriteNumber(aContext->CS, 32);
			WriteSerialString("\" ESI=\"");
			WriteNumber(aContext->ESI, 32);
			WriteSerialString("\" EBP=\"");
			WriteNumber(aContext->EBP, 32);
			WriteSerialString("\" ESP=\"");
			WriteNumber(aContext->ESP, 32);
			WriteSerialString("\" EBX=\"");
			WriteNumber(aContext->EBX, 32);
			WriteSerialString("\" EDX=\"");
			WriteNumber(aContext->EDX, 32);
			WriteSerialString("\" ECX=\"");
			WriteNumber(aContext->ECX, 32);
			WriteSerialString("\" EAX=\"");
			WriteNumber(aContext->EAX, 32);
			WriteSerialString("\" Param=\"");
			WriteNumber(aContext->Param, 32);
			WriteSerialString("\" EIP=\"");
			WriteNumber(aContext->EIP, 32);
			WriteSerialString("\" EFlags=\"");
			WriteNumber(aContext->EFlags, 32);
			WriteSerialString("\" UserESP=\"");
			WriteNumber(aContext->UserESP, 32);
			WriteSerialString("\"/>\r\n");
			EndLogging();
		}

		public static void WriteBinary(string aModule, string aMessage, uint aBlock, byte[] aValue) {
			StartLogging();
			WriteSerialString("<Binary Module=\"");
			WriteSerialString(aModule);
			WriteSerialString("\" Message=\"");
			WriteSerialString(aMessage);
			WriteSerialString("\" Block=\"0x");
			WriteNumber(aBlock, 24);
			WriteSerialString("\" Value=\"");
			for (int i = 0; i < aValue.Length; i++) {
				WriteNumber(aValue[i],8, false);
			}
			WriteSerialString("\"/>\r\n");
		}

		private static void WriteSerialString(string aData) {
			for (int i = 0; i < aData.Length; i++) {
				Serial.WriteSerial(0, (byte)aData[i]);
			}
		}
	}
}