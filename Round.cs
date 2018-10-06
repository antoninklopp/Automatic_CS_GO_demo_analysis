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
        public RoundType typeCT;
        public RoundType typeT; 

        public Round() {
        }

        public List<PlayerBombSite> VerboseDefenseSetUp() {
            return DefenseSetUp.ToPlayersList(); 
        }

        /// <summary>
        /// Determine the eco type of the round
        /// </summary>
        /// <param name="PlayerBeginningRound"></param>
        public void DetermineEcoType(IEnumerable<DemoInfo.Player> PlayerBeginningRound) {
            int MoneyCT = 0;
            int MoneyT = 0; 

            foreach (DemoInfo.Player p in PlayerBeginningRound) {
                if (p.Team == Team.CounterTerrorist) {
                    MoneyCT += p.CurrentEquipmentValue;
                } else {
                    MoneyT += p.CurrentEquipmentValue;
                }
            }

            float AverageMoneyCT = MoneyCT / 5; 
            if (AverageMoneyCT < 1500) {
                typeCT = RoundType.ECO; 
            } else if (AverageMoneyCT < 3500) {
                typeCT = RoundType.LITTLE_BUY; 
            } else {
                typeCT = RoundType.FULL_BUY; 
            }

            float AverageMoneyT = MoneyT / 5;
            if (AverageMoneyT < 1500) {
                typeT = RoundType.ECO;
            }
            else if (AverageMoneyT < 3500) {
                typeT = RoundType.LITTLE_BUY;
            }
            else {
                typeT = RoundType.FULL_BUY;
            }

            Console.WriteLine("{0} buy CT   {1} buy T", typeCT, typeT); 
        }

    }
}
