﻿using System;
using DemoInfo; 

namespace CS_GO_Analysis {
    public class Death : PositionEvent {
        public Death(PositionEvent p) : base(p) {
        }

        public Death(Vector position, Team t) : base(position, t) {
        }
    }
}
