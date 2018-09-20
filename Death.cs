using System;
using DemoInfo; 

namespace CS_GO_Analysis {
    public class Death {

        public Vector Position;
        public Team T;

        public Death(Vector position, Team t) {
            Position = position;
            T = t;
        }
    }
}
