using System;
using System.Collections.Generic;
using System.Linq;

namespace TspGa
{
    class Population
    {
        private List<Individual> population;
        public Population(int populationSize, double mutationRate)
        {
            Random random = new Random();

            // Initializes each individual of the population with the mutation rate and a random order for the cities
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new Individual(mutationRate, Enumerable.Range(0, populationSize).OrderBy(item => random.Next()).ToList()));
            
            }
        }
    }
}
