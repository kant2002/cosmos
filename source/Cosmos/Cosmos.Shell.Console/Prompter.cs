﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.Shell.Console {
	/// <summary>
	/// The demonstration prompter.
	/// </summary>
        //private List<Commands.CommandBase> _commands;

        //private bool running = true;

        //public void Stop() {
        //    running = false;
        //}

        //public override void Initialize() {
        //    _commands = new List<Cosmos.Shell.Console.Commands.CommandBase>();
        //    _commands.Add(new Commands.BreakCommand());
        //    _commands.Add(new Commands.ClsCommand());
        //    _commands.Add(new Commands.DirCommand());
        //    _commands.Add(new Commands.EchoCommand());
        //    _commands.Add(new Commands.ExitCommand(Stop));
        //    _commands.Add(new Commands.FailCommand());
        //    _commands.Add(new Commands.HelpCommand(_commands));
        //    _commands.Add(new Commands.TimeCommand());
        //    _commands.Add(new Commands.TypeCommand());
        //    _commands.Add(new Commands.VersionCommand());
        //    _commands.Add(new Commands.LspciCommand());
        //    _commands.Add(new Commands.MountCommand());

        //    while (running) {
        //        System.Console.Write("Running = ");
        //        System.Console.Write(running.ToString());
        //        System.Console.Write(" ");
        //        System.Console.Write("/> ");
        //        string line = System.Console.ReadLine();
        //        if (string.IsNullOrEmpty(line)) { continue; }
        //        int index = line.IndexOf(' ');
        //        string command;
        //        string param;
        //        if (index == -1) {
        //            command = line;
        //            param = "";
        //        } else {
        //            command = line.Substring(0, index);
        //            param = line.Substring(index + 1);
        //        }

        //        bool found = false;
        //        for (int i = 0; i < _commands.Count; i++) {
        //            if (_commands[i].Name == command) {
        //                found = true;
        //                _commands[i].Execute(param);
        //                break;
        //            }
        //        }

        //        if (!found) {
        //            System.Console.ForegroundColor = ConsoleColor.Red;
        //            System.Console.Write("The command ");
        //            System.Console.Write(command);
        //            System.Console.WriteLine(" is not supported. Please type help for more information.");
        //            System.Console.ForegroundColor = ConsoleColor.White;
        //            System.Console.WriteLine();
        //        }
        //    }
        //}

	//}
}
