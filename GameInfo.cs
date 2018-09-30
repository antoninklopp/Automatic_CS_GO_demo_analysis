using System;
using DemoInfo;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using CS_GO_Analysis.Maps; 

namespace CS_GO_Analysis {

    /// <summary>
    /// A global object meant to get all the informations from the game. 
    /// </summary>
    public class GameInfo {

        /// <summary>
        /// All the rounds from the game
        /// </summary>
        public List<Round> AllRounds = new List<Round>();

        /// <summary>
        /// Scoreboard of the game. 
        /// </summary>
        public Scoreboard board = new Scoreboard();

        /// <summary>
        /// Dictionnary of all the players. 
        /// </summary>
        public Dictionary<string, Player> AllPlayers = new Dictionary<string, Player>();

        public string mapName;
        public string Team1;
        public string Team2;

        public GameInfo() { }

        /// <summary>
        /// Return the map of the current Map; 
        /// </summary>
        /// <param name="parser"></param>
        public string GetMapName(DemoParser parser) {
            return mapName; 
        }

        /// <summary>
        /// Checks if the team we are looking for 
        /// is one of the two teams playing
        /// </summary>
        /// <param name="parser"></param>
        /// <returns>true if the team we are looking for is in the game</returns>
        public bool CheckTeamName(DemoParser parser, string TeamName) {

            if (Team1.Equals(TeamName) || Team2.Equals(TeamName)) {
                return true; 
            }
            return false; 
        }

        /// <summary>
        /// Get the name of the Teams. 
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        public List<string> GetTeamNames(DemoParser parser) {
            return new List<string>() { Team1, Team2 }; 
        }

        public void ParseHeader(DemoParser parser) {
            parser.ParseHeader();

            parser.MatchStarted += (sender, e) => {
                Team1 = parser.CTClanName;
                Team2 = parser.TClanName;
                mapName = parser.Map;
            }; 

            parser.ParseToEnd(); 
        }

        /// <summary>
        /// Get all the informations from the game
        /// </summary>
        /// <param name="parser"></param>
        public void ParseGame(DemoParser parser) {
            List<Death> deaths = new List<Death>();

            float timeBeginningRound = 0f;

            Round currentRound = new Round();
            bool setUpDetermined = false;
            
            parser.ParseHeader();
            mapName = parser.Map;

            Map_JSON map = new Map_JSON();

            // Get information from the assoiated JSON file. 
            try {
                map = JsonConvert.DeserializeObject<Map_JSON>(File.ReadAllText("Maps_json/" + mapName + ".json"));
            }
            catch (FileNotFoundException) {
                Console.WriteLine("File was not found {0}", mapName);
                Environment.Exit(1);
            }

            int numberCT = 5;
            int numberT = 5;

            parser.MatchStarted += (sender, e) => {
                // When the match starts we create a new scoreboard
                board = new Scoreboard(); 
            }; 

            // Make a print on round-start so you can see the actual frags per round. 
            parser.RoundStart += (sender, e) => {

                if (!board.IsInitialized()) {
                    foreach (var player in parser.PlayingParticipants) {
                        board.AddPlayerToScoreboard(new PlayerScoreboard(player.Name)); 
                    }
                }

                foreach (var player in parser.PlayingParticipants) {
                    AllPlayers[player.Name].TeamSide = player.Team; 
                }

                timeBeginningRound = parser.CurrentTime;
                
                // Console.WriteLine("New Round, Current Score: T {0} : {1} CT", parser.TScore, parser.CTScore);

                numberCT = 5;
                numberT = 5;
                
                deaths = new List<Death>();

                currentRound = new Round {
                    CTTeam = parser.CTClanName,
                    TTeam = parser.TClanName, 
                    Number = parser.CTScore + parser.TScore
                };
                setUpDetermined = false;

                board.UpdateScoreBoardNewRound(); 
            };

            parser.RoundEnd += (sender, e) => {

                AllRounds.Add(currentRound); 

                //Console.WriteLine("NumberCT alive " + numberCT.ToString() + " Number T alive " + numberT.ToString());
                //Console.WriteLine();
            };

            parser.TickDone += (sender, e) => {

                float currentTime = parser.CurrentTime - timeBeginningRound;

                if (currentTime > 35 && !setUpDetermined) {
                    // Console.WriteLine("CurrentTime {0}", currentTime); 
                    setUpDetermined = true;
                    List<Player> PlayersList = new List<Player>(); 
                    foreach(KeyValuePair<string, Player> entry in AllPlayers) {
                        PlayersList.Add(new Player(entry.Value)); 
                    }
                    currentRound.DefenseSetUp = new TeamSetUp(PlayersList, map);

                    List<Player> PlayerToDraw = new List<Player>(); 
                    foreach(Player p in PlayersList) {
                        Player p2 = new Player(p) {
                            Position = GetPositionMiniMap(p.Position, map.pos_x, map.pos_y, map.scale)
                        };
                        PlayerToDraw.Add(p2); 
                    }

                    GenerateHeatMaps.GenerateMap(PlayerToDraw, mapName, currentRound.Number); 
                }

                // Updated every frame
                foreach (var player in parser.PlayingParticipants) {
                    // We multiply it by the time of one tick
                    // Since the velocity is given in 
                    // ingame-units per second
                    // Console.WriteLine("{0} {1}", player.Name, player.Position);
                    if (AllPlayers.ContainsKey(player.Name)) {
                        Player current = AllPlayers[player.Name];
                        if (player.IsAlive) {
                            current.Update(player.Position,
                                GetPositionMiniMap(player.Position, map.pos_x, map.pos_y, map.scale),
                                player.ActiveWeapon.AmmoInMagazine);
                        }
                    }
                    else {
                        if (player.ActiveWeapon == null) {
                            AllPlayers.Add(player.Name, new Player(player.Name, player.Position, player.Position,
                            0, EquipmentElement.Knife, player.Team,
                            (player.Team == Team.CounterTerrorist) ? parser.CTClanName : parser.TClanName));
                        }
                        else {
                            AllPlayers.Add(player.Name, new Player(player.Name, player.Position, player.Position,
                            player.ActiveWeapon.AmmoInMagazine, player.ActiveWeapon.Weapon, player.Team,
                            (player.Team == Team.CounterTerrorist) ? parser.CTClanName : parser.TClanName));
                        }
                    }
                }
            };

            parser.PlayerKilled += (sender, e) => {
                board.UpdateScoreBoardPlayerKilled(e);
                AllPlayers[e.Victim.Name].AddDeath(new Death(GetPositionMiniMap(e.Victim.Position, map.pos_x, map.pos_y, map.scale)
                    , e.Victim.Team)); 
                AllPlayers[e.Killer.Name].AddKill()
            };

            // Occurs when a player gets hurt. 
            parser.PlayerHurt += (sender, e) => {
                board.UpdatePlayerHurt(e); 
            }; 

            parser.ParseToEnd();
        }

        public void AddRound(Round r) {
            AllRounds.Add(r); 
        }

        /// <summary>
        /// Get all the setup used during the games. 
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public Dictionary<SetUp, int> GetAllSetUp(string teamName) {
            Dictionary<SetUp, int> AllSetUps = new Dictionary<SetUp, int>(); 
            foreach (Round r in AllRounds) {
                Console.WriteLine(r.CTTeam); 
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
        public TeamSetUp GetGlobalSetUp(string teamName) {
            Dictionary<SetUp, int> AllSetUps = GetAllSetUp(teamName);
            SetUp MaxASetUp = new SetUp();
            SetUp MaxBSetup = new SetUp(); 
            int MaxASetUpNumber = 0;
            int MaxBSetUpNumber = 0;
            foreach (KeyValuePair<SetUp, int> entry in AllSetUps) {
                // Console.WriteLine("SetUP {0} {2} occuring {1} ", entry.Key, entry.Value, entry.Key.GetHashCode()); 
                if (entry.Key.Site == SetUp.BombSite.A) {
                    if (entry.Value > MaxASetUpNumber) {
                        MaxASetUp = entry.Key;
                        MaxASetUpNumber = entry.Value; 
                    }
                } else {
                    if (entry.Value > MaxBSetUpNumber) {
                        MaxBSetup = entry.Key;
                        MaxBSetUpNumber = entry.Value;
                    }
                }
            }
            return new TeamSetUp(MaxASetUp, MaxBSetup); 
        }

        private static Vector GetPositionMiniMap(Vector Position, int pos_x, int pos_y, float scale) {
            Vector PositionMiniMap = new Vector((Position.X - pos_x) / scale, -(Position.Y - pos_y) / scale, 0f);
            if ((PositionMiniMap.X > 1024) || (PositionMiniMap.X < 0) || (PositionMiniMap.Y > 1024) || (PositionMiniMap.Y < 0)) {
                Console.WriteLine("PROBLEM");
            }
            return PositionMiniMap;
        }

        /// <summary>
        /// Generate the player deaths maps. 
        /// </summary>
        public void GenerateDeathMapPlayer() {
            foreach (KeyValuePair<string, Player> entry in AllPlayers) {
                GenerateHeatMaps.GenerateDeathsPlayer(entry.Value, mapName); 
            }
        }

        public void GenerateHeatMapPlayer() {
            foreach (KeyValuePair<string, Player> entry in AllPlayers) {
                GenerateHeatMaps.GenerateHeatpMapPosition(entry.Value, mapName, Team.CounterTerrorist);
                GenerateHeatMaps.GenerateHeatpMapPosition(entry.Value, mapName, Team.Terrorist);
            }
        }

        /// <summary>
        /// Get the names of players in a team. 
        /// </summary>
        /// <param name="TeamName"></param>
        /// <returns></returns>
        public List<string> GetPlayersFromTeam(string TeamName) {
            List<string> AllPlayerNames = new List<string>();
            foreach (KeyValuePair<string, Player> p in AllPlayers) {
                if (p.Value.TeamName == TeamName) {
                    AllPlayerNames.Add(p.Key);
                }
            }
            // Check that we have 5 players in the team. 
            if (AllPlayerNames.Count != 5) {
                Console.WriteLine("ERROR : Not 5 players found in the team"); 
            }
            return AllPlayerNames; 
        }
    }
}
