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
            if (e.Assister != null) {
                AllPlayers[e.Assister.Name].Assists++; 
            }

            if (e.Headshot) {
                AllPlayers[e.Killer.Name].EnemyKilledWithHS(); 
            } else {
                AllPlayers[e.Killer.Name].EnemyKilledNoHS();
            }

            AllPlayers[e.Victim.Name].Deaths++; 
        }


    }

}
