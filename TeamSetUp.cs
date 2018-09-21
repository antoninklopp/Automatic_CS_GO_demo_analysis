using System;
using DemoInfo;
using System.Collections.Generic;
using System.Collections; 

namespace CS_GO_Analysis {
    public class TeamSetUp {

        public SetUp A;
        public SetUp B;

        /// <summary>
        /// Name of the team. 
        /// The team must be in CT. 
        /// </summary>
        public Team Name;

        /// <summary>
        /// The Money Type
        /// </summary>
        public Round.RoundType MoneyType; 

        public TeamSetUp() {

        }

        public List<PlayerBombSite> ToPlayersList() {
            List<PlayerBombSite> Players = A.ToPlayerList(); 
            foreach (PlayerBombSite b in B.ToPlayerList()) {
                Players.Add(b); 
            }
            return Players; 
        }

    }
}
