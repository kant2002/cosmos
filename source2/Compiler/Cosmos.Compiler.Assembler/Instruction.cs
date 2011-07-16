﻿using System;
using System.IO;
using System.Linq;

namespace Cosmos.Compiler.Assembler {
  public abstract class Instruction : BaseAssemblerElement {
    protected string mMnemonic;
    public string Mnemonic {
      get { return mMnemonic; }
    }

    protected int mMethodID;
    public int MethodID {
      get { return mMethodID; }
    }

    protected int mAsmMethodIdx;
    public int AsmMethodIdx {
      get { return mAsmMethodIdx; }
    }

    public override void WriteText(Assembler aAssembler, TextWriter aOutput) {
      aOutput.Write(mMnemonic);
    }

    public Instruction() : this(true) {
    }

    public Instruction(bool aAddToAssembler) {
      if (aAddToAssembler) {
        var xAsm = Assembler.CurrentInstance;
        int mMethodID = xAsm.CurrentMethodID;

        // Only emit label if its executable code.
        if (this is Label || this is Comment) {
        } else {
          new Label("." + Assembler.CurrentInstance.AsmIlIdx.ToString("X2"));
        }
        xAsm.Add(this);
      }
      var xAttribs = GetType().GetCustomAttributes(typeof(OpCodeAttribute), false);
      if (xAttribs != null && xAttribs.Length > 0) {
        var xAttrib = (OpCodeAttribute)xAttribs[0];
        mMnemonic = xAttrib.Mnemonic;
      }
    }

    public override ulong? ActualAddress {
      get {
        // TODO: for now, we dont have any data alignment
        return StartAddress;
      }
    }

    public override void UpdateAddress(Assembler aAssembler, ref ulong aAddress) {
      base.UpdateAddress(aAssembler, ref aAddress);
    }

    public override bool IsComplete(Assembler aAssembler) {
      throw new NotImplementedException("Method not implemented for instruction " + this.GetType().FullName.Substring(typeof(Instruction).Namespace.Length + 1));
    }

    public override void WriteData(Assembler aAssembler, Stream aOutput) {
      throw new NotImplementedException("Method not implemented for instruction " + this.GetType().FullName.Substring(typeof(Instruction).Namespace.Length + 1));
    }

    [Obsolete]
    public override byte[] GetData(Assembler aAssembler) {
      throw new NotImplementedException("Method not implemented for instruction " + this.GetType().FullName.Substring(typeof(Instruction).Namespace.Length + 1));
    }
  }
}