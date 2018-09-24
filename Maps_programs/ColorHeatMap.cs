using System;
using System.Drawing; 

namespace CS_GO_Analysis.Maps {
    public class ColorHeatMap {

        public static Color GetColorHeatMap(int currentInt, int maxInt) {
            // Console.WriteLine("{0} {1}", currentInt, maxInt); 
            if (currentInt == 0) {
                return Color.FromArgb(0, 0, 0, 0); 
            }
            Color c = Color.FromArgb(120, 0, Math.Min(255, (int)((float)currentInt * 255 * 15 / maxInt)),
               Math.Min(255 - (int)((float)currentInt * 255 * 15 / maxInt), 255));
            return c; 
        }

    }
}
