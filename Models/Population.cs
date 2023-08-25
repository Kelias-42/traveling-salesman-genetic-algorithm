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
                List<int> randomlyOrderedCities = new List<int> { 0 };
                randomlyOrderedCities.AddRange(Enumerable.Range(1, map.points.Count - 1).OrderBy(item => random.Next()));
                randomlyOrderedCities.Add(0);
                currentGeneration.Add(new Individual(mutationRate, randomlyOrderedCities, map));
            }
            AssignFitnessCumSum();
        }
        private void AssignFitnessCumSum()
        {
            double cumSum = 0;
            for (int i = 0; i < currentGeneration.Count; i++)
            {
                currentGeneration[i].fitnessScoreCumSum = cumSum;
                cumSum += currentGeneration[i].fitnessScore;
            }
        }
        private Individual GetParent(List<Individual> population)
        {
            Random random = new Random();
            Individual parent = null;
            double cumSumScale = population.Max(x => x.fitnessScoreCumSum);
            List<Individual> possibleParents = population.Where(x => x.fitnessScoreCumSum <= random.NextDouble() * cumSumScale).ToList();
            double biggestValidScore = -1;

            foreach (Individual individual in possibleParents)
            {
                if (individual.fitnessScoreCumSum > biggestValidScore)
                {
                    biggestValidScore = individual.fitnessScoreCumSum;
                    parent = individual;
                }
            }

            return parent;
        }
        private List<Individual> SelectParents(int parentCount, List<Individual> population)
        {
            List<Individual> parents = new List<Individual>();

            while (parents.Count != parentCount)
            {
                Individual parent = GetParent(population);
                if (!parents.Contains(parent))
                    parents.Add(parent);
            }

            return parents;
        }
        private Individual GenerateOffspring(List<Individual> parents, double mutationRate, Map map)
        {
            List<int> offspringCities = parents[0].GetRemainingCities(parents[1].GetRandomCitySlice());
            Individual offspring = new Individual(mutationRate, offspringCities, map);

            offspring.Mutate();

            return offspring;
        }
        public Individual CreateNextGeneration(double mutationRate, Map map, int eliteCount)
        {
            Individual bestPerformingIndividual;
            List<Individual> nextGeneration = new List<Individual>();

            if (eliteCount > 0)
            {
                nextGeneration.AddRange(currentGeneration.OrderBy(x => x.totalDistance).ToList().Take(eliteCount));
                for (int i = 0; i < nextGeneration.Count; i++)
                    nextGeneration[i].Mutate();
            }

            while (nextGeneration.Count != currentGeneration.Count)
                nextGeneration.Add(GenerateOffspring(SelectParents(2, currentGeneration), mutationRate, map));
            
            currentGeneration = nextGeneration;
            AssignFitnessCumSum();
            bestPerformingIndividual = currentGeneration.OrderBy(x => x.totalDistance).First();

            return bestPerformingIndividual;
        }
    }
}
