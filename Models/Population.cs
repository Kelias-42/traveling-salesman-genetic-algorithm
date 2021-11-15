using System;
using System.Collections.Generic;
using System.Linq;

namespace TspGa
{
    class Population
    {
        private List<Individual> currentGeneration = new List<Individual>();
        public Population(int populationSize, double mutationRate, Map map)
        {
            Random random = new Random();

            // Initializes each individual of the population with the mutation rate and a random order for the cities
            for (int i = 0; i < populationSize; i++)
            {
                currentGeneration.Add(new Individual(mutationRate, Enumerable.Range(0, map.points.Count).OrderBy(item => random.Next()).ToList(), map));
            }
        }
        private Individual SelectParent(List<Individual> population)
        {
            Individual parent = null;
            Random random = new Random();
            bool accept = false;
            
            while (!accept)
            {
                parent = population[random.Next(population.Count)];
                if (random.NextDouble() <= parent.fitnessScore)
                    accept = true;
            }

            return parent;
        }
        private Individual GenerateOffspring(Individual firstParent, Individual secondParent, double mutationRate, Map map)
        {
            List<int> offspringCities = secondParent.GetRemainingCities(firstParent.GetRandomCitySlice());
            Individual offspring = new Individual(mutationRate, offspringCities, map);

            offspring.Mutate();

            return offspring;
        }
        public double CreateNextGeneration(double mutationRate, Map map, int eliteCount)
        {
            // Assign fitness score to each individual
            double maxDistance = currentGeneration.Max(x => x.totalDistance);
            currentGeneration.ForEach(x => x.fitnessScore = 1 - x.totalDistance / maxDistance);

            double shortestDistance = currentGeneration.Min(x => x.totalDistance);

            List<Individual> nextGeneration = new List<Individual>();

            if (eliteCount > 0)
            {
                nextGeneration.AddRange(currentGeneration.OrderBy(x => x.totalDistance).ToList().Take(eliteCount));
                for (int i = 0; i < nextGeneration.Count; i++)
                    nextGeneration[i].Mutate();
            }

            while (nextGeneration.Count != currentGeneration.Count)
                nextGeneration.Add(GenerateOffspring(SelectParent(currentGeneration), SelectParent(currentGeneration), mutationRate, map));
            
            currentGeneration = nextGeneration;
            
            return shortestDistance;
        }
    }
}
