using System;
using System.Collections.Generic;
using System.Linq;
using Thoughtworks.Trains.Application.Routes.Extensions;
using Thoughtworks.Trains.Domain.Railway;
using Thoughtworks.Trains.Domain.Towns;
using Thoughtworks.Trains.Domain.Towns.Exceptions;

namespace Thoughtworks.Trains.Application.Routes
{
    public class RouteService
    {
        public static int ResolveDistance(IPath path)
        {
            var distance = 0;
            for (var i = 0; i < path.Towns.Count() - 1; i++)
            {
                var fromTown = path.Towns.ElementAt(i);
                var toTown = path.Towns.ElementAt(i + 1);

                distance += fromTown
                    .GetRouteByName(toTown.Name)
                    .Distance;
            }

            return distance;
        }

        public static int ResolveTripsWithMaxStops(Town from, Town to, int maxStops)
        {
            if (maxStops == 1)
                return from.HasRoute(to.Name) ? 1 : 0;

            var trips = 0;
            var currentDepth = 0;
            var queue = new Queue<Town>();
            queue.Enqueue(from);
            queue.Enqueue(null);
            while (queue.Count > 1 && currentDepth <= maxStops)
            {
                var town = queue.Dequeue();
                if (town == null)
                {
                    currentDepth++;
                    queue.Enqueue(null);
                    continue;
                }

                if (town.HasRoute(to.Name))
                    trips++;

                foreach (var route in town.Routes) queue.Enqueue(route.To);
            }

            return trips;
        }

        public static int ResolveTripsWithExactNumberOfStops(Town from, Town to, int stops)
        {
            if (stops == 1)
                return from.HasRoute(to.Name) ? 1 : 0;

            var trips = 0;
            var currentDepth = 0;
            var queue = new Queue<Town>();
            queue.Enqueue(from);
            queue.Enqueue(null);
            while (queue.Count > 1 && currentDepth <= stops)
            {
                var town = queue.Dequeue();
                if (town == null)
                {
                    currentDepth++;
                    queue.Enqueue(null);
                    continue;
                }

                if (town.HasRoute(to.Name) && currentDepth == stops)
                    trips++;

                foreach (var route in town.Routes) queue.Enqueue(route.To);
            }

            return trips;
        }

        //public static int ResolveShortestDistance(RailwaySystem railwaySystem, Town from, Town to)
        //{
        //    var distances = new Dictionary<Town, int>();
        //    var actualTowns = railwaySystem.GetTowns() as List<Town> ?? railwaySystem.GetTowns().ToList();
 
        //    foreach (var town in actualTowns) distances[town] = int.MaxValue;
        //    foreach (var adjacent in railwaySystem.GetRoutesByTown(from)) distances[adjacent.To] = adjacent.Distance;

        //    while (actualTowns.Count() != 0)
        //    {
        //        var actualShortest = actualTowns.OrderBy(t => distances[t]).First();
        //        actualTowns.Remove(actualShortest);

        //        if (distances[actualShortest] == int.MaxValue) break;

        //        if (actualShortest.Equals(to)) return distances[actualShortest];

        //        foreach (var route in railwaySystem.GetRoutesByTown(actualShortest))
        //        {
        //            var actualDistance = distances[actualShortest] + route.Distance;
        //            if (actualDistance >= distances[route.To]) continue;
        //            distances[route.To] = actualDistance;
        //        }
        //    }

        //    throw new InvalidRouteException($"There's no such route from '{from}' to '{to}'.");
        //}

        public static IResolvedPath ResolveShortestDistance(RailwaySystem railwaySystem, Town from, Town to)
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
                var actualShortest = actualTowns.OrderBy(t => distances[t]).First();
                actualTowns.Remove(actualShortest);

                if (distances[actualShortest] == int.MaxValue) break;

                if (actualShortest.Equals(to))
                {
                    var path = new ResolvedPath(from);
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

        public static int ResolveRoutesWithDistanceLessThan(Town from, Town to, int maxDistance)
        {
            throw new NotImplementedException();
        }
    }
}
