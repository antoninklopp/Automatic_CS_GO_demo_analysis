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

        public Vector NotMovingPosition;
        public int numberTickNotMoving; 

        public List<Death> AllDeaths;
        public List<Kill> AllKills; 

        // important to differentiate CT and T heatmaps. 
        public int[,] PositionHeatT = new int[(int)(1024f/sizeHeatMap) + 1, (int)(1024f / sizeHeatMap) + 1];
        public int[,] PositionHeatCT = new int[(int)(1024f / sizeHeatMap) + 1, (int)(1024f / sizeHeatMap) + 1];

        // The position of freeze, of wait without moving
        public int[,] PositionHeatTNoMove = new int[(int)(1024f / sizeHeatMap) + 1, (int)(1024f / sizeHeatMap) + 1];
        public int[,] PositionHeatCTNoMove = new int[(int)(1024f / sizeHeatMap) + 1, (int)(1024f / sizeHeatMap) + 1];

        public Player(string name) {
            Name = name;
            AllDeaths = new List<Death>();
            AllKills = new List<Kill>(); 
        }

        public Player(string name, Vector position, Vector lastPosition, int lastBulletNumber, EquipmentElement weapon, 
            Team teamSide, string teamName) : this(name) {
            Position = position;
            LastPosition = lastPosition;
            LastBulletNumber = lastBulletNumber;
            Weapon = weapon;
            TeamSide = teamSide;
            TeamName = teamName;
            NotMovingPosition = new Vector(position.X, position.Y, position.Z);
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
            NotMovingPosition = new Vector(p.Position.X, p.Position.Y, p.Position.Z);
            AllDeaths = new List<Death>();
            AllKills = new List<Kill>();
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

            // Check for lines holding.
            if (Distance(position, NotMovingPosition) < 10f) {
                numberTickNotMoving += 1;
                if (numberTickNotMoving > 500){
                    if (TeamSide == Team.CounterTerrorist) {
                        PositionHeatCTNoMove[(int)(PositionMiniMap.X / sizeHeatMap), (int)(PositionMiniMap.Y / sizeHeatMap)]++;
                    }
                    else {
                        PositionHeatTNoMove[(int)(PositionMiniMap.X / sizeHeatMap), (int)(PositionMiniMap.Y / sizeHeatMap)]++;
                    }
                }
                // Console.WriteLine("{0} {1} {2} {3}", numberTickNotMoving, Distance(position, NotMovingPosition), position, NotMovingPosition); 
            } else {
                NotMovingPosition = new Vector(position.X, position.Y, position.Z);
                numberTickNotMoving = 0;
            }
        }

        public void AddDeath(Death d) {
            AllDeaths.Add(d);
        }

        public void AddKill(Kill k) {
            AllKills.Add(k); 
        }

        /// <summary>
        /// Simple distance function between vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static float Distance(Vector v1, Vector v2) {
            return (float)Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2) + Math.Pow(v1.Z - v2.Z, 2)); 
        }
    }
}
