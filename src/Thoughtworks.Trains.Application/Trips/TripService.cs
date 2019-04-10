using System;
using System.Collections.Generic;
using System.Linq;
using Thoughtworks.Trains.Application.Extensions;
using Thoughtworks.Trains.Application.Paths;
using Thoughtworks.Trains.Domain.Railway;
using Thoughtworks.Trains.Domain.Towns;
using Thoughtworks.Trains.Domain.Towns.Exceptions;

namespace Thoughtworks.Trains.Application.Trips
{
    public class TripService
    {
        public IEnumerable<ITrip> Search(Town from, Town to, Func<ITrip, bool> stopSearching, Func<ITrip, bool> matchCondition = null)
        {
            var trips = new List<ITrip>();

            void SearchPaths(Town current, Trip currentTrip = null)
            {
                if (currentTrip == null) currentTrip = new Trip(current);
                if (stopSearching(currentTrip)) return;

                if (current.Equals(to) && currentTrip.Routes.Any() && (matchCondition == null || matchCondition(currentTrip)))
                {
                    var trip = new Trip(from);
                    foreach (var route in currentTrip.Routes) trip.AddRoute(route);
                    trips.Add(trip);
                }

                foreach (var route in current.Routes)
                {
                    currentTrip.AddRoute(route);
                    SearchPaths(route.To, currentTrip);
                    currentTrip.RemoveLast();
                }
            }

            SearchPaths(from);
            return trips;
        }

        public int ResolveDistance(IPath path)
        {
            var distance = 0;
            for (var i = 0; i < path.Towns.Count() - 1; i++)
            {
                var fromTown = path.Towns.ElementAt(i);
                var toTown = path.Towns.ElementAt(i + 1);
                distance += fromTown.GetRoute(toTown).Distance;
            }
            return distance;
        }

        public ITrip FindShortest(RailwaySystem railwaySystem, Town from, Town to)
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
                // This is bad. Should use a Binary Heap instead, but .NET doesn't have one by default like Java's Priority Queue.
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
    }
}
