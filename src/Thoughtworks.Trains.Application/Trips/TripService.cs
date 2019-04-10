using System;
using System.Collections.Generic;
using System.Linq;
using Thoughtworks.Trains.Application.Paths;
using Thoughtworks.Trains.Domain.Railway;
using Thoughtworks.Trains.Domain.Towns;
using Thoughtworks.Trains.Domain.Towns.Exceptions;
using Thoughtworks.Trains.Domain.Towns.Extensions;

namespace Thoughtworks.Trains.Application.Trips
{
    /// <summary>
    /// Provides information about a Trip and its routes.
    /// </summary>
    public class TripService
    {
        /// <summary>
        /// Finds a <see cref="ITrip"/> between two <see cref="Town"/>s based on the conditions provided.
        /// </summary>
        /// <param name="from">The <see cref="ITown"/> from where the trip begins.</param>
        /// <param name="to">The <see cref="ITown"/> to where the trip ends.</param>
        /// <param name="stopCondition">
        /// A <see cref="Func{TResult}"/> that receives an <see cref="ITrip"/> and returns a <see cref="bool"/>
        /// that will determine the condition to stop looking for routes in this trip.
        /// </param>
        /// <param name="matchCondition">
        /// (optional) A <see cref="Func{TResult}"/> that receives an <see cref="ITrip"/> and returns a <see cref="bool"/>
        /// that will add a new condition to consider a route as valid. </param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="stopCondition"/> is null.</exception>
        /// <returns>An instance of <see cref="IEnumerable{T}"/> of <see cref="ITrip"/> with the trips found.</returns>
        public IEnumerable<ITrip> Search(ITown from, ITown to, Func<ITrip, bool> stopCondition, Func<ITrip, bool> matchCondition = null)
        {
            if (stopCondition == null)
                throw new ArgumentNullException(nameof(stopCondition));

            var trips = new List<ITrip>();

            void SearchPaths(ITown current, Trip currentTrip = null)
            {
                if (currentTrip == null) currentTrip = new Trip(current);
                if (stopCondition(currentTrip)) return;

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

        /// <summary>
        /// Calculates the total distance based on a <see cref="IPath"/>.
        /// </summary>
        /// <param name="path">An instance of <see cref="IPath"/>.</param>
        /// <returns>An <see cref="int"/> with the total distance of this <paramref name="path"/>.</returns>
        public int ResolveDistance(IPath path)
        {
            var distance = 0;
            for (var i = 0; i < path.Stops.Count() - 1; i++)
            {
                var fromTown = path.Stops.ElementAt(i);
                var toTown = path.Stops.ElementAt(i + 1);
                distance += fromTown.GetRoute(toTown).Distance;
            }
            return distance;
        }

        /// <summary>
        /// Finds the shortest possible <see cref="ITrip"/> between two <see cref="ITown"/>s.
        /// </summary>
        /// <param name="railwaySystem">An instance of <see cref="IRailwaySystem"/>.</param>
        /// <param name="from">The <see cref="ITown"/> from where the trip begins.</param>
        /// <param name="to">The <see cref="ITown"/> to where the trip ends.</param>
        /// <exception cref="InvalidRouteException">Thrown whenever no <see cref="ITrip"/> between the two <see cref="ITown"/>s exists.</exception>
        /// <returns>The instance of the shortest <see cref="ITrip"/> found.</returns>
        public ITrip FindShortest(IRailwaySystem railwaySystem, ITown from, ITown to)
        {
            var shortestPath = new Dictionary<ITown, Route>();
            var distances = new Dictionary<ITown, int>();
            var actualTowns = railwaySystem.GetTowns() as List<ITown> ?? railwaySystem.GetTowns().ToList();

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