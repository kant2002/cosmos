﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.IL2CPU {
  public abstract class ILOp {
    //TODO: Use System.Reflection.Emit.OpCodes where possible, but we still need these
    //enums because they are used in attributes and other places that we need
    //compile time values
    public enum Code : ushort {
      #region Values
      Nop = 0x0000,
      Break = 0x0001,
      Ldarg_0 = 0x0002,
      Ldarg_1 = 0x0003,
      Ldarg_2 = 0x0004,
      Ldarg_3 = 0x0005,
      Ldloc_0 = 0x0006,
      Ldloc_1 = 0x0007,
      Ldloc_2 = 0x0008,
      Ldloc_3 = 0x0009,
      Stloc_0 = 0x000A,
      Stloc_1 = 0x000B,
      Stloc_2 = 0x000C,
      Stloc_3 = 0x000D,
      Ldarg_S = 0x000E,
      Ldarga_S = 0x000F,
      Starg_S = 0x0010,
      Ldloc_S = 0x0011,
      Ldloca_S = 0x0012,
      Stloc_S = 0x0013,
      Ldnull = 0x0014,
      Ldc_I4_M1 = 0x0015,
      Ldc_I4_0 = 0x0016,
      Ldc_I4_1 = 0x0017,
      Ldc_I4_2 = 0x0018,
      Ldc_I4_3 = 0x0019,
      Ldc_I4_4 = 0x001A,
      Ldc_I4_5 = 0x001B,
      Ldc_I4_6 = 0x001C,
      Ldc_I4_7 = 0x001D,
      Ldc_I4_8 = 0x001E,
      Ldc_I4_S = 0x001F,
      Ldc_I4 = 0x0020,
      Ldc_I8 = 0x0021,
      Ldc_R4 = 0x0022,
      Ldc_R8 = 0x0023,
      Dup = 0x0025,
      Pop = 0x0026,
      Jmp = 0x0027,
      Call = 0x0028,
      Calli = 0x0029,
      Ret = 0x002A,
      Br_S = 0x002B,
      Brfalse_S = 0x002C,
      Brtrue_S = 0x002D,
      Beq_S = 0x002E,
      Bge_S = 0x002F,
      Bgt_S = 0x0030,
      Ble_S = 0x0031,
      Blt_S = 0x0032,
      Bne_Un_S = 0x0033,
      Bge_Un_S = 0x0034,
      Bgt_Un_S = 0x0035,
      Ble_Un_S = 0x0036,
      Blt_Un_S = 0x0037,
      Br = 0x0038,
      Brfalse = 0x0039,
      Brtrue = 0x003A,
      Beq = 0x003B,
      Bge = 0x003C,
      Bgt = 0x003D,
      Ble = 0x003E,
      Blt = 0x003F,
      Bne_Un = 0x0040,
      Bge_Un = 0x0041,
      Bgt_Un = 0x0042,
      Ble_Un = 0x0043,
      Blt_Un = 0x0044,
      Switch = 0x0045,
      Ldind_I1 = 0x0046,
      Ldind_U1 = 0x0047,
      Ldind_I2 = 0x0048,
      Ldind_U2 = 0x0049,
      Ldind_I4 = 0x004A,
      Ldind_U4 = 0x004B,
      Ldind_I8 = 0x004C,
      Ldind_I = 0x004D,
      Ldind_R4 = 0x004E,
      Ldind_R8 = 0x004F,
      Ldind_Ref = 0x0050,
      Stind_Ref = 0x0051,
      Stind_I1 = 0x0052,
      Stind_I2 = 0x0053,
      Stind_I4 = 0x0054,
      Stind_I8 = 0x0055,
      Stind_R4 = 0x0056,
      Stind_R8 = 0x0057,
      Add = 0x0058,
      Sub = 0x0059,
      Mul = 0x005A,
      Div = 0x005B,
      Div_Un = 0x005C,
      Rem = 0x005D,
      Rem_Un = 0x005E,
      And = 0x005F,
      Or = 0x0060,
      Xor = 0x0061,
      Shl = 0x0062,
      Shr = 0x0063,
      Shr_Un = 0x0064,
      Neg = 0x0065,
      Not = 0x0066,
      Conv_I1 = 0x0067,
      Conv_I2 = 0x0068,
      Conv_I4 = 0x0069,
      Conv_I8 = 0x006A,
      Conv_R4 = 0x006B,
      Conv_R8 = 0x006C,
      Conv_U4 = 0x006D,
      Conv_U8 = 0x006E,
      Callvirt = 0x006F,
      Cpobj = 0x0070,
      Ldobj = 0x0071,
      Ldstr = 0x0072,
      Newobj = 0x0073,
      Castclass = 0x0074,
      Isinst = 0x0075,
      Conv_R_Un = 0x0076,
      Unbox = 0x0079,
      Throw = 0x007A,
      Ldfld = 0x007B,
      Ldflda = 0x007C,
      Stfld = 0x007D,
      Ldsfld = 0x007E,
      Ldsflda = 0x007F,
      Stsfld = 0x0080,
      Stobj = 0x0081,
      Conv_Ovf_I1_Un = 0x0082,
      Conv_Ovf_I2_Un = 0x0083,
      Conv_Ovf_I4_Un = 0x0084,
      Conv_Ovf_I8_Un = 0x0085,
      Conv_Ovf_U1_Un = 0x0086,
      Conv_Ovf_U2_Un = 0x0087,
      Conv_Ovf_U4_Un = 0x0088,
      Conv_Ovf_U8_Un = 0x0089,
      Conv_Ovf_I_Un = 0x008A,
      Conv_Ovf_U_Un = 0x008B,
      Box = 0x008C,
      Newarr = 0x008D,
      Ldlen = 0x008E,
      Ldelema = 0x008F,
      Ldelem_I1 = 0x0090,
      Ldelem_U1 = 0x0091,
      Ldelem_I2 = 0x0092,
      Ldelem_U2 = 0x0093,
      Ldelem_I4 = 0x0094,
      Ldelem_U4 = 0x0095,
      Ldelem_I8 = 0x0096,
      Ldelem_I = 0x0097,
      Ldelem_R4 = 0x0098,
      Ldelem_R8 = 0x0099,
      Ldelem_Ref = 0x009A,
      Stelem_I = 0x009B,
      Stelem_I1 = 0x009C,
      Stelem_I2 = 0x009D,
      Stelem_I4 = 0x009E,
      Stelem_I8 = 0x009F,
      Stelem_R4 = 0x00A0,
      Stelem_R8 = 0x00A1,
      Stelem_Ref = 0x00A2,
      Ldelem = 0x00A3,
      Stelem = 0x00A4,
      Unbox_Any = 0x00A5,
      Conv_Ovf_I1 = 0x00B3,
      Conv_Ovf_U1 = 0x00B4,
      Conv_Ovf_I2 = 0x00B5,
      Conv_Ovf_U2 = 0x00B6,
      Conv_Ovf_I4 = 0x00B7,
      Conv_Ovf_U4 = 0x00B8,
      Conv_Ovf_I8 = 0x00B9,
      Conv_Ovf_U8 = 0x00BA,
      Refanyval = 0x00C2,
      Ckfinite = 0x00C3,
      Mkrefany = 0x00C6,
      Ldtoken = 0x00D0,
      Conv_U2 = 0x00D1,
      Conv_U1 = 0x00D2,
      Conv_I = 0x00D3,
      Conv_Ovf_I = 0x00D4,
      Conv_Ovf_U = 0x00D5,
      Add_Ovf = 0x00D6,
      Add_Ovf_Un = 0x00D7,
      Mul_Ovf = 0x00D8,
      Mul_Ovf_Un = 0x00D9,
      Sub_Ovf = 0x00DA,
      Sub_Ovf_Un = 0x00DB,
      Endfinally = 0x00DC,
      Leave = 0x00DD,
      Leave_S = 0x00DE,
      Stind_I = 0x00DF,
      Conv_U = 0x00E0,
      Prefix7 = 0x00F8,
      Prefix6 = 0x00F9,
      Prefix5 = 0x00FA,
      Prefix4 = 0x00FB,
      Prefix3 = 0x00FC,
      Prefix2 = 0x00FD,
      Prefix1 = 0x00FE,
      Prefixref = 0x00FF,
      Arglist = 0xFE00,
      Ceq = 0xFE01,
      Cgt = 0xFE02,
      Cgt_Un = 0xFE03,
      Clt = 0xFE04,
      Clt_Un = 0xFE05,
      Ldftn = 0xFE06,
      Ldvirtftn = 0xFE07,
      Ldarg = 0xFE09,
      Ldarga = 0xFE0A,
      Starg = 0xFE0B,
      Ldloc = 0xFE0C,
      Ldloca = 0xFE0D,
      Stloc = 0xFE0E,
      Localloc = 0xFE0F,
      Endfilter = 0xFE11,
      Unaligned = 0xFE12,
      Volatile = 0xFE13,
      Tailcall = 0xFE14,
      Initobj = 0xFE15,
      Constrained = 0xFE16,
      Cpblk = 0xFE17,
      Initblk = 0xFE18,
      Rethrow = 0xFE1A,
      Sizeof = 0xFE1C,
      Refanytype = 0xFE1D,
      Readonly = 0xFE1E
      #endregion
    }

    //TODO: Rename or chnage to constructor dependency
    public abstract void Scan(ILReader aReader, ILScanner aScanner);

    //TODO: Change this to a preinitialized array
    public static byte GetOperandSize(Code aOpCode) {
      switch (aOpCode) {
        case Code.Add:
        case Code.Add_Ovf:
        case Code.Add_Ovf_Un:
        case Code.And:
        case Code.Arglist:
        case Code.Break:
        case Code.Ceq:
        case Code.Cgt:
        case Code.Cgt_Un:
        case Code.Ckfinite:
        case Code.Clt:
        case Code.Clt_Un:
        case Code.Conv_I:
        case Code.Conv_I1:
        case Code.Conv_I2:
        case Code.Conv_I4:
        case Code.Conv_I8:
        case Code.Conv_Ovf_I:
        case Code.Conv_Ovf_I_Un:
        case Code.Conv_Ovf_I1:
        case Code.Conv_Ovf_I1_Un:
        case Code.Conv_Ovf_I2:
        case Code.Conv_Ovf_I2_Un:
        case Code.Conv_Ovf_I4:
        case Code.Conv_Ovf_I4_Un:
        case Code.Conv_Ovf_I8:
        case Code.Conv_Ovf_I8_Un:
        case Code.Conv_Ovf_U:
        case Code.Conv_Ovf_U_Un:
        case Code.Conv_Ovf_U1:
        case Code.Conv_Ovf_U1_Un:
        case Code.Conv_Ovf_U2:
        case Code.Conv_Ovf_U2_Un:
        case Code.Conv_Ovf_U4:
        case Code.Conv_Ovf_U4_Un:
        case Code.Conv_Ovf_U8:
        case Code.Conv_Ovf_U8_Un:
        case Code.Conv_R_Un:
        case Code.Conv_R4:
        case Code.Conv_R8:
        case Code.Conv_U:
        case Code.Conv_U1:
        case Code.Conv_U2:
        case Code.Conv_U4:
        case Code.Conv_U8:
        case Code.Cpblk:
        case Code.Div:
        case Code.Div_Un:
        case Code.Dup:
        case Code.Endfilter:
        case Code.Endfinally:
        case Code.Initblk:
        case Code.Ldarg_0:
        case Code.Ldarg_1:
        case Code.Ldarg_2:
        case Code.Ldarg_3:
        case Code.Ldc_I4_0:
        case Code.Ldc_I4_1:
        case Code.Ldc_I4_2:
        case Code.Ldc_I4_3:
        case Code.Ldc_I4_4:
        case Code.Ldc_I4_5:
        case Code.Ldc_I4_6:
        case Code.Ldc_I4_7:
        case Code.Ldc_I4_8:
        case Code.Ldc_I4_M1:
        case Code.Ldelem_I:
        case Code.Ldelem_I1:
        case Code.Ldelem_I2:
        case Code.Ldelem_I4:
        case Code.Ldelem_I8:
        case Code.Ldelem_R4:
        case Code.Ldelem_R8:
        case Code.Ldelem_Ref:
        case Code.Ldelem_U1:
        case Code.Ldelem_U2:
        case Code.Ldelem_U4:
        case Code.Ldind_I:
        case Code.Ldind_I1:
        case Code.Ldind_I2:
        case Code.Ldind_I4:
        case Code.Ldind_I8:
        case Code.Ldind_R4:
        case Code.Ldind_R8:
        case Code.Ldind_Ref:
        case Code.Ldind_U1:
        case Code.Ldind_U2:
        case Code.Ldind_U4:
        case Code.Ldlen:
        case Code.Ldloc_0:
        case Code.Ldloc_1:
        case Code.Ldloc_2:
        case Code.Ldloc_3:
        case Code.Ldnull:
        case Code.Localloc:
        case Code.Mul:
        case Code.Mul_Ovf:
        case Code.Mul_Ovf_Un:
        case Code.Neg:
        case Code.Nop:
        case Code.Not:
        case Code.Or:
        case Code.Pop:
        case Code.Prefix1:
        case Code.Prefix2:
        case Code.Prefix3:
        case Code.Prefix4:
        case Code.Prefix5:
        case Code.Prefix6:
        case Code.Prefix7:
        case Code.Prefixref:
        case Code.Readonly:
        case Code.Refanytype:
        case Code.Rem:
        case Code.Rem_Un:
        case Code.Ret:
        case Code.Rethrow:
        case Code.Shl:
        case Code.Shr:
        case Code.Shr_Un:
        case Code.Stelem_I:
        case Code.Stelem_I1:
        case Code.Stelem_I2:
        case Code.Stelem_I4:
        case Code.Stelem_I8:
        case Code.Stelem_R4:
        case Code.Stelem_R8:
        case Code.Stelem_Ref:
        case Code.Stind_I:
        case Code.Stind_I1:
        case Code.Stind_I2:
        case Code.Stind_I4:
        case Code.Stind_I8:
        case Code.Stind_R4:
        case Code.Stind_R8:
        case Code.Stind_Ref:
        case Code.Stloc_0:
        case Code.Stloc_1:
        case Code.Stloc_2:
        case Code.Stloc_3:
        case Code.Sub:
        case Code.Sub_Ovf:
        case Code.Sub_Ovf_Un:
        case Code.Switch:
        case Code.Tailcall:
        case Code.Throw:
        case Code.Volatile:
        case Code.Xor:
          return 0;
        case Code.Beq_S:
        case Code.Bge_S:
        case Code.Bge_Un_S:
        case Code.Bgt_S:
        case Code.Bgt_Un_S:
        case Code.Ble_S:
        case Code.Ble_Un_S:
        case Code.Blt_S:
        case Code.Blt_Un_S:
        case Code.Bne_Un_S:
        case Code.Br_S:
        case Code.Brfalse_S:
        case Code.Brtrue_S:
        case Code.Ldarg_S:
        case Code.Ldarga_S:
        case Code.Ldc_I4_S:
        case Code.Ldloc_S:
        case Code.Ldloca_S:
        case Code.Leave_S:
        case Code.Starg_S:
        case Code.Stloc_S:
        case Code.Unaligned:
          return 1;
        case Code.Ldarg:
        case Code.Ldarga:
        case Code.Ldloc:
        case Code.Ldloca:
        case Code.Starg:
        case Code.Stloc:
          return 2;
        case Code.Beq:
        case Code.Bge:
        case Code.Bge_Un:
        case Code.Bgt:
        case Code.Bgt_Un:
        case Code.Ble:
        case Code.Ble_Un:
        case Code.Blt:
        case Code.Blt_Un:
        case Code.Bne_Un:
        case Code.Box:
        case Code.Br:
        case Code.Brfalse:
        case Code.Brtrue:
        case Code.Call:
        case Code.Calli:
        case Code.Callvirt:
        case Code.Castclass:
        case Code.Constrained:
        case Code.Cpobj:
        case Code.Initobj:
        case Code.Isinst:
        case Code.Jmp:
        case Code.Ldc_I4:
        case Code.Ldc_R4:
        case Code.Ldelem:
        case Code.Ldelema:
        case Code.Ldfld:
        case Code.Ldflda:
        case Code.Ldftn:
        case Code.Ldobj:
        case Code.Ldsfld:
        case Code.Ldsflda:
        case Code.Ldstr:
        case Code.Ldtoken:
        case Code.Ldvirtftn:
        case Code.Leave:
        case Code.Mkrefany:
        case Code.Newarr:
        case Code.Newobj:
        case Code.Refanyval:
        case Code.Sizeof:
        case Code.Stelem:
        case Code.Stfld:
        case Code.Stobj:
        case Code.Stsfld:
        case Code.Unbox:
        case Code.Unbox_Any:
          return 4;
        case Code.Ldc_I8:
        case Code.Ldc_R8:
          return 8;
        default:
          throw new NotSupportedException("OpCode not supported: " + aOpCode.ToString());
      }
    }

    //TODO: Change to a preinitialized array
    public static long? GetShortcutOperand(Code aOpCode) {
      switch (aOpCode) {
        case Code.Ldarg_0:
          return 0;
        case Code.Ldarg_1:
          return 1;
        case Code.Ldarg_2:
          return 2;
        case Code.Ldarg_3:
          return 3;
        case Code.Ldc_I4_0:
          return 0;
        case Code.Ldc_I4_1:
          return 1;
        case Code.Ldc_I4_2:
          return 2;
        case Code.Ldc_I4_3:
          return 3;
        case Code.Ldc_I4_4:
          return 4;
        case Code.Ldc_I4_5:
          return 5;
        case Code.Ldc_I4_6:
          return 6;
        case Code.Ldc_I4_7:
          return 7;
        case Code.Ldc_I4_8:
          return 8;
        case Code.Ldc_I4_M1:
          return -1;
        case Code.Ldloc_0:
          return 0;
        case Code.Ldloc_1:
          return 1;
        case Code.Ldloc_2:
          return 2;
        case Code.Ldloc_3:
          return 3;
        case Code.Stloc_0:
          return 0;
        case Code.Stloc_1:
          return 1;
        case Code.Stloc_2:
          return 2;
        case Code.Stloc_3:
          return 3;
        default:
          return null;
      }
    }
    
    //TODO: Change to preinitialized array.  Only need a small array.
    public static Code ExpandShortcut(Code aOpCode) {
          switch (aOpCode) {
              case Code.Beq_S:
                  return Code.Beq;
              case Code.Bge_S:
                  return Code.Bge;
              case Code.Bge_Un_S:
                  return Code.Bge_Un;
              case Code.Bgt_S:
                  return Code.Bgt;
              case Code.Bgt_Un_S:
                  return Code.Bgt_Un;
              case Code.Ble_S:
                  return Code.Ble;
              case Code.Ble_Un_S:
                  return Code.Ble_Un;
              case Code.Blt_S:
                  return Code.Blt;
              case Code.Blt_Un_S:
                  return Code.Blt_Un;
              case Code.Bne_Un_S:
                  return Code.Bne_Un;
              case Code.Br_S:
                  return Code.Br;
              case Code.Brfalse_S:
                  return Code.Brfalse;
              case Code.Brtrue_S:
                  return Code.Brtrue;
              case Code.Ldarg_0:
                  return Code.Ldarg;
              case Code.Ldarg_1:
                  return Code.Ldarg;
              case Code.Ldarg_2:
                  return Code.Ldarg;
              case Code.Ldarg_3:
                  return Code.Ldarg;
              case Code.Ldarg_S:
                  return Code.Ldarg;
              case Code.Ldarga_S:
                  return Code.Ldarga;
              case Code.Ldc_I4_0:
                  return Code.Ldc_I4;
              case Code.Ldc_I4_1:
                  return Code.Ldc_I4;
              case Code.Ldc_I4_2:
                  return Code.Ldc_I4;
              case Code.Ldc_I4_3:
                  return Code.Ldc_I4;
              case Code.Ldc_I4_4:
                  return Code.Ldc_I4;
              case Code.Ldc_I4_5:
                  return Code.Ldc_I4;
              case Code.Ldc_I4_6:
                  return Code.Ldc_I4;
              case Code.Ldc_I4_7:
                  return Code.Ldc_I4;
              case Code.Ldc_I4_8:
                  return Code.Ldc_I4;
              case Code.Ldc_I4_M1:
                  return Code.Ldc_I4;
              case Code.Ldc_I4_S:
                  return Code.Ldc_I4;
              case Code.Ldloc_0:
                  return Code.Ldloc;
              case Code.Ldloc_1:
                  return Code.Ldloc;
              case Code.Ldloc_2:
                  return Code.Ldloc;
              case Code.Ldloc_3:
                  return Code.Ldloc;
              case Code.Ldloc_S:
                  return Code.Ldloc;
              case Code.Ldloca_S:
                  return Code.Ldloca;
              case Code.Leave_S:
                  return Code.Leave;
              case Code.Starg_S:
                  return Code.Starg;
              case Code.Stloc_0:
                  return Code.Stloc;
              case Code.Stloc_1:
                  return Code.Stloc;
              case Code.Stloc_2:
                  return Code.Stloc;
              case Code.Stloc_3:
                  return Code.Stloc;
              case Code.Stloc_S:
                  return Code.Stloc;
              default:
                  return aOpCode;
          }
      }

  }
}
