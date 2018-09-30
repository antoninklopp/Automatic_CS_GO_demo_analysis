using System;
using System.IO;
using DemoInfo;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace CS_GO_Analysis.Maps {
    public class GenerateHeatMaps {
        public static void GenerateMap(List<Death> listDeaths, int roundNumber=0) {

            Bitmap bitmap = new Bitmap("Maps/de_mirage_radar.png");
            Graphics g = Graphics.FromImage(bitmap);

            foreach (Death d in listDeaths) {
                g.DrawEllipse(Pens.Orange, 500, 10, 10, 10); 
                if (d.T == Team.CounterTerrorist) {
                    g.DrawEllipse(Pens.DarkBlue, new Rectangle((int)d.Position.X, (int)d.Position.Y, 5, 5));
                } else {
                    g.DrawEllipse(Pens.Yellow, new Rectangle((int)d.Position.X, (int)d.Position.Y, 5, 5));
                }
            }

            bitmap.Save(@"Test_images/" + roundNumber.ToString()  + ".png", ImageFormat.Png);
        }

        public static void GenerateMap(List<Player> listPlayers, string mapName, int roundNumber = 0) {
            Bitmap bitmap = new Bitmap("Maps/" + mapName + "_radar.png");
            Graphics g = Graphics.FromImage(bitmap);

            foreach (Player p in listPlayers) {
                if (p.TeamSide == Team.CounterTerrorist) {
                    g.DrawEllipse(Pens.DarkBlue, new Rectangle((int)p.Position.X, (int)p.Position.Y, 5, 5));
                }
                else {
                    g.DrawEllipse(Pens.Yellow, new Rectangle((int)p.Position.X, (int)p.Position.Y, 5, 5));
                }
            }

            bitmap.Save(@"Test_images/" + mapName + "_" + roundNumber.ToString() + ".png", ImageFormat.Png);
        }

        /// <summary>
        /// Generate a Map of all the deaths of a player
        /// </summary>
        public static void GenerateDeathsPlayer(Player p, string mapName, string folder= "Test_images") {
            Bitmap bitmap = new Bitmap("Maps/" + mapName + "_radar.png");
            Graphics g = Graphics.FromImage(bitmap);

            List<Death> listDeaths = p.AllDeaths;
            string playerName = p.Name; 

            foreach (Death d in listDeaths) {
                if (d.T == Team.CounterTerrorist) {
                    g.DrawEllipse(Pens.DarkBlue, new Rectangle((int)d.Position.X, (int)d.Position.Y, 5, 5));
                } else {
                    g.DrawEllipse(Pens.Yellow, new Rectangle((int)d.Position.X, (int)d.Position.Y, 5, 5));
                }
            }
            
            if (!Directory.Exists(folder + "/Deaths")) {
                Directory.CreateDirectory(folder + "/Deaths"); 
            }
            bitmap.Save(folder + "/Deaths/" + mapName + "_" + playerName + ".png", ImageFormat.Png);
        }

        /// <summary>
        /// Generate a Map of all the deaths of a player
        /// </summary>
        public static void GenerateKillsPlayer(Player p, string mapName, string folder="Test_images") {
            Bitmap bitmap = new Bitmap("Maps/" + mapName + "_radar.png");
            Graphics g = Graphics.FromImage(bitmap);

            List<Kill> listKills = p.AllKills;
            string playerName = p.Name;

            foreach (Kill k in listKills) {
                if (k.T == Team.CounterTerrorist) {
                    g.DrawEllipse(Pens.DarkBlue, new Rectangle((int)k.Position.X, (int)k.Position.Y, 5, 5));
                }
                else {
                    g.DrawEllipse(Pens.Yellow, new Rectangle((int)k.Position.X, (int)k.Position.Y, 5, 5));
                }
            }

            if (!Directory.Exists(folder + "/Kills")) {
                Directory.CreateDirectory(folder + "/Kills");
            }
            bitmap.Save(folder + "/Kills/" + mapName + "_" + playerName + ".png", ImageFormat.Png);
        }

        public static void GenerateHeatpMapPosition(Player p, string mapName, Team t, string folder="Test_images") {
            Bitmap bitmap = new Bitmap("Maps/" + mapName + "_radar.png");
            Graphics g = Graphics.FromImage(bitmap);

            List<Death> listDeaths = p.AllDeaths;
            string playerName = p.Name;

            // First we find the max value
            int maxValue = 0;
            if (t == Team.CounterTerrorist) {
                maxValue = p.PositionHeatCT.Cast<int>().Max();
            }
            else {
                maxValue = p.PositionHeatT.Cast<int>().Max();
            }

            // iterate over the position of the player
            for (int i = 0; i < (int)(1024f/Player.sizeHeatMap) + 1; i++) {
                for (int j = 0; j < (int)(1024f / Player.sizeHeatMap) + 1; j++) {
                    int currentPosition; 
                    if (t == Team.CounterTerrorist) {
                        currentPosition = p.PositionHeatCT[i, j];
                    } else {
                        currentPosition = p.PositionHeatT[i, j];
                    }
                    Color c = ColorHeatMap.GetColorHeatMap(currentPosition, maxValue);
                    Brush b = new SolidBrush(c); 
                    g.FillRectangle(b, i * Player.sizeHeatMap, j * Player.sizeHeatMap, 
                        Player.sizeHeatMap, Player.sizeHeatMap);
                }
            }

            if (!Directory.Exists(folder + "/Position")) {
                Directory.CreateDirectory(folder + "/Position");
            }

            bitmap.Save(folder + "/Position/HeatMap_" + mapName + "_" + p.Name + "_" + t + ".png", ImageFormat.Png);
        }

        public static void GenerateHeatpMapPositionHolding(Player p, string mapName, Team t, string folder = "Test_images") {
            Bitmap bitmap = new Bitmap("Maps/" + mapName + "_radar.png");
            Graphics g = Graphics.FromImage(bitmap);

            List<Death> listDeaths = p.AllDeaths;
            string playerName = p.Name;

            // First we find the max value
            int maxValue = 0;
            if (t == Team.CounterTerrorist) {
                maxValue = p.PositionHeatCTNoMove.Cast<int>().Max();
            }
            else {
                maxValue = p.PositionHeatTNoMove.Cast<int>().Max();
            }

            // iterate over the position of the player
            for (int i = 0; i < (int)(1024f / Player.sizeHeatMap) + 1; i++) {
                for (int j = 0; j < (int)(1024f / Player.sizeHeatMap) + 1; j++) {
                    int currentPosition;
                    if (t == Team.CounterTerrorist) {
                        currentPosition = p.PositionHeatCTNoMove[i, j];
                    }
                    else {
                        currentPosition = p.PositionHeatTNoMove[i, j];
                    }
                    Color c = ColorHeatMap.GetColorHeatMap(currentPosition, maxValue);
                    Brush b = new SolidBrush(c);
                    g.FillRectangle(b, i * Player.sizeHeatMap, j * Player.sizeHeatMap,
                        Player.sizeHeatMap, Player.sizeHeatMap);
                }
            }

            if (!Directory.Exists(folder + "/Holding")) {
                Directory.CreateDirectory(folder + "/Holding");
            }

            bitmap.Save(folder + "/Holding/HeatMap_" + mapName + "_" + p.Name + "_" + t + ".png", ImageFormat.Png);
        }
    }
}
