using System;
using DemoInfo;
using System.Collections.Generic;
using CS_GO_Analysis.Maps; 

namespace CS_GO_Analysis {

    /// <summary>
    /// A way to analyze multiple games at one. 
    /// All the games need to be on the same map. 
    /// </summary>
    public class MultipleGames {

        /// <summary>
        /// The map we want to study
        /// </summary>
        private string MapName;

        /// <summary>
        /// The team we want to study
        /// </summary>
        private string TeamName; 

        public List<GameInfo> AllGames; 

        public MultipleGames(string mapName, string teamName) {
            MapName = mapName;
            TeamName = teamName; 
        }

        public void AnalyzeNewGame(DemoParser demo) {
            GameInfo g = new GameInfo();
            // First we need to check if the map is the right type and from the right team
            if (g.GetMapName(demo) != MapName || !g.CheckTeamName(demo, TeamName)) {
                return; 
            }

            g.ParseGame(demo);
            AllGames.Add(g); 
        }

        /// <summary>
        /// Generate the heatmaps from the players
        /// into the map. 
        /// </summary>
        public void GenerateHeatMapsPosition() {

            Dictionary<string, Player> Players = GetPlayers(); 

            // Get the data from all the games
            foreach (GameInfo g in AllGames) {
                Dictionary<string, Player> gamePlayers = g.AllPlayers; 
                foreach (KeyValuePair<string, Player> entry in Players){
                    Player p = gamePlayers[entry.Key]; 
                    for (int i = 0; i < (int)(1024f/Player.sizeHeatMap) + 1; i++) {
                        for (int j = 0; j < (int)(1024f / Player.sizeHeatMap) + 1; j++) {
                            Players[entry.Key].PositionHeatCT[i, j] += p.PositionHeatCT[i, j]; 
                            Players[entry.Key].PositionHeatT[i, j] += p.PositionHeatT[i, j]; 
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, Player> entry in Players) {
                GenerateHeatMaps.GenerateHeatpMapPosition(entry.Value, MapName, Team.CounterTerrorist);
                GenerateHeatMaps.GenerateHeatpMapPosition(entry.Value, MapName, Team.Terrorist);
            }

        }

        /// <summary>
        /// Get the players from the team from the game. 
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, Player> GetPlayers() {
            // The players must be in the first demo. 
            Dictionary<string, Player> Players = new Dictionary<string, Player>();
            GameInfo g = AllGames[0];
            List<string> PlayersNames = g.GetPlayersFromTeam(TeamName); 
            // Create the dictionnary with all players.
            foreach (string player in PlayersNames) {
                Players.Add(player, new Player(player)); 
            }
            return Players; 
        }
    }
}
