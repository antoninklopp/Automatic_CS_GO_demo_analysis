using System;
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

            if (args.Length != 3) {
                Console.WriteLine("Help : dotnet run folder map team");
            }
            var parentFolder = System.AppDomain.CurrentDomain.BaseDirectory.Split("\\bin\\")[0];
            Directory.SetCurrentDirectory(parentFolder);
            string folder = args[0];
            string map = args[1];
            string team = args[2];

            MultipleGames m = new MultipleGames(map, team, folder);
            m.GenerateHeatMapsPosition();
            m.GenerateKillPosition();
            m.GenerateHeatMapsPositionNoMove();
        }
    }
}
