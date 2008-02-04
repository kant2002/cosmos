﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.Kernel {
    // This class provides boot configurations to be called
    // as the first line from Application code.
    // For now we just have default, but can add others in the future
    public class Boot {
        public static void Default() {
            //Init Heap first - Hardware loads devices and they need heap
			Heap.CheckInit();
            
            // This should be the ONLY reference to the specific assembly
            // Later I would like to make this auto loading, but .NET's
            // init methods are a bit more work than Delphi's. :)
            Cosmos.Hardware.PC.Global.Init();

            // Now init kernel devices and rest of kernel
            Keyboard.Initialize();
        }
    }
}
