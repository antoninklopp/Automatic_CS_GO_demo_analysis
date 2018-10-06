using System;
using DemoInfo;
using System.Collections.Generic; 

namespace CS_GO_Analysis {
    public class Round {

        public enum RoundType {
            ECO, // Between 0 and 1500 $ of average equipment
            LITTLE_BUY, // Between 1500 $ and 3500$
            FULL_BUY  // More than 4000 $ of equipment.
        }; 

        public int Number;

        public TeamSetUp DefenseSetUp;

        public string CTTeam;
        public string TTeam;

        /// <summary>
        /// Eco type of the round
        /// </summary>
        public RoundType type; 

        public Round() {
        }

        public List<PlayerBombSite> VerboseDefenseSetUp() {
            return DefenseSetUp.ToPlayersList(); 
        }

        /// <summary>
        /// Determine the eco type of the round
        /// </summary>
        /// <param name="PlayerBeginningRound"></param>
        public void DetermineEcoType(List<DemoInfo.Player> PlayerBeginningRound) {
            int Money = 0;

            foreach (DemoInfo.Player p in PlayerBeginningRound) {
                
            }

            float AverageMoney = Money / 5; 
            if (AverageMoney < 1500) {
                type = RoundType.ECO; 
            } else if (AverageMoney < 3500) {
                type = RoundType.LITTLE_BUY; 
            } else {
                type = RoundType.FULL_BUY; 
            }
        }

    }
}
