using System;

namespace TspGa
{
    class MapPoint
    {
        public int x;
        public int y;
        public MapPoint(int width, int height, int seed)
        {
            Random random = new Random(seed);
            x = random.Next(0, width);
            y = random.Next(0, height);
        }
    }
}
