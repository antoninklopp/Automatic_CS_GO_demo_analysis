﻿using System;
using DemoInfo;
using System.Collections.Generic;
using System.Text; 

namespace CS_GO_Analysis {

    /// <summary>
    /// Describing a SetUp
    ///
    /// - Bombsite
    /// - Player On Site
    /// - Position of the Player
    /// 
    /// WARNING : A set up is just for the defense. 
    /// </summary>
    public class SetUp {

        public enum BombSite {
            A, 
            B
        };

        public BombSite Site;

        public List<PlayerBombSite> players = new List<PlayerBombSite>(); 

        public SetUp() {

        }

        public SetUp(BombSite site, List<PlayerBombSite> players) {
            Site = site;
            this.players = players;
        }

        /// <summary>
        /// Determine the SetUp on a bomb site
        /// </summary>
        /// <param name="Players"></param>
        /// <param name="mapInfo"></param>
        public void DetermineSetUp(List<PlayerBombSite> Players, Map_JSON mapInfo, BombSite site) {
            this.Site = site;

            float BombSiteX;
            float BombSiteY;

            // Get the bomb site information
            if (site == BombSite.A) {
                BombSiteX = mapInfo.bombA_x;
                BombSiteY = mapInfo.bombA_y;
            } else {
                BombSiteX = mapInfo.bombB_x;
                BombSiteY = mapInfo.bombB_y;
            }

            // We need to find a better measure but we say that a player is a player of a 
            // bomb site if he is at a distance inferior to the bombsite

            // Iterate over the players to determine their site
            foreach (PlayerBombSite p in Players) {
                if (Distance(new Vector(BombSiteX, BombSiteY, 0), p.Position, 
                        mapInfo.pos_x, mapInfo.pos_y, mapInfo.scale) < 0.3f) {
                    // The player is a player of this bombsite
                    if (Program.VERBOSE) {
                        Console.WriteLine("{0} is a player of {1} bombsite", p.Name, site); 
                    }
                    players.Add(new PlayerBombSite(p)); 
                }
            }
        }

        public float Distance(Vector BombSite, Vector Player, float offsetX, float offsetY, float scale) {
            // We want the number between 0 and 1
            // Y coordinate is the wrong sense
            Player = new Vector((Player.X - offsetX) / (scale * 1024), -(Player.Y - offsetY) / (scale * 1024), Player.Z / (scale * 1024));
            return (float)Math.Sqrt(Math.Pow((BombSite.X - Player.X), 2) + Math.Pow((BombSite.Y - Player.Y), 2)); 
        }

        public override string ToString() {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("Bombsite {0}\n", this.Site);
            foreach (PlayerBombSite p in players) {
                s.AppendFormat("{0} ", p.Name); 
            }

            return s.ToString(); 
        }

        /// <summary>
        /// Return the list of the players in this bombsite. 
        /// </summary>
        /// <returns></returns>
        public List<PlayerBombSite> ToPlayerList() {
            return players; 
        }

        /// <summary>
        /// Override the hash code for the setup. 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            List<string> listNames = new List<string>(); 
            foreach(PlayerBombSite p in players) {
                listNames.Add(p.Name); 
            }
            // Sort the player names because we want that player A-B and player B-A be the same setup. 
            listNames.Sort();
            string concatenatedNames = ""; 
            foreach(string n in listNames) {
                concatenatedNames += n;
            }
            return concatenatedNames.GetHashCode() + Site.GetHashCode(); 
        }

        public override bool Equals(object obj) {
            if (obj.GetType() != typeof(SetUp)) {
                return false; 
            }
            SetUp objSetUp = (SetUp)obj; 
            if (objSetUp.GetHashCode() == GetHashCode()) {
                return true; 
            }
            return false;
        }
    }
}
