using System;
using DemoInfo;
using System.Collections.Generic; 

namespace CS_GO_Analysis {

    public class Player {

        public string Name;
        public Vector Position;
        public Vector LastPosition;
        public int LastBulletNumber;
        public EquipmentElement Weapon;
        public Team TeamName;

        public List<Death> AllDeaths; 

        public Player(string name) {
            Name = name;
            AllDeaths = new List<Death>(); 
        }

        public Player(string name, Vector position, Vector lastPosition, int lastBulletNumber, EquipmentElement weapon, 
            Team teamName) : this(name) {
            Position = position;
            LastPosition = lastPosition;
            LastBulletNumber = lastBulletNumber;
            Weapon = weapon;
            TeamName = teamName; 
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="p"></param>
        public Player (Player p) {
            Name = p.Name;
            Position = p.Position;
            LastPosition = p.LastPosition;
            LastBulletNumber = p.LastBulletNumber;
            Weapon = p.Weapon;
            TeamName = p.TeamName;
            AllDeaths = new List<Death>();
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

        public void AddDeath(Death d) {
            AllDeaths.Add(d);
        }
    }
}
