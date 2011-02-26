﻿using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace MatthijsPlayground
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
        }

        protected override void Run()
        {
            string xTest;
            DoSomething();
            xTest = "test";
            var xSB = new StringBuilder("AAAA");
            
            if (xSB[0] == 'A')
            {
                Console.WriteLine("1");
            }
            else
            {
                Console.WriteLine("2");
            }
            while (true)
                ;
        }

        private static void DoSomething()
        {
            WriteLine("Line1");
            WriteLine("Line2");
            WriteLine("Line3");
        }

        private static void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
