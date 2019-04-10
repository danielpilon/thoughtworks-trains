using System.Collections.Generic;
using System.Linq;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Application.Trips
{
    /// <summary>
    /// Gathers the information of a Trip between an origin and a destination town.
    /// </summary>
    internal sealed class Trip : ITrip
    {
        /// <summary>
        /// Creates a new instance of <see cref="Trip"/>.
        /// </summary>
        /// <param name="origin">The town from which the trip originates.</param>
        public Trip(ITown origin) => Origin = origin;

        /// <summary>
        /// Gets a <see cref="Stack{T}"/> of <see cref="Routes"/>
        /// </summary>
        private Stack<Route> InnerRoutes { get; } = new Stack<Route>();

        /// <summary>
        /// An instance of <see cref="ITown"/> from where the trip originates.
        /// </summary>
        public ITown Origin { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="Route"/> with the sequence of routes that defines this tirp.
        /// </summary>
        public IEnumerable<Route> Routes => InnerRoutes;

        /// <inheritdoc />
        public int TotalDistance { get; private set; }

        /// <inheritdoc />
        public int Stops { get; private set; }

        /// <summary>
        /// Adds a new <see cref="Route"/> to this trip.
        /// </summary>
        /// <param name="route">An instance of <see cref="Route"/>.</param>
        public void AddRoute(Route route)
        {
            InnerRoutes.Push(route);
            TotalDistance += route.Distance;
            Stops++;
        }

        /// <summary>
        /// Removes the last of the route from this trip.
        /// </summary>
        public void RemoveLast()
        {
            var route = InnerRoutes.Pop();
            TotalDistance -= route.Distance;
            Stops--;
        }

        /// <inheritdoc />
        public override string ToString() => $"{Origin.Name}->{string.Join("->", Routes.SelectMany(_ => _.To.Name))}";
    }
}