﻿using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Build.Windows;

namespace Cosmos.Shell.Guess {
	public class Program {

		#region Build Console
		// This contains code to launch the build console. Most users should not chagne this.
		[STAThread]
		public static void Main() {
			BuildUI.Run();
		}
		#endregion

		public static void Init() {
			Cosmos.Sys.Boot.Default();

			Random mt = new Random();
			int num = mt.Next();

			System.Console.WriteLine("I am thinking of a number between 0 and 100. What is it?");
			System.Console.ForegroundColor = ConsoleColor.Blue;
			System.Console.Write("Take a guess: ");
			System.Console.ForegroundColor = ConsoleColor.White;
			short guess = short.Parse(System.Console.ReadLine());

			while (guess != num) {
				System.Console.ForegroundColor = ConsoleColor.Red;
				if (guess > num)
					System.Console.WriteLine("Too high.");
				else
					System.Console.WriteLine("Too low.");

				System.Console.ForegroundColor = ConsoleColor.Blue;
				System.Console.Write("Take another guess: ");
				System.Console.ForegroundColor = ConsoleColor.White;
				guess = short.Parse(System.Console.ReadLine());
			}

			System.Console.WriteLine("You got it!!!!");
		}
	}
}
