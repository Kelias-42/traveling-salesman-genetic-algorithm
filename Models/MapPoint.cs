using System;

namespace TspGa
{
    class MapPoint
    {
        public int x;
        public int y;
        public int pointNumber;
        public MapPoint(int width, int height, int index)
        {
            Random random = new();
            x = random.Next(0, width);
            y = random.Next(0, height);
            pointNumber = index;
        }
    }
}
