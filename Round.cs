using System;
using DemoInfo;
using System.Collections.Generic; 

namespace CS_GO_Analysis {
    public class Round {

        public enum RoundType {
            ECO, // Between 0 and 1500 $ of average equipment
            LITTLE_BUY, // Between 1500 $ and 4000$
            FULL_BUY  // More than 4000 $ of equipment.
        }; 

        public int Number;

        public TeamSetUp DefenseSetUp; 

        public Round() {
        }

        public List<PlayerBombSite> VerboseDefenseSetUp() {
            return DefenseSetUp.ToPlayerList(); 
        }
    }
}
