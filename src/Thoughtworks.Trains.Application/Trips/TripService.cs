using System;
using System.Collections.Generic;
using System.Linq;
using Thoughtworks.Trains.Application.Trips.Extensions;
using Thoughtworks.Trains.Domain.Railway;
using Thoughtworks.Trains.Domain.Towns;
using Thoughtworks.Trains.Domain.Towns.Exceptions;

namespace Thoughtworks.Trains.Application.Trips
{
    public class TripService
    {
        public static int ResolveDistance(IPath path)
        {
            var distance = 0;
            for (var i = 0; i < path.Towns.Count() - 1; i++)
            {
                var fromTown = path.Towns.ElementAt(i);
                var toTown = path.Towns.ElementAt(i + 1);

                distance += fromTown
                    .GetRoute(toTown)
                    .Distance;
            }

            return distance;
        }

        public static IEnumerable<ITrip> ResolveTripsUpToMaxStops(Town from, Town to, int maxStops)
        {
            if (maxStops == 0) return Enumerable.Empty<ITrip>();
            if (maxStops == 1 && from.HasRoute(to))
            {
                if (!from.HasRoute(to)) return Enumerable.Empty<ITrip>();
                var path = new Trip(from);
                path.AddRoute(from.GetRoute(to));
                return new[] { path };
            }

            var trips = new List<ITrip>();
            var currentPath = new List<Route>();
            void SearchPaths(Town current)
            {
                if (currentPath.Count > maxStops)
                    return;

                if (current.Equals(to) && currentPath.Count > 0)
                {
                    var trip = new Trip(from);
                    foreach (var route in currentPath) trip.AddRoute(route);
                    trips.Add(trip);
                }

                foreach (var route in current.Routes)
                {
                    currentPath.Add(route);
                    SearchPaths(route.To);
                    currentPath.Remove(route);
                }
            }

            SearchPaths(from);
            return trips;
        }

        public static ITrip ResolveShortestTrip(RailwaySystem railwaySystem, Town from, Town to)
        {
            var shortestPath = new Dictionary<Town, Route>();
            var distances = new Dictionary<Town, int>();
            var actualTowns = railwaySystem.GetTowns() as List<Town> ?? railwaySystem.GetTowns().ToList();

            foreach (var town in actualTowns) distances[town] = town.Equals(from) ? 0 : int.MaxValue;
            if (from.CloneIfEquals(to, out var clonedFrom, $"{from.Name}_"))
            {
                actualTowns.Add(clonedFrom);
                distances[to] = int.MaxValue;
                distances[clonedFrom] = 0;
            }

            while (actualTowns.Count() != 0)
            {
                // TODO: This is bad. Should use a Binary Heap instead, but .NET doesn't have one by default like Java's Priority Queue.
                var actualShortest = actualTowns.OrderBy(t => distances[t]).First();
                actualTowns.Remove(actualShortest);

                if (distances[actualShortest] == int.MaxValue) break;

                if (actualShortest.Equals(to))
                {
                    var path = new Trip(from);
                    while (shortestPath.ContainsKey(actualShortest))
                    {
                        path.AddRoute(shortestPath[actualShortest]);
                        actualShortest = shortestPath[actualShortest].From;
                    }

                    return path;
                }

                foreach (var route in actualShortest.Routes)
                {
                    var actualDistance = distances[actualShortest] + route.Distance;
                    if (actualDistance >= distances[route.To]) continue;
                    distances[route.To] = actualDistance;
                    shortestPath[route.To] = route;
                }
            }

            throw new InvalidRouteException($"There's no such route from '{from}' to '{to}'.");
        }

        public static IEnumerable<ITrip> ResolveRoutesWithDistanceLessThan(Town from, Town to, int maxDistance)
        {
            throw new NotImplementedException();
        }
    }
}
