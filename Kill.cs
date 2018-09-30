using System;
using DemoInfo;

namespace CS_GO_Analysis {
    public class Kill : PositionEvent {
        public Kill(PositionEvent p) : base(p) {
        }

        public Kill(Vector position, Team t) : base(position, t) {
        }
    }
}
