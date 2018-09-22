using System;
using System.Reflection;
using System.IO;
using DemoInfo;
using System.Collections.Generic;
using System.Linq;

namespace CS_GO_Analysis {
    class Program {

        public static bool VERBOSE = false; 

        public static void Main(string[] args) {
            using (var fileStream = File.OpenRead(args[0])) {
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
