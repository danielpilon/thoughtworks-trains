using System.Collections.Generic;
using System.Linq;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Application.Trips
{
    internal sealed class Trip : ITrip
    {

        public Trip(Town origin)
        {
            Origin = origin;
        }

        private List<Route> InnerRoutes { get; } = new List<Route>();

        public Town Origin { get; }

        public IEnumerable<Route> Routes => InnerRoutes;

        public int TotalDistance { get; private set; }

        public int Stops { get; private set; }

        public void AddRoute(Route route)
        {
            InnerRoutes.Add(route);
            TotalDistance += route.Distance;
            Stops++;
        }

        public override string ToString() => $"{Origin.Name}->{string.Join("->", Routes.SelectMany(_ => _.To.Name))}";
    }
}