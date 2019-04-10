using System;
using System.Text.RegularExpressions;
using Thoughtworks.Trains.Domain.Railway;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Application.RailwaySystems
{
    public class RailwaySystemBuilder : IRailwaySystemBuilder
    {
        private Regex RouteSetRegex { get; } = new Regex("^(?<origin>[A-Z])(?<destination>[A-Z])(?<distance>[1-9][0-9]*)$", RegexOptions.Compiled);

        public RailwaySystem Build(string routeSet)
        {
            var railwaySystem = new Domain.Railway.RailwaySystem();
            foreach (var routeDefinition in routeSet.Split(','))
            {
                if (!RouteSetRegex.Match(routeDefinition).Success) throw new ArgumentException($"Route {routeDefinition} is not in a valid format.", nameof(routeSet));
                var routeParts = RouteSetRegex.Match(routeDefinition).Groups;
                var origin = railwaySystem.GetOrAddTownByName(routeParts["origin"].Value);
                var destination = railwaySystem.GetOrAddTownByName(routeParts["destination"].Value);
                var distance = Convert.ToInt32(routeParts["distance"].Value);
                var route = new Route(origin, destination, distance);
                origin.AddRoute(route);
            }
            return railwaySystem;
        }
    }
    public interface IRailwaySystemBuilder
    {
        RailwaySystem Build(string routeSet);
    }
}