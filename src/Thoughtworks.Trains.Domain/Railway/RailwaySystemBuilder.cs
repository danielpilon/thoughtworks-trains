using System;
using System.Text.RegularExpressions;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Domain.Railway
{
    /// <summary>
    /// Creates new instances of <see cref="IRailwaySystem"/>.
    /// </summary>
    public class RailwaySystemBuilder
    {
        /// <summary>
        /// Gets a <see cref="Regex"/> that matches to comma-separated route sets.
        /// </summary>
        private Regex CommaSeparatedRouteSetRegex { get; } = new Regex("^(?<origin>[A-Z])(?<destination>[A-Z])(?<distance>[1-9][0-9]*)$", RegexOptions.Compiled);

        /// <summary>
        /// Builds a new instance of <see cref="IRailwaySystem"/> based on a comma-separated route set.
        /// </summary>
        /// <param name="routeSet">A comma-separated <see cref="string"/> with the routes and their respective distances.</param>
        /// <remarks>
        /// The expected format is a comma separated of values as {origin town name}{destination town name}{total distance}.
        /// For example: AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7.
        /// </remarks>
        /// <returns>A new instance of <see cref="IRailwaySystem"/>.</returns>
        public IRailwaySystem Build(string routeSet)
        {
            var railwaySystem = new RailwaySystem();
            foreach (var routeDefinition in routeSet.Split(','))
            {
                if (!CommaSeparatedRouteSetRegex.Match(routeDefinition).Success)
                    throw new ArgumentException($"Route {routeDefinition} is not in a valid format.", nameof(routeSet));
                var routeParts = CommaSeparatedRouteSetRegex.Match(routeDefinition).Groups;
                var origin = GetOrAddTownByName(railwaySystem, routeParts["origin"].Value);
                var destination = GetOrAddTownByName(railwaySystem, routeParts["destination"].Value);
                var distance = Convert.ToInt32(routeParts["distance"].Value);
                var route = new Route(origin, destination, distance);
                origin.AddRoute(route);
            }
            return railwaySystem;
        }

        /// <summary>
        /// Gets an instance of <see cref="Town"/> from the <paramref name="railwaySystem"/>, or creates a new one if not exists.
        /// </summary>
        /// <param name="railwaySystem">An instance of <see cref="RailwaySystem"/>.</param>
        /// <param name="townName">The name of the town to search.</param>
        /// <returns>An instance of <see cref="Town"/>.</returns>
        private Town GetOrAddTownByName(RailwaySystem railwaySystem, string townName)
        {
            if (railwaySystem.HasTown(townName))
                return railwaySystem.GetTownByName(townName) as Town;
            var town = new Town(townName);
            railwaySystem.AddTown(town);
            return town;
        }
    }
}