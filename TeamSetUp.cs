using System;
using DemoInfo;
using System.Collections.Generic;
using System.Collections;
using System.Text; 

namespace CS_GO_Analysis {
    public class TeamSetUp {

        public SetUp A;
        public SetUp B;

        /// <summary>
        /// The Money Type
        /// </summary>
        public Round.RoundType MoneyType; 

        public TeamSetUp() {

        }

        public TeamSetUp(List<Player> AllPlayers, Map_JSON info) {
            A = new SetUp();
            B = new SetUp(); 
            List<PlayerBombSite> Players = new List<PlayerBombSite>(); 
            foreach(Player p in AllPlayers) {
                if (p.TeamName == Team.CounterTerrorist) {
                    Players.Add(new PlayerBombSite(p.Name, p.Position)); 
                }
            }
            A.DetermineSetUp(Players, info, SetUp.BombSite.A);
            B.DetermineSetUp(Players, info, SetUp.BombSite.B); 
        }

        public TeamSetUp(SetUp a, SetUp b, Round.RoundType moneyType) {
            A = a;
            B = b;
            MoneyType = moneyType;
        }

        public TeamSetUp(SetUp a, SetUp b) {
            A = a;
            B = b;
        }

        public List<PlayerBombSite> ToPlayersList() {
            List<PlayerBombSite> Players = A.ToPlayerList(); 
            foreach (PlayerBombSite b in B.ToPlayerList()) {
                Players.Add(b); 
            }
            return Players; 
        }

        public override string ToString() {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("A Setup {0} \n", A);
            s.AppendFormat("B Setup {0} \n", B);
            return s.ToString(); 
        }
    }
}
