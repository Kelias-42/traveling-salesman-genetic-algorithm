using System;
using System.Collections.Generic;
using System.Linq;

namespace TspGa
{
    class Program
    {
        private static void SavePerformanceHistoryPlot(double[] generationIndex, double[] shortestDistanceHistory, int eliteCount)
        {
            var plt = new ScottPlot.Plot(1600, 900);
            plt.AddScatter(generationIndex, shortestDistanceHistory);
            plt.SaveFig(string.Format("{0}.png", eliteCount == 0 ? "plot_no_elite" : "plot_with_" + eliteCount.ToString() + "_elites"));
        }
        private static void SaveSolutionImprovementHistoryPlot(Map map, List<List<int>> solutionHistory)
        {
            var plt = new ScottPlot.Plot(1600, 900);
            for (int i = 0; i < solutionHistory.Count; i++)
            {
                double[] xPlot = new double[solutionHistory[i].Count];
                double[] yPlot = new double[solutionHistory[i].Count];
                for (int j = 0; j < solutionHistory[i].Count; j++)
                {
                    xPlot[j] = map.points[solutionHistory[i][j]].x;
                    yPlot[j] = map.points[solutionHistory[i][j]].y;
                }
                plt.AddScatter(xPlot, yPlot);
                plt.SaveFig(string.Format("solution_history/improvement_{0}.png", i));
                plt.Clear();
            }
        }
        static void Main(string[] args)
        {
            int pointsCount = 12;
            int mapWidth = 25;
            int mapHeight = 25;
            int populationSize = 100;
            int generationCount = 2500;
            int eliteCount = 20;
            double mutationRate = 0.015;

            double[] generationIndex = new double[generationCount];
            double[] shortestDistanceHistory = new double[generationCount];
            List<List<int>> solutionHistory = new List<List<int>>();

            Map map = new Map(pointsCount, mapWidth, mapHeight);
            Population population = new Population(populationSize, mutationRate, map);
            double bestScore = 0;

            for (int i = 0; i < generationCount; i++)
            {
                Individual bestPerformingIndividual = population.CreateNextGeneration(mutationRate, map, eliteCount);
                shortestDistanceHistory[i] = bestPerformingIndividual.totalDistance;
                generationIndex[i] = i;
                if (bestScore < bestPerformingIndividual.fitnessScore)
                {
                    bestScore = bestPerformingIndividual.fitnessScore;
                    solutionHistory.Add(bestPerformingIndividual.GetOrderedCities().ToList());
                    Console.Write(string.Format("{0} {1} {2}", i, bestPerformingIndividual.fitnessScore, bestPerformingIndividual.totalDistance));
                    foreach (int city in bestPerformingIndividual.GetOrderedCities())
                        Console.Write(string.Format(" {0}", city));
                    Console.WriteLine();
                }
            }
            SavePerformanceHistoryPlot(generationIndex, shortestDistanceHistory, eliteCount);
            SaveSolutionImprovementHistoryPlot(map, solutionHistory);
        }
    }
}
