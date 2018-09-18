using System;
using DemoInfo;

namespace CS_GO_Analysis {
    public static class FragGenerator {
        public static void GenerateFrags(DemoParser parser) {
            parser.ParseHeader();

            int numberCT = 5;
            int numberT = 5; 

            // Make a print on round-start so you can see the actual frags per round. 
            parser.RoundStart += (sender, e) => {
                Console.WriteLine("New Round, Current Score: T {0} : {1} CT", parser.TScore, parser.CTScore);

                numberCT = 5;
                numberT = 5;
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


                if (e.Victim.Team == Team.CounterTerrorist) {
                    numberCT--; 
                } else {
                    numberT--;
                }
                
                Console.WriteLine();

            };

            parser.RoundEnd += (sender, e) => {
                Console.WriteLine("NumberCT alive " + numberCT.ToString() + " Number T alive " + numberT.ToString());
                Console.WriteLine();
            }; 

            parser.ParseToEnd();
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
    }
}
