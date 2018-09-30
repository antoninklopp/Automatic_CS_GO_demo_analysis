using System;
using DemoInfo; 

namespace CS_GO_Analysis {
    public class PositionEvent {
        public Vector Position;
        public Team T;

        public PositionEvent(Vector position, Team t) {
            Position = position;
            T = t;
        }

        public PositionEvent(PositionEvent p) {
            Position = p.Position;
            T = p.T; 
        }
    }
}
