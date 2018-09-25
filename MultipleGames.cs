using System;
using DemoInfo;
using System.Collections.Generic; 

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
            return Players; 
        }
    }
}
