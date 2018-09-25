using System;
using DemoInfo;
using System.Collections.Generic; 

namespace CS_GO_Analysis {

    public class Player {

        public const int sizeHeatMap = 5;  

        public string Name;
        public Vector Position;
        public Vector LastPosition;
        public int LastBulletNumber;
        public EquipmentElement Weapon;
        public Team TeamSide;
        public string TeamName; 

        public List<Death> AllDeaths;

        // important to differentiate CT and T heatmaps. 
        public int[,] PositionHeatT = new int[(int)(1024f/sizeHeatMap) + 1, (int)(1024f / sizeHeatMap) + 1];
        public int[,] PositionHeatCT = new int[(int)(1024f / sizeHeatMap) + 1, (int)(1024f / sizeHeatMap) + 1];

        public Player(string name) {
            Name = name;
            AllDeaths = new List<Death>(); 
        }

        public Player(string name, Vector position, Vector lastPosition, int lastBulletNumber, EquipmentElement weapon, 
            Team teamSide, string teamName) : this(name) {
            Position = position;
            LastPosition = lastPosition;
            LastBulletNumber = lastBulletNumber;
            Weapon = weapon;
            TeamSide = teamSide;
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
            TeamSide = p.TeamSide;
            AllDeaths = new List<Death>();
        }

        public void Update(Vector position, Vector PositionMiniMap, int newBulletNumber) {
            //if (LastBulletNumber != newBulletNumber) {
            //    Console.WriteLine("{0} fired a shot with {1} previous Ammo {2} next Ammo {3}", Name, Weapon.ToString(), LastBulletNumber, 
            //        newBulletNumber);
            //}
            LastPosition = Position;
            Position = position;
            LastBulletNumber = newBulletNumber;
            if (TeamSide == Team.CounterTerrorist) {
                PositionHeatCT[(int)(PositionMiniMap.X/ sizeHeatMap), (int)(PositionMiniMap.Y/ sizeHeatMap)]++;
            } else {
                PositionHeatT[(int)(PositionMiniMap.X / sizeHeatMap), (int)(PositionMiniMap.Y / sizeHeatMap)]++;
            }
        }

        public void AddDeath(Death d) {
            AllDeaths.Add(d);
        }
    }
}
