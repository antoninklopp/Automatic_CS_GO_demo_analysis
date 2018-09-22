﻿using System;
using System.Reflection;
using System.IO;
using DemoInfo;
using System.Collections.Generic;
using System.Linq;
using CS_GO_Analysis.Get_Demos; 

namespace CS_GO_Analysis {
    class Program {

        public static bool VERBOSE = false; 

        public static void Main(string[] args) {
            string folder = args[0];
            foreach (string s in ComputerDemos.ScanListDemos(folder)) {
                using (var fileStream = File.OpenRead(s)) {
                    using (var parser = new DemoParser(fileStream)) {
                        // FragGenerator.GenerateFrags(parser);
                        GameInfo g = new GameInfo();
                        g.ParseGame(parser);
                        TeamSetUp t = g.GetGlobalSetUp("G2 Esports");
                        Console.WriteLine(t);
                    }
                }
            }
        }
    }
}
