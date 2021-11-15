using System;
using System.Collections.Generic;
using System.Linq;

namespace TspGa
{
    class Program
    {
        static void Main(string[] args)
        {
            int pointsCount = 25;
            int mapWidth = 200;
            int mapHeight = 200;
            int populationSize = 100;
            int generationCount = 500;
            int eliteCount = 20;
            double mutationRate = 0.02;

            double[] generationIndex = new double[generationCount];
            double[] shortestDistanceHistory = new double[generationCount];
            var plt = new ScottPlot.Plot(1600, 900);

            Map map = new Map(pointsCount, mapWidth, mapHeight);
            Population population = new Population(populationSize, mutationRate, map);
            for (int i = 0; i < generationCount; i++)
            {
                double shortestDistance = population.CreateNextGeneration(mutationRate, map, eliteCount);
                shortestDistanceHistory[i] = shortestDistance;
                generationIndex[i] = i;
            }

            plt.AddScatter(generationIndex, shortestDistanceHistory);
            plt.SaveFig(string.Format("{0}.png", eliteCount == 0 ? "plot_no_elite" : "plot_with_" + eliteCount.ToString() +"_elites"));
        }
    }
}
