using System;
using DemoInfo; 

public class PlayerBombSite
{

    public enum PlayerWeaponStyle {
        RIFLE, 
        AWP
    }; 

    public string Name;
    public Vector Position;
    public PlayerWeaponStyle WeaponStyle; 

	public PlayerBombSite()
	{

	}

    public PlayerBombSite(string name, Vector position, PlayerWeaponStyle weaponStyle) {
        Name = name;
        Position = position;
        WeaponStyle = weaponStyle;
    }

    public PlayerBombSite(PlayerBombSite p) {
        this.Name = p.Name;
        this.Position = p.Position;
        this.WeaponStyle = p.WeaponStyle; 
    }
}
