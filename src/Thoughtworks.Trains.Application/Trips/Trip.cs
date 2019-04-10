using System.Collections.Generic;
using System.Linq;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Application.Trips
{
    internal sealed class Trip : ITrip
    {
        public Trip(ITown origin)
        {
            Origin = origin;
        }

        private Stack<Route> InnerRoutes { get; } = new Stack<Route>();
        
        public ITown Origin { get; }

        public IEnumerable<Route> Routes => InnerRoutes;

        public int TotalDistance { get; private set; }

        public int Stops { get; private set; }

        public void AddRoute(Route route)
        {
            InnerRoutes.Push(route);
            TotalDistance += route.Distance;
            Stops++;
        }

        public void RemoveLast()
        {
            var route = InnerRoutes.Pop();
            TotalDistance -= route.Distance;
            Stops--;
        }

        public override string ToString() => $"{Origin.Name}->{string.Join("->", Routes.SelectMany(_ => _.To.Name))}";
    }
}