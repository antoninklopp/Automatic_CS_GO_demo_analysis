using System;
using DemoInfo;


namespace CS_GO_Analysis {
    public class PlayerBombSite {

        public enum PlayerWeaponStyle {
            RIFLE,
            AWP
        };

        public string Name;
        public Vector Position;
        public PlayerWeaponStyle WeaponStyle;
        public SetUp.BombSite Bombsite;

        public PlayerBombSite() {

        }

        public PlayerBombSite(string name, Vector position) {
            Name = name;
            Position = position;
        }

        public PlayerBombSite(string name, Vector position, PlayerWeaponStyle weaponStyle, SetUp.BombSite bombsite) {
            Name = name;
            Position = position;
            WeaponStyle = weaponStyle;
            Bombsite = bombsite; 
        }

        public PlayerBombSite(PlayerBombSite p) {
            this.Name = p.Name;
            this.Position = p.Position;
            this.WeaponStyle = p.WeaponStyle;
            this.Bombsite = p.Bombsite; 
        }
    }
}