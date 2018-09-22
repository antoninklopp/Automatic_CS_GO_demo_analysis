﻿using System;
using System.IO;
using DemoInfo;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CS_GO_Analysis {
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

            bitmap.Save(@"Test_images/test" + roundNumber.ToString()  + ".png", ImageFormat.Png);
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

            bitmap.Save(@"Test_images/test2-" + roundNumber.ToString() + ".png", ImageFormat.Png);
        }
    }
}
