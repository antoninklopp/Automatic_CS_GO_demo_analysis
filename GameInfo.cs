using System;
using DemoInfo;
using System.Collections.Generic;

namespace CS_GO_Analysis {

    /// <summary>
    /// A global object meant to get all the informations from the game. 
    /// </summary>
    public class GameInfo {

        /// <summary>
        /// All the rounds from the game
        /// </summary>
        public List<Round> AllRounds;

        public GameInfo() {



        }

        public void AddRound(Round r) {
            AllRounds.Add(r); 
        }

        public TeamSetUp GetGlobalSetUp(Team teamName) {

            foreach (Round r in AllRounds) {
                r.
            }
        }

        public TeamSetUp GetGlobalSetUp(Team teamName, Round.RoundType MoneyType) {
            foreach (Round r in AllRounds) {

            }
        }
    }
}
