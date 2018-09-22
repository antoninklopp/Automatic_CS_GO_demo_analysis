﻿using System;
using DemoInfo;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO; 

namespace CS_GO_Analysis {

    /// <summary>
    /// A global object meant to get all the informations from the game. 
    /// </summary>
    public class GameInfo {

        /// <summary>
        /// All the rounds from the game
        /// </summary>
        public List<Round> AllRounds = new List<Round>();

        public GameInfo() {
            
        }

        public void ParseGame(DemoParser parser) {
            Dictionary<string, Player> AllPlayers = new Dictionary<string, Player>();
            List<Death> deaths = new List<Death>();
            var outputStream = new StreamWriter("round.txt");

            float timeBeginningRound = 0f;

            Round currentRound = new Round();
            bool setUpDetermined = false; 

            parser.ParseHeader();

            string mapName = parser.Map;
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

            outputStream.WriteLine(parser.Map);

            // Make a print on round-start so you can see the actual frags per round. 
            parser.RoundStart += (sender, e) => {

                timeBeginningRound = parser.CurrentTime;
                outputStream.WriteLine("Round {0}", parser.CTScore + parser.TScore);
                Console.WriteLine("New Round, Current Score: T {0} : {1} CT", parser.TScore, parser.CTScore);

                numberCT = 5;
                numberT = 5;

                AllPlayers = new Dictionary<string, Player>();
                
                deaths = new List<Death>();

                currentRound = new Round {
                    CTTeam = parser.CTClanName,
                    TTeam = parser.TClanName
                };
                setUpDetermined = false; 
            };

            parser.RoundEnd += (sender, e) => {

                AllRounds.Add(currentRound); 

                Console.WriteLine("NumberCT alive " + numberCT.ToString() + " Number T alive " + numberT.ToString());
                Console.WriteLine();
            };

            parser.TickDone += (sender, e) => {

                float currentTime = parser.CurrentTime - timeBeginningRound;

                if (currentTime > 30 && !setUpDetermined) {
                    setUpDetermined = true;
                    List<Player> PlayersList = new List<Player>(); 
                    foreach(KeyValuePair<string, Player> entry in AllPlayers) {
                        PlayersList.Add(new Player(entry.Value)); 
                    }
                    currentRound.DefenseSetUp = new TeamSetUp(PlayersList, map);  
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
                            current.Update(player.Position, player.ActiveWeapon.AmmoInMagazine);
                        }
                    }
                    else {
                        AllPlayers.Add(player.Name, new Player(player.Name, player.Position, player.Position,
                        player.ActiveWeapon.AmmoInMagazine, player.ActiveWeapon.Weapon, player.Team));
                    }
                }
            };

            parser.ParseToEnd();
            outputStream.Close();
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
    }
}
