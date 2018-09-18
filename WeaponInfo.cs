using System;
using DemoInfo;
using System.Linq;

public class WeaponInfo
{
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
}
