using System;
using DemoInfo;

namespace CS_GO_Analysis {

    public class Player {

        public string Name;
        public Vector Position;
        public Vector LastPosition;
        public int LastBulletNumber;
        public EquipmentElement Weapon; 

        public Player(string name, Vector position, Vector lastPosition, int lastBulletNumber, EquipmentElement weapon) {
            Name = name;
            Position = position;
            LastPosition = lastPosition;
            LastBulletNumber = lastBulletNumber;
            Weapon = weapon;
        }

        public void Update(Vector position, int newBulletNumber) {
            //if (LastBulletNumber != newBulletNumber) {
            //    Console.WriteLine("{0} fired a shot with {1} previous Ammo {2} next Ammo {3}", Name, Weapon.ToString(), LastBulletNumber, 
            //        newBulletNumber);
            //}
            LastPosition = Position;
            Position = position;
            LastBulletNumber = newBulletNumber; 
        }
    }
}
