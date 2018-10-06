using System;
using DemoInfo;
using System.Linq;


namespace CS_GO_Analysis {
    public class WeaponInfo {

        /// <summary>
        /// Get the infos about the max Ammo in the Weapons. 
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public static int WeaponAmmoMagazine(EquipmentElement weapon) {
            switch (weapon) {
                case EquipmentElement.Unknown:
                    return 0;
                case EquipmentElement.P2000:
                    return 13;
                case EquipmentElement.Glock:
                    return 20;
                case EquipmentElement.P250:
                    return 13;
                case EquipmentElement.Deagle:
                    return 7;
                case EquipmentElement.FiveSeven:
                    return 20;
                case EquipmentElement.DualBarettas:
                    return 30;
                case EquipmentElement.Tec9:
                    return 24;
                case EquipmentElement.CZ:
                    return 12;
                case EquipmentElement.USP:
                    return 12;
                case EquipmentElement.Revolver:
                    return 8;
                case EquipmentElement.MP7:
                    return 30;
                case EquipmentElement.MP9:
                    return 30;
                case EquipmentElement.Bizon:
                    return 64;
                case EquipmentElement.Mac10:
                    return 30;
                case EquipmentElement.UMP:
                    return 25;
                case EquipmentElement.P90:
                    return 50;
                case EquipmentElement.SawedOff:
                    return 7;
                case EquipmentElement.Nova:
                    return 8;
                case EquipmentElement.Swag7:
                    return 5;
                case EquipmentElement.XM1014:
                    return 7;
                case EquipmentElement.M249:
                    return 100;
                case EquipmentElement.Negev:
                    return 150;
                case EquipmentElement.Gallil:
                    return 34;
                case EquipmentElement.Famas:
                    return 25;
                case EquipmentElement.AK47:
                    return 30;
                case EquipmentElement.M4A4:
                    return 30;
                case EquipmentElement.M4A1:
                    return 20;
                case EquipmentElement.Scout:
                    return 10;
                case EquipmentElement.SG556:
                    return 30;
                case EquipmentElement.AUG:
                    return 30;
                case EquipmentElement.AWP:
                    return 10;
                case EquipmentElement.Scar20:
                    return 20;
                case EquipmentElement.G3SG1:
                    return 20;
                case EquipmentElement.Zeus:
                    return 1;
                case EquipmentElement.Kevlar:
                    return -1;
                case EquipmentElement.Helmet:
                    return -1;
                case EquipmentElement.Bomb:
                    return -1;
                case EquipmentElement.Knife:
                    return -1;
                case EquipmentElement.DefuseKit:
                    return -1;
                case EquipmentElement.World:
                    break;
                case EquipmentElement.Decoy:
                    break;
                case EquipmentElement.Molotov:
                    break;
                case EquipmentElement.Incendiary:
                    break;
                case EquipmentElement.Flash:
                    break;
                case EquipmentElement.Smoke:
                    break;
                case EquipmentElement.HE:
                    break;
            }
            return 0;
        }

        /// <summary>
        /// Get the price of the weapon. 
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public static int WeaponPrice(EquipmentElement weapon) {
            switch (weapon) {
                case EquipmentElement.Unknown:
                    break;
                case EquipmentElement.P2000:
                    return 200; 
                case EquipmentElement.Glock:
                    return 200; 
                case EquipmentElement.P250:
                    return 250; 
                case EquipmentElement.Deagle:
                    return 700; 
                case EquipmentElement.FiveSeven:
                    return 500; 
                case EquipmentElement.DualBarettas:
                    return 400; 
                case EquipmentElement.Tec9:
                    return 500; 
                case EquipmentElement.CZ:
                    return 500; 
                case EquipmentElement.USP:
                    return 200; 
                case EquipmentElement.Revolver:
                    return 600; 
                case EquipmentElement.MP7:
                    return 1500; 
                case EquipmentElement.MP9:
                    return 1250; 
                case EquipmentElement.Bizon:
                    return 1400; 
                case EquipmentElement.Mac10:
                    return 1050; 
                case EquipmentElement.UMP:
                    return 1200; 
                case EquipmentElement.P90:
                    return 2350; 
                case EquipmentElement.SawedOff:
                    return 1200; 
                case EquipmentElement.Nova:
                    return 1200; 
                case EquipmentElement.Swag7:
                    return 1800; 
                case EquipmentElement.XM1014:
                    return 2000; 
                case EquipmentElement.M249:
                    return 5200; 
                case EquipmentElement.Negev:
                    return 1700; 
                case EquipmentElement.Gallil:
                    return 2000; 
                case EquipmentElement.Famas:
                    return 2250; 
                case EquipmentElement.AK47:
                    return 2700; 
                case EquipmentElement.M4A4:
                    return 3100; 
                case EquipmentElement.M4A1:
                    return 3100; 
                case EquipmentElement.Scout:
                    return 1700; 
                case EquipmentElement.SG556:
                    return 3000; 
                case EquipmentElement.AUG:
                    return 3300; 
                case EquipmentElement.AWP:
                    return 4750; 
                case EquipmentElement.Scar20:
                    return 5000; 
                case EquipmentElement.G3SG1:
                    return 5000; 
                case EquipmentElement.Zeus:
                    return 200; 
                case EquipmentElement.Kevlar:
                    return 650; 
                case EquipmentElement.Helmet:
                    return 350; 
                case EquipmentElement.Bomb:
                    return 0; 
                case EquipmentElement.Knife:
                    break;
                case EquipmentElement.DefuseKit:
                    return 400;
                case EquipmentElement.World:
                    break;
                case EquipmentElement.Decoy:
                    return 50;
                case EquipmentElement.Molotov:
                    return 400;
                case EquipmentElement.Incendiary:
                    return 600;
                case EquipmentElement.Flash:
                    return 200;
                case EquipmentElement.Smoke:
                    return 300;
                case EquipmentElement.HE:
                    return 300;
            }
            return 0; 
        }
    }
}
