using System;

namespace TspGa
{
    class Program
    {
        static void Main(string[] args)
        {
            int pointsCount = 20;
            int mapWidth = 200;
            int mapHeight = 200;
            int populationSize = 50;
            int cycles = 100;
            double mutationRate = 0.01;

            Map map = new Map(pointsCount, mapWidth, mapHeight);
            Population population = new Population(populationSize, mutationRate);
            Console.ReadKey();
        }
    }
}
