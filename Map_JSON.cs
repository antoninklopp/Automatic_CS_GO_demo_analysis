using System;
using System.IO;

namespace CS_GO_Analysis {
    public class Map_JSON {

        string material; 

        /// <summary>
        /// Upper left corner X position
        /// </summary>
        public int pos_x;

        /// <summary>
        /// Upper left corner Y position
        /// </summary>
        public int pos_y;

        public float scale;

        public float CTSpawn_x;
        public float CTSpawn_y;

        public float TSpawn_x;
        public float TSpawn_y;

        public float bombA_x;
        public float bombA_y;

        public float bombB_x;
        public float bombB_y;

        public Map_JSON() {

        }
    }

}
