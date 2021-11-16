using System.Collections.Generic;
using System.Linq;

namespace TspGa
{
    class Map
    {
        public List<MapPoint> points = new List<MapPoint>();
        public Map(int numberOfPoints, int mapWidth, int mapHeight)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                points.Add(new MapPoint(mapWidth, mapHeight, i));
            }
            points = points.OrderBy(point => point.x).ToList();
        }
    }
}
