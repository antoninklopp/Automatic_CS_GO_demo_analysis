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

        /// <summary>
        /// Get all the setup used during the games. 
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public Dictionary<SetUp, int> GetAllSetUp(Team teamName) {
            Dictionary<SetUp, int> AllSetUps = new Dictionary<SetUp, int>(); 
            foreach (Round r in AllRounds) {
                if (r.CTTeam == teamName) {
                    SetUp A = r.DefenseSetUp.A;
                    SetUp B = r.DefenseSetUp.B; 
                    if (AllSetUps.ContainsKey(A)) {
                        AllSetUps[A]++; 
                    } else {
                        AllSetUps.Add(A, 1); 
                    }

                    if (AllSetUps.ContainsKey(B)) {
                        AllSetUps[B]++;
                    }
                    else {
                        AllSetUps.Add(B, 1);
                    }
                }
            }
            return AllSetUps; 
        }
        
        /// <summary>
        /// Get the global SetUp for the game. 
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public TeamSetUp GetGlobalSetUp(Team teamName) {
            Dictionary<SetUp, int> AllSetUps = GetAllSetUp(teamName);
            SetUp MaxASetUp = new SetUp();
            SetUp MaxBSetup = new SetUp(); 
            int MaxASetUpNumber = 0;
            int MaxBSetUpNumber = 0;
            foreach (KeyValuePair<SetUp, int> entry in AllSetUps) {
                if (entry.Key.Site == SetUp.BombSite.A) {
                    if (entry.Value > MaxASetUpNumber) {
                        MaxASetUp = entry.Key; 
                    }
                } else {
                    if (entry.Value > MaxBSetUpNumber) {
                        MaxBSetup = entry.Key;
                    }
                }
            }
            return new TeamSetUp(MaxASetUp, MaxBSetup, teamName); 
        }
    }
}
