﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware2;

namespace Cosmos.Core {
    static public class Global {
        static readonly public BaseIOGroups BaseIOGroups = new BaseIOGroups();
        static readonly public Cosmos.Debug.Kernel.Debugger Dbg = new Cosmos.Debug.Kernel.Debugger("Core", "");

        static public void Init() {
            // Temp
            //Init Heap first - Hardware loads devices and they need heap
            Console.WriteLine("    Init Heap");
            Kernel.Heap.Init();

            Kernel.CPU.CreateGDT();
            PIC.Init();
            Kernel.CPU.CreateIDT(true);
            Kernel.CPU.InitFloat();
            // End Temp
            IRQs.Dummy();
        }
    }
}
