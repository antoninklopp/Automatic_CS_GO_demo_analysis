using DemoInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CS_GO_Analysis
{
    public class PositionName
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("position")]
        public Vector Position;

        public PositionName(string name, Vector position)
        {
            Name = name;
            Position = position;
        }

        public double Distance(Vector _position)
        {
            return Math.Sqrt(Math.Pow((Position.X - _position.X), 2) + Math.Pow((Position.Y - _position.Y), 2)); 
        }
    }

    public class MapPositions
    {
        [JsonProperty("positions")]
        public List<PositionName> Positions; 

        public MapPositions(string mapName)
        {
            LoadMapPositions(mapName); 
        }

        public void LoadMapPositions(string mapName)
        {
            Positions = JsonConvert.DeserializeObject<MapPositions>(File.ReadAllText($"cameras/{mapName}_cameras.json")).Positions;
        }

        public string GetClosestPosition(Vector position)
        {
            double minDistance = double.MaxValue;
            PositionName closestPosition = Positions[0]; 
            foreach (var mapPosition in Positions)
            {
                var distance = mapPosition.Distance(position); 
                if (distance < minDistance)
                {
                    closestPosition = mapPosition;
                    minDistance = distance; 
                }
            }
            return closestPosition.Name; 
        }


    }
}
