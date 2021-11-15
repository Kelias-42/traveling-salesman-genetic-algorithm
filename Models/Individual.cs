using System;
using System.Collections.Generic;

namespace TspGa
{
    class Individual
    {
        private double _mutationRate;
        private IList<int> _orderedCities;
        public double totalDistance;
        public double fitnessScore;
        public Individual(double mutationRate, IList<int> orderedCities, Map map)
        {
            _mutationRate = mutationRate;
            _orderedCities = orderedCities;
            totalDistance = GetTotalDistance(map);
        }

        /// <summary>
        /// Determine whether to mutate the individual or not and apply the mutation if necessary
        /// </summary>
        public void Mutate()
        {
            Random random = new Random();

            for (int i = 0; i < _orderedCities.Count; i++)
            {
                if (random.NextDouble() <= _mutationRate)
                {
                    int swappedPosition = random.Next(_orderedCities.Count);
                    int tmp = _orderedCities[i];
                    _orderedCities[i] = _orderedCities[swappedPosition];
                    _orderedCities[swappedPosition] = tmp;
                }
            }
        }
        private double GetDistanceBetweenTwoPoints(MapPoint firstPoint, MapPoint secondPoint)
        {
            return Math.Sqrt((secondPoint.x - firstPoint.x) * (secondPoint.x - firstPoint.x) + (secondPoint.y - firstPoint.y) * (secondPoint.y - firstPoint.y));
        }
        public double GetTotalDistance(Map map)
        {
            double totalDistance = 0;
            for (int i = 0; i < _orderedCities.Count - 1; i++)
                totalDistance += GetDistanceBetweenTwoPoints(map.points[_orderedCities[i]], map.points[_orderedCities[i + 1]]);
            totalDistance += GetDistanceBetweenTwoPoints(map.points[_orderedCities[0]], map.points[_orderedCities[_orderedCities.Count - 1]]);
            return totalDistance;
        }
        public List<int> GetRandomCitySlice()
        {
            Random random = new Random();
            int pointA = random.Next(_orderedCities.Count);
            int pointB = random.Next(_orderedCities.Count);
            int start = Math.Min(pointA, pointB);
            int end = Math.Max(pointA, pointB);
            List<int> citySlice = new List<int>();

            for (int i = start; i <= end; i++)
            {
                citySlice.Add(_orderedCities[i]);
            }
            return citySlice;
        }
        public List<int> GetRemainingCities(List<int> cities)
        {
            foreach (int city in _orderedCities)
            {
                if (!cities.Contains(city))
                    cities.Add(city);
            }
            return cities; 
        }
    }
}
