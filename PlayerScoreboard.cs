using System;
using DemoInfo;

namespace CS_GO_Analysis {

    /// <summary>
    /// An extension of the player class to add aditional information for scoreboard. 
    /// </summary>
    public class PlayerScoreboard : Player {

        /// <summary>
        /// Number of HeadShot
        /// </summary>
        public int HS;

        /// <summary>
        /// Number of rounds played
        /// </summary>
        public int RoundNumber;

        /// <summary>
        /// Total damage dealt
        /// </summary>
        public int Damage;

        /// <summary>
        /// Total enemy flashed
        /// </summary>
        public int EnemyFlashed;

        /// <summary>
        /// Number of enemy flashed leading to a kill.
        /// </summary>
        public int EnemyFlashedToKill; 

        /// <summary>
        /// Number of HE making damages
        /// </summary>
        public int HEGrenadeLanded;

        /// <summary>
        /// Number of HE grenades making kills. 
        /// </summary>
        public int HEGrenadeKills; 

        /// <summary>
        /// Number of kills
        /// </summary>
        public int Kills;

        /// <summary>
        /// Number of assists
        /// </summary>
        public int Assists;

        /// <summary>
        /// Number of deaths
        /// </summary>
        public int Deaths; 

        public PlayerScoreboard(string name) : base(name) {

        }

        public PlayerScoreboard(string name, Vector position, Vector lastPosition, 
            int lastBulletNumber, EquipmentElement weapon, Team teamSide, string TeamName) : 
            base(name, position, lastPosition, lastBulletNumber, weapon, teamSide, TeamName) {
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return base.ToString();
        }

        public void EnemyKilledWithHS() {
            HS++;
            Kills++; 
        }

        public void EnemyKilledNoHS() {
            Kills++; 
        }

        public float HSRate() {
            return (float)HS / Kills; 
        }

        public void UpdateNewRound() {
            RoundNumber++; 
        }

    }
}
