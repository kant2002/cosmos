﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Indy.IL2CPU.Assembler;
using Indy.IL2CPU.Assembler.X86;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Instruction = Mono.Cecil.Cil.Instruction;

namespace Indy.IL2CPU {
	public static class AssemblyDefinitionsExtensions {
		public static TypeDefinition FindType(this AssemblyDefinition aAssembly, string aFullName) {
			foreach(ModuleDefinition xModule in aAssembly.Modules) {
				if(xModule.Types.Contains(aFullName)) {
					return xModule.Types[aFullName];
				}
			}
			throw new Exception("Type not found '" + aFullName + "'!");
		}
	}
	public class MethodDefinitionComparer: IComparer<MethodDefinition> {
		#region IComparer<MethodDefinition> Members
		public int Compare(MethodDefinition x, MethodDefinition y) {
			return GenerateFullName(x).CompareTo(GenerateFullName(y));
		}
		#endregion

		private static string GenerateFullName(MethodReference aDefinition) {
			StringBuilder sb = new StringBuilder();
			sb.Append(aDefinition.DeclaringType.FullName + "." + aDefinition.Name);
			sb.Append("(");
			foreach (ParameterDefinition param in aDefinition.Parameters) {
				sb.Append(param.ParameterType.FullName);
				sb.Append(",");
			}
			return sb.ToString().TrimEnd(',') + ")";
		}
	}

	public delegate void DebugLogHandler(string aMessage);

	public class Engine {
		protected static Engine mCurrent;
		protected AssemblyDefinition mCrawledAssembly;
		protected DebugLogHandler mDebugLog;
		protected OpCodeMap mMap = new OpCodeMap();
		protected Assembler.Assembler mAssembler;

		/// <summary>
		/// Contains a list of all methods. This includes methods to be processed and already processed.
		/// </summary>
		protected SortedList<MethodDefinition, bool> mMethods = new SortedList<MethodDefinition, bool>(new MethodDefinitionComparer());

		/// <summary>
		/// Compiles an assembly to CPU-specific code. The entrypoint of the assembly will be 
		/// crawled to see what is neccessary, same goes for all dependencies.
		/// </summary>
		/// <param name="aAssembly">The assembly of which to crawl the entry-point method.</param>
		/// <param name="aOpAssembly">The assembly containing the architecture-specific implementation (x86, AMD64, etc)</param>
		/// <param name="aOutput"></param>
		public void Execute(string aAssembly, string aOpAssembly, StreamWriter aOutput) {
			mCurrent = this;
			try {
				if (aOutput == null) {
					throw new ArgumentNullException("aOutput");
				}
				mCrawledAssembly = AssemblyFactory.GetAssembly(aAssembly);
				if (mCrawledAssembly.EntryPoint == null) {
					throw new NotSupportedException("Libraries are not supported!");
				}

				using (mAssembler = new Assembler.X86.Assembler(aOutput)) {
					mMap.LoadOpMapFromAssembly(aOpAssembly, mAssembler);
					IL.Op.QueueMethod += QueueMethod;
					try {
						mMethods.Add(mCrawledAssembly.EntryPoint, false);
						ProcessAllMethods();
					} finally {
						mAssembler.Flush();
						IL.Op.QueueMethod -= QueueMethod;
					}
				}
			} finally {
				mCurrent = null;
			}
		}

		private void ProcessAllMethods() {
			MethodDefinition xCurrentMethod;
			while ((xCurrentMethod = (from item in mMethods.Keys
																where !mMethods[item]
																select item).FirstOrDefault()) != null) {
				OnDebugLog("Processing method '{0}'", xCurrentMethod.DeclaringType.FullName + "." + xCurrentMethod.Name);
				// what to do if a method doesn't have a body?
				if (xCurrentMethod.HasBody) {
					new Assembler.Label(xCurrentMethod);
					foreach (Instruction xInstruction in xCurrentMethod.Body.Instructions) {
						MethodReference xMethodReference = xInstruction.Operand as MethodReference;
						if (xMethodReference != null) {
							#region add methods so that they get processed
							// TODO: find a more efficient way to get the MethodDefinition from a MethodReference
							AssemblyNameReference xAssemblyNameReference = xMethodReference.DeclaringType.Scope as AssemblyNameReference;
							if (xAssemblyNameReference != null) {
								AssemblyDefinition xReferencedMethodAssembly = mCrawledAssembly.Resolver.Resolve(xAssemblyNameReference);
								if (xReferencedMethodAssembly != null) {
									foreach (ModuleDefinition xModule in xReferencedMethodAssembly.Modules) {
										var xReferencedType = xModule.Types[xMethodReference.DeclaringType.FullName];
										if (xReferencedType != null) {
											var xMethodDef = xReferencedType.Methods.GetMethod(xMethodReference.Name, xMethodReference.Parameters);
											if (xMethodDef != null) {
												QueueMethod(xMethodDef);
											}
											break;
										}
									}
								}
							} else {
								ModuleDefinition xReferencedModule = xMethodReference.DeclaringType.Scope as ModuleDefinition;
								if (xReferencedModule != null) {
									var xReferencedType = xReferencedModule.Types[xMethodReference.DeclaringType.FullName];
									if (xReferencedType != null) {
										var xMethodDef = xReferencedType.Methods.GetMethod(xMethodReference.Name, xMethodReference.Parameters);
										if (xMethodDef != null) {
											QueueMethod(xMethodDef);
										}
									}
								} else {
									OnDebugLog("Error: Unhandled scope: " + xMethodReference.DeclaringType.Scope == null ? "**NULL**" : xMethodReference.DeclaringType.Scope.GetType().FullName);
								}
							}
							#endregion
						}
						mMap.GetOpForOpCode(xInstruction.OpCode.Code).Assemble(xInstruction);
					}
				}
				mMethods[xCurrentMethod] = true;
			}
		}

		// MtW: 
		//		Right now, we only support one engine at a time per AppDomain. This might be changed
		//		later. See for example NHibernate does this with the ICurrentSessionContext interface
		public static void QueueMethod(MethodDefinition aMethod) {
			if (mCurrent == null) {
				throw new Exception("ERROR: No Current Engine found!");
			}
			if (!mCurrent.mMethods.ContainsKey(aMethod)) {
				mCurrent.mMethods.Add(aMethod, false);
			}
		}

		public static void QueueMethod(string aAssembly, string aType, string aMethod) {
			if (mCurrent == null) {
				throw new Exception("ERROR: No Current Engine found!");
			}
			var xAssemblyDef = mCurrent.mCrawledAssembly.Resolver.Resolve(aAssembly);
			TypeDefinition xTypeDef = null;
			foreach (ModuleDefinition xModDef in xAssemblyDef.Modules) {
				if (xModDef.Types.Contains(aType)) {
					xTypeDef = xModDef.Types[aType];
					break;
				}
			}
			if (xTypeDef == null) {
				throw new Exception("Type '" + aType + "' not found in assembly '" + aAssembly + "'!");
			}
			// todo: find a way to specify one overload of a method
			int xCount = 0;
			foreach (MethodDefinition xMethodDef in xTypeDef.Methods) {
				if (xMethodDef.Name == aMethod) {
					QueueMethod(xMethodDef);
					xCount++;
				}
			}
			foreach (MethodDefinition xMethodDef in xTypeDef.Constructors) {
				if (xMethodDef.Name == aMethod) {
					QueueMethod(xMethodDef);
					xCount++;
				}
			}
			if (xCount == 0) {
				throw new Exception("Method '" + aType + "." + aMethod + "' not found in assembly '" + aAssembly + "'!");
			}
		}

		public event DebugLogHandler DebugLog {
			add {
				mDebugLog += value;
			}
			remove {
				mDebugLog -= value;
			}
		}

		private void OnDebugLog(string aMessage, params object[] args) {
			if (mDebugLog != null) {
				mDebugLog(String.Format(aMessage, args));
			}
		}
	}
}