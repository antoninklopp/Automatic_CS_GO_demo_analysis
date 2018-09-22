using System;
using System.IO;
using System.Collections.Generic; 

namespace CS_GO_Analysis.Get_Demos {

    /// <summary>
    /// This class is meant to scan the computer from a folder and find all the demos
    /// available to scan them. 
    /// </summary>
    public class ComputerDemos {

        public static string[] ScanListDemos(string folder) {
            string[] AllDemos = Directory.GetFiles(folder, "*.dem", SearchOption.AllDirectories);
            return AllDemos;
        }

    }

}
