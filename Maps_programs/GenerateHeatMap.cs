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
                if (p.TeamName == Team.CounterTerrorist) {
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
        public static void GenerateDeathsPlayer(Player p, string mapName) {
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

            bitmap.Save(@"Test_images/" + mapName + "_" + playerName + ".png", ImageFormat.Png);
        }

        public static void GenerateHeatpMapPosition(Player p, string mapName, Team t) {
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

            bitmap.Save(@"Test_images/HeatMap_" + mapName + "_" + p.Name + "_" + t + ".png", ImageFormat.Png);
        }
    }
}
