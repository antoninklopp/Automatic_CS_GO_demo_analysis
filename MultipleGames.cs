using System;
using DemoInfo;
using System.Collections.Generic; 

namespace CS_GO_Analysis {

    /// <summary>
    /// A way to analyze multiple games at one. 
    /// All the games need to be on the same map. 
    /// </summary>
    public class MultipleGames {

        private string MapName;

        private string TeamName; 

        public List<GameInfo> AllGames; 

        public MultipleGames(string mapName, string teamName) {
            MapName = mapName;
            TeamName = teamName; 
        }

        public void AnalyzeNewGame(GameInfo g) {
            // First we need to check if the map is the right type and from the right team
        }
    }
}
