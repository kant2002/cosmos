﻿using System;
using System.Linq;
using Cosmos.IL2CPU.Plugs;

namespace Cosmos.IL2CPU.X86.Plugs.CustomImplementations.MS.System {
	[Plug(Target = typeof(String), IsMicrosoftdotNETOnly = true)]
	public static class StringImpl {

        public static unsafe void Ctor(string aThis, char[] aChars,
            [FieldAccess(Name = "System.Int32 System.String.m_stringLength")] ref int aStringLength,
            [FieldAccess(Name = "System.Char System.String.m_firstChar")] char* aFirstChar)
        {
            aStringLength = aChars.Length;
            for (int i = 0; i < aChars.Length; i++) {
                aFirstChar[i] = aChars[i];
            }
        }

        public static unsafe void Ctor(string aThis, char[] aChars, int start, int length,
            [FieldAccess(Name = "System.Int32 System.String.m_stringLength")] ref int aStringLength,
            [FieldAccess(Name = "System.Char System.String.m_firstChar")] char* aFirstChar)
        {
            aStringLength = length;
			for (int i = 0; i < length; i++) {
                aFirstChar[i] = aChars[start+i];
            }
        }

        ////[PlugMethod(Signature = "System_Void__System_String__ctor_System_Char____System_Int32__System_Int32_")]
        //public static unsafe void Ctor(String aThis, [FieldAccess(Name = "$$Storage$$")]ref Char[] aStorage, Char[] aChars, int aStartIndex, int aLength,
        //    [FieldAccess(Name = "System.Int32 System.String.m_stringLength")] ref int aStringLength,
        //    [FieldAccess(Name = "System.Char System.String.m_firstChar")] ref char* aFirstChar) {
        //    Char[] newChars = new Char[aLength];
        //    Array.Copy(aChars, aStartIndex, newChars, 0, aLength);
        //    aStorage = newChars;
        //    aStringLength = newChars.Length;
        //    fixed (char* xFirstChar = &aStorage[0]) {
        //        aFirstChar = xFirstChar;
        //    }
        //}

        ////[PlugMethod(Signature = "System_Void__System_String__ctor_System_Char___")]
        //public static unsafe void Ctor(String aThis, [FieldAccess(Name = "$$Storage$$")] ref Char[] aStorage, Char[] aChars,
        //    [FieldAccess(Name = "System.Int32 System.String.m_stringLength")]ref int aStringLength,
        //    [FieldAccess(Name = "System.Char System.String.m_firstChar")] ref char* aFirstChar) {
        //    aStorage = aChars;
        //    aStringLength = aChars.Length;
        //    fixed(char* xFirstChar = &aStorage[0]){
        //        aFirstChar = xFirstChar;
        //    }
        //}

		//TODO didnt work
		//public static unsafe void Ctor(String aThis, char aChar, int aLength,
		//    [FieldAccess(Name = "System.Int32 System.String.m_stringLength")] ref int aStringLength,
		//    [FieldAccess(Name = "System.Char System.String.m_firstChar")] ref char* aFirstChar)
		//{
		//    aStringLength = aLength;
		//    for (int i = 0; i < aLength; i++) {
		//        aFirstChar[i] = aChar;
		//    }
		//}

        [PlugMethod(Signature = "System_Int32__System_String_get_Length__")]
		public static unsafe int get_Length(int* aThis, [FieldAccess(Name = "System.Int32 System.String.m_stringLength")]ref int aLength) {
			return aLength;
		}

		public static unsafe char get_Chars(byte* aThis, int aIndex) {
            var xCharIdx = (char*)(aThis + 16);
            return xCharIdx[aIndex];
		}

        //public static char[] ToCharArray(string aThis, [FieldAccess(Name = "$$Storage$$")] ref char[] aChars)
        //{
        //    return aChars;
        //}

        public static string FastAllocateString(int length) {
			return new String(new char[length]);
		}

		//public static unsafe string GetStringForStringBuilder(string value, int startIndex, int length, int capacity) {
		//    string str = FastAllocateString(capacity);
		//    if (value.Length == 0x0) {
		//        str.SetLength(0x0);
		//        return str;
		//    }
		//    fixed (char* chRef = &str.m_firstChar) {
		//        fixed (char* chRef2 = &value.m_firstChar) {
		//            wstrcpy(chRef, chRef2 + startIndex, length);
		//        }
		//    }
		//    str.SetLength(length);
		//    return str;
		//    return null;
		//}

		//public static unsafe void wstrcpy(char* dmem, char* smem, int charCount) {
		//    for (int i = 0; i < charCount; i++) {
		//        dmem[i] = smem[i];
		//    }
		//}
	}

  //StringImpl2 plugs StringImpl which is also a plug????
	//[Plug(Target = typeof(Cosmos.IL2CPU.CustomImplementation.System.StringImpl))]
    //public static class StringImpl2 {
    //    //System_Int32__System_String_get_Length__
    //    [PlugMethod(Enabled=false)]
    //    public static unsafe uint GetStorage(uint* aStringPtr, [FieldAccess(Name = "$$Storage$$")]ref uint aStorage) {
    //        return aStorage;
    //    }

    //    [PlugMethod(Signature = "System_Char____Cosmos_IL2CPU_CustomImplementation_System_GetStorageArray_System_String__")]
    //    public static char[] GetStorageArray(string aThis, [FieldAccess(Name = "$$Storage$$")]ref char[] aStorage)
    //    {
    //        return aStorage;
    //    }

    //    [PlugMethod(Enabled = false)]
    //    public static void FakeMethod() {
    //        //char[] xThis = null;
    //        //CustomImplementation.System.StringImpl.Ctor(null, ref xThis, null);
    //    }
    //}
}
