using System;
using System.Collections.Generic;

namespace TspGa
{
    class Individual
    {
        private double _mutationRate;
        private IList<int> _orderedPoints;
        public double totalDistance;
        public Individual(double mutationRate, IList<int> orderedPoints, Map map)
        {
            _mutationRate = mutationRate;
            _orderedPoints = orderedPoints;
            totalDistance = GetTotalDistance(map);
        }

        /// <summary>
        /// Determine whether to mutate the individual or not and apply the mutation if necessary
        /// </summary>
        public void TryMutate()
        {
            Random random = new Random();
            if (random.NextDouble() <= _mutationRate)
            {
                // Get the position of the elements that will be swapped
                int firstSwapped = random.Next(_orderedPoints.Count);
                int secondSwapped = random.Next(_orderedPoints.Count - 1);
                if (secondSwapped == firstSwapped)
                    secondSwapped = _orderedPoints.Count - 1;
                // Swap the elements
                int tmp = _orderedPoints[firstSwapped];
                _orderedPoints[firstSwapped] = _orderedPoints[secondSwapped];
                _orderedPoints[secondSwapped] = tmp;
            }
        }
        private double GetDistanceBetweenTwoPoints(MapPoint firstPoint, MapPoint secondPoint)
        {
            return Math.Sqrt((secondPoint.x - firstPoint.x) * (secondPoint.x - firstPoint.x) + (secondPoint.y - firstPoint.y) * (secondPoint.y - firstPoint.y));
        }
        public double GetTotalDistance(Map map)
        {
            double totalDistance = 0;
            for (int i = 0; i < _orderedPoints.Count - 1; i++)
                totalDistance += GetDistanceBetweenTwoPoints(map.points[_orderedPoints[i]], map.points[_orderedPoints[i + 1]]);
            totalDistance += GetDistanceBetweenTwoPoints(map.points[_orderedPoints[0]], map.points[_orderedPoints[-1]]);
            return totalDistance;
        }
    }
}
