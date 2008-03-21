﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cosmos.Build.Windows {
    class Global {
        public static void Call(string aEXEPathname, string aArgLine, string aWorkDir) {
            Call(aEXEPathname, aArgLine, aWorkDir, true, true);
        }

        public static void Call(string aEXEPathname, string aArgLine, string aWorkDir, bool aWait, bool aCapture) {
            var xStartInfo = new ProcessStartInfo();
            xStartInfo.FileName = aEXEPathname;
            xStartInfo.Arguments = aArgLine;
            xStartInfo.WorkingDirectory = aWorkDir;
            xStartInfo.CreateNoWindow = false;
            xStartInfo.UseShellExecute = !aCapture;
            xStartInfo.RedirectStandardError = aCapture;
            xStartInfo.RedirectStandardOutput = aCapture;
            var xProcess = Process.Start(xStartInfo);
            
            if (!aWait && aCapture)
            {
                // we arent gonna wait till it has finished by default. 
                // but if there was an error the app may exit quickly and we should display it
                // wait a small amount of time then check
                Thread.Sleep(500);
            }

            if (aWait || (aCapture && xProcess.HasExited)) {
                if (!xProcess.WaitForExit(60 * 1000) || xProcess.ExitCode != 0) {
                    //TODO: Fix
                    if (aCapture) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error when executing: " + xStartInfo.FileName + " " + 
                            xStartInfo.Arguments + " from directory " + xStartInfo.WorkingDirectory);
                        Console.Write(xProcess.StandardOutput.ReadToEnd());
                        Console.Write(xProcess.StandardError.ReadToEnd());
                    } else {
                        throw new Exception("Call failed");
                    }
                }
            }
        }
    }
}
