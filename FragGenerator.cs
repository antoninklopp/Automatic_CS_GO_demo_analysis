using System;
using DemoInfo;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using CS_GO_Analysis.Maps; 

namespace CS_GO_Analysis {

    public static class FragGenerator {

        public static bool BackStabbing(PlayerKilledEventArgs e) {
            Vector PositionVictim = e.Victim.Position;
            Vector PositionKiller = e.Killer.Position;
            float VictimLookX = e.Victim.ViewDirectionX;
            float VictimLookY = e.Victim.ViewDirectionY;

            Console.WriteLine("Looking towards {0} {1}", VictimLookX, VictimLookY);

            Vector DirectionVictimKiller = new Vector(PositionKiller.X - PositionVictim.X, PositionKiller.Y - PositionVictim.Y,
                PositionKiller.Z - PositionVictim.Z);

            float dotprod = VictimLookX * DirectionVictimKiller.X + VictimLookY * DirectionVictimKiller.Y;

            return dotprod < 0;  
        }


        public static void GenerateFrags(DemoParser parser) {

            Dictionary<string, Player> AllPlayers = new Dictionary<string, Player>();
            List<Death> deaths = new List<Death>(); 
            var outputStream = new StreamWriter("round.txt");

            float timeBeginningRound = 0f; 

            parser.ParseHeader();

            string mapName = parser.Map;
            Map_JSON map = new Map_JSON(); 
            // Get information from the assoiated JSON file. 
            try {
                map = JsonConvert.DeserializeObject<Map_JSON>(File.ReadAllText("Maps_json/" + mapName + ".json"));
            } catch (FileNotFoundException) {
                Console.WriteLine("File was not found {0}", mapName);
                System.Environment.Exit(1); 
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

                //foreach (var player in parser.PlayingParticipants) {
                //    AllPlayers.Add(player.Name, new Player(player.Name, player.Position, player.Position,
                //        player.ActiveWeapon.AmmoInMagazine, player.ActiveWeapon.Weapon));
                //}
            };

            parser.PlayerKilled += (sender, e) => {
                if (e.Killer == null) {
                    // The player has murdered himself (falling / own nade / ...)
                    Console.WriteLine("<World><0><None>");
                }
                else {
                    Console.Write("<{0}><{1}> killed ", e.Killer.Name, ShortTeam(e.Killer.Team));
                }

                if (e.Assister == null) {
                    // nothing
                }
                else {
                    Console.Write(" + <{0}><{1}> assist ", e.Assister.Name, ShortTeam(e.Assister.Team));
                }

                Console.Write(" [{0}]", e.Weapon.Weapon);

                if (e.Headshot) {
                    Console.Write("[HS]");
                }

                if (e.PenetratedObjects > 0) {
                    Console.Write("[Wall]");
                }

                Console.Write(" ");

                Console.Write(" <{0}><{1}>", e.Victim.Name, ShortTeam(e.Victim.Team));
                Console.Write(" Weapon {0} Ammo in Magazine {1} {2}", e.Victim.ActiveWeapon.Weapon, 
                    e.Victim.ActiveWeapon.AmmoInMagazine.ToString(), 
                    e.Victim.ActiveWeapon.AmmoInMagazine == WeaponInfo.WeaponAmmoMagazine(e.Victim.ActiveWeapon.Weapon) ? 
                    "Ammo Max" : "");

                if (BackStabbing(e)) {
                    Console.Write("  Not Looking player");
                }
                else {
                    Console.Write("  LookingPlayer"); 
                }

                if (e.Victim.Team == Team.CounterTerrorist) {
                    numberCT--; 
                } else {
                    numberT--;
                }
                
                Console.WriteLine();
                WriteToFileDeadPlayers(outputStream, AllPlayers[e.Victim.Name]);
                deaths.Add(new Death(GetPositionMiniMap(e.Victim.Position, map.pos_x, map.pos_y, map.scale), e.Victim.Team)); 
            };

            parser.RoundEnd += (sender, e) => {
                Console.WriteLine("NumberCT alive " + numberCT.ToString() + " Number T alive " + numberT.ToString());
                Console.WriteLine();

                GenerateHeatMaps.GenerateMap(deaths, parser.CTScore + parser.TScore); 
            };

            parser.TickDone += (sender, e) => {

                float currentTime = parser.CurrentTime - timeBeginningRound; 

                // Updated every frame
                foreach (var player in parser.PlayingParticipants) {
                    // We multiply it by the time of one tick
                    // Since the velocity is given in 
                    // ingame-units per second
                    // Console.WriteLine("{0} {1}", player.Name, player.Position);
                    if (AllPlayers.ContainsKey(player.Name)) {
                        Player current = AllPlayers[player.Name];
                        //if (player.IsAlive) {
                        //    current.Update(player.Position, player.ActiveWeapon.AmmoInMagazine);
                        //}
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


        private static string ShortTeam(Team team) { 
            switch (team) {
                case Team.Spectate:
                    return "None";
                case Team.Terrorist:
                    return "T";
                case Team.CounterTerrorist:
                    return "CT";
            }

            return "None";
        }

        private static void WriteToFileDeadPlayers(StreamWriter outputStream, Player DeadPlayer) {
            //outputStream.WriteLine("{0};{1};{2};{3}", DeadPlayer.Name,
            //    DeadPlayer.Position.X, DeadPlayer.Position.Y, DeadPlayer.Position.Z
            //    ); 

            if( (DeadPlayer.Position.X + 3230 < 0) || ((DeadPlayer.Position.X + 3230)/5.0 > 1024) || (DeadPlayer.Position.Y - 1713 > 0)
                || ((DeadPlayer.Position.Y - 1713)/5.0 < -1024)) {
                outputStream.WriteLine("PROBLEM"); 
            }
        }

        private static Vector GetPositionMiniMap(Vector Position, int pos_x, int pos_y, float scale) {
            Vector PositionMiniMap = new Vector((Position.X - pos_x) / scale, -(Position.Y - pos_y) / scale, 0f);
            if ((PositionMiniMap.X > 1024) || (PositionMiniMap.X < 0) || (PositionMiniMap.Y > 1024) || (PositionMiniMap.Y < 0)) {
                Console.WriteLine("PROBLEM"); 
            }
            return PositionMiniMap; 
        }
    }

}
