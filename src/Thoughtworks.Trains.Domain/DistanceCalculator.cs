using System.Collections.Generic;
using System.Linq;

namespace Thoughtworks.Trains.Domain
{
    public class DistanceCalculator
    {
        public static int ResolveDistance(Path path)
        {
            var distance = 0;
            for (var i = 0; i < path.Routes.Count() - 1; i++)
            {
                var fromStation = path.Routes.ElementAt(i);
                var toStation = path.Routes.ElementAt(i + 1);

                distance += fromStation
                    .GetDestinationByName(toStation.Name)
                    .Distance;
            }

            return distance;
        }

        public static int ResolveTripsWithMaxStops(Station from, Station to, int maxStops)
        {
            if (maxStops == 1)
                return from.HasDestination(to.Name) ? 1 : 0;

            var trips = 0;
            var currentDepth = 0;
            var queue = new Queue<Station>();
            queue.Enqueue(from);
            queue.Enqueue(null);
            while (queue.Count > 1 && currentDepth < maxStops)
            {
                var station = queue.Dequeue();
                if (station == null)
                {
                    currentDepth++;
                    queue.Enqueue(null);
                    continue;
                }

                if (station.HasDestination(to.Name))
                    trips++;

                foreach (var destination in station.Destinations) queue.Enqueue(destination.To);
            }

            return trips;
        }
    }
}
