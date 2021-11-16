using System;
using System.Collections.Generic;

namespace TspGa
{
    class Individual
    {
        private double _mutationRate;
        private List<int> _orderedCities;
        public double totalDistance;
        public double fitnessScore;
        public double fitnessScoreCumSum;
        public Individual(double mutationRate, List<int> orderedCities, Map map)
        {
            _mutationRate = mutationRate;
            _orderedCities = orderedCities;
            totalDistance = GetTotalDistance(map);
            fitnessScore = 1 / totalDistance;
        }

        /// <summary>
        /// Determine whether to mutate the individual or not and apply the mutation if necessary
        /// </summary>
        public void Mutate()
        {
            Random random = new Random();

            for (int i = 1; i < _orderedCities.Count - 1; i++)
            {
                if (random.NextDouble() <= _mutationRate)
                {
                    int swappedPosition = random.Next(1, _orderedCities.Count - 1);
                    int tmp = _orderedCities[i];
                    _orderedCities[i] = _orderedCities[swappedPosition];
                    _orderedCities[swappedPosition] = tmp;
                }
            }
        }
        private double GetDistanceBetweenTwoPoints(MapPoint firstPoint, MapPoint secondPoint)
        {
            return Math.Sqrt(Math.Pow(secondPoint.x - firstPoint.x, 2) + Math.Pow(secondPoint.y - firstPoint.y, 2));
        }
        public double GetTotalDistance(Map map)
        {
            double totalDistance = 0;
            for (int i = 0; i < _orderedCities.Count - 1; i++)
                totalDistance += GetDistanceBetweenTwoPoints(map.points[_orderedCities[i]], map.points[_orderedCities[i + 1]]);
            return totalDistance;
        }
        public List<int> GetRandomCitySlice()
        {
            Random random = new Random();
            int pointA = random.Next(1, _orderedCities.Count - 1);
            int pointB = random.Next(1, _orderedCities.Count - 1);
            int start = Math.Min(pointA, pointB);
            int end = Math.Max(pointA, pointB);
            List<int> citySlice = new List<int>();

            for (int i = start; i <= end; i++)
                citySlice.Add(_orderedCities[i]);
            return citySlice;
        }
        public List<int> GetRemainingCities(List<int> cities)
        {
            cities.Insert(0, 0);
            foreach (int city in _orderedCities)
                if (!cities.Contains(city) && city != 0)
                    cities.Add(city);
            cities.Add(0);

            return cities; 
        }
        public List<int> GetOrderedCities()
        {
            return _orderedCities;
        }
    }
}
