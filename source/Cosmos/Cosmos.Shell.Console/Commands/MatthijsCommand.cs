﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.Shell.Console.Commands {
	public class MatthijsCommand: CommandBase {
		public override string Name {
			get {
				return "matthijs";
			}
		}

		public override string Summary {
			get {
				return "Executes tests Matthijs is working on. DO NOT EXECUTE WITHOUT INVESTIGATING WHAT IT DOES!!";
			}
		}

		public override void Execute(string param) {
			System.Console.WriteLine("Execute Matthijs now!");
		}

		public override void Help() {
			System.Console.WriteLine(Summary);
		}
	}
}
