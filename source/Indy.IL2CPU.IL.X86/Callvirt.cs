using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using CPU = Indy.IL2CPU.Assembler;
using CPUx86 = Indy.IL2CPU.Assembler.X86;

namespace Indy.IL2CPU.IL.X86 {
	[OpCode(Code.Callvirt, false)]
	public class Callvirt: Op {
		private int mMethodIdentifier;
		private string mThisAddress;
		private bool mHasReturn;
		private string mNormalAddress;
		public Callvirt(Instruction aInstruction, MethodInformation aMethodInfo)
			: base(aInstruction, aMethodInfo) {
			//System.Diagnostics.Debugger.Break();
			MethodReference xMethod = aInstruction.Operand as MethodReference;
			if (xMethod == null) {
				throw new Exception("Unable to determine Method!");
			}
			MethodDefinition xMethodDef = Engine.GetDefinitionFromMethodReference(xMethod);
			if (xMethodDef.IsStatic) {
				mNormalAddress = new CPU.Label(xMethod).Name;
				mHasReturn = !xMethod.ReturnType.ReturnType.FullName.StartsWith("System.Void");
				return;
			}
			mMethodIdentifier = Engine.GetMethodIdentifier(xMethodDef);
			Engine.QueueMethodRef(VTablesImplRefs.GetMethodAddressForTypeRef);
			MethodInformation xTheMethodInfo = Engine.GetMethodInfo(xMethodDef, new CPU.Label(xMethodDef).Name, null);
			mThisAddress = xTheMethodInfo.Arguments[0].VirtualAddress;
			mHasReturn = xTheMethodInfo.IsInstanceMethod;
		}

		public override void DoAssemble() {
			if (!String.IsNullOrEmpty(mNormalAddress)) {
				Call(mNormalAddress);
			} else {
				Ldarg(Assembler, mThisAddress);
				Pop("eax");
				Pushd("[eax]");
				Call(new CPU.Label(VTablesImplRefs.GetMethodAddressForTypeRef).Name);
				Call("eax");
			}
			if (mHasReturn) {
				Pushd("eax");
			}
		}
	}
}