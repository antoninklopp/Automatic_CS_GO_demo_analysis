using System;
using System.Collections.Generic;
using DemoInfo; 

namespace CS_GO_Analysis {

    /// <summary>
    /// A class meant to generate scoreboard. 
    /// </summary>
    public class Scoreboard {

        public Dictionary<string, PlayerScoreboard> AllPlayers;

        public Scoreboard() {
            AllPlayers = new Dictionary<string, PlayerScoreboard>(); 
        }

        public void AddPlayerToScoreboard(PlayerScoreboard p) {
            if (!AllPlayers.ContainsKey(p.Name)) {
                AllPlayers.Add(p.Name, p);
            }
        }

        /// <summary>
        /// Update the score board when a player is killed. 
        /// </summary>
        /// <param name="e"></param>
        public void UpdateScoreBoardPlayerKilled(PlayerKilledEventArgs e) {

            if (!AllPlayers.ContainsKey(e.Killer.Name)) {
                AllPlayers.Add(e.Killer.Name, new PlayerScoreboard(e.Killer.Name)); 
            }
            if (!AllPlayers.ContainsKey(e.Victim.Name)) {
                AllPlayers.Add(e.Victim.Name, new PlayerScoreboard(e.Victim.Name));
            }

            if (e.Assister != null) {
                AllPlayers[e.Assister.Name].Assists++; 
            }

            //if (e.Headshot) {
            //    AllPlayers[e.Killer.Name].EnemyKilledWithHS(); 
            //} else {
            //    AllPlayers[e.Killer.Name].EnemyKilledNoHS();
            //}

            AllPlayers[e.Killer.Name].Kills++; 

            AllPlayers[e.Victim.Name].Deaths++; 
        }

        /// <summary>
        /// Generate the scoreboard of the end of the game. 
        /// </summary>
        public void GenerateScoreboard() {
            foreach (KeyValuePair<string, PlayerScoreboard> entry in AllPlayers) {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("{0, 15} | {1, 5} | {2,5} | {3, 5} ", entry.Value.Name, entry.Value.Kills, 
                    entry.Value.Assists, entry.Value.Deaths); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the scoreboard is initializd, no if is not</returns>
        public bool IsInitialized() {
            return (AllPlayers.Count != 0); 
        }


    }

}
