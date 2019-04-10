using System;
using System.Text.RegularExpressions;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Domain.Railway
{
    public class RailwaySystemBuilder : IRailwaySystemBuilder
    {
        private Regex RouteSetRegex { get; } = new Regex("^(?<origin>[A-Z])(?<destination>[A-Z])(?<distance>[1-9][0-9]*)$", RegexOptions.Compiled);

        public IRailwaySystem Build(string routeSet)
        {
            var railwaySystem = new RailwaySystem();
            foreach (var routeDefinition in routeSet.Split(','))
            {
                if (!RouteSetRegex.Match(routeDefinition).Success) throw new ArgumentException($"Route {routeDefinition} is not in a valid format.", nameof(routeSet));
                var routeParts = RouteSetRegex.Match(routeDefinition).Groups;
                var origin = GetOrAddTownByName(railwaySystem, routeParts["origin"].Value);
                var destination = GetOrAddTownByName(railwaySystem, routeParts["destination"].Value);
                var distance = Convert.ToInt32(routeParts["distance"].Value);
                var route = new Route(origin, destination, distance);
                origin.AddRoute(route);
            }
            return railwaySystem;
        }

        private Town GetOrAddTownByName(RailwaySystem railwaySystem, string townName)
        {
            if (railwaySystem.HasTown(townName))
                return railwaySystem.GetTownByName(townName) as Town;
            var town = new Town(townName);
            railwaySystem.AddTown(town);
            return town;
        }
    }
    public interface IRailwaySystemBuilder
    {
        IRailwaySystem Build(string routeSet);
    }
}