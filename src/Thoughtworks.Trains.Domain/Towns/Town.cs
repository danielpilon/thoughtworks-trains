using System;
using System.Collections.Generic;
using Thoughtworks.Trains.Domain.Towns.Exceptions;

namespace Thoughtworks.Trains.Domain.Towns
{
    public class Town : IEquatable<Town>
    {
        public Town(string name)
        {
            Name = name;
        }

        private Dictionary<Town, Route> RoutesByDestinationTown { get; } = new Dictionary<Town, Route>();

        public string Name { get; }

        public IEnumerable<Route> Routes => RoutesByDestinationTown.Values;

        public void AddRoute(Route route) => RoutesByDestinationTown.Add(route.To, route);

        public Route GetRoute(Town to) =>
            RoutesByDestinationTown.ContainsKey(to) ? RoutesByDestinationTown[to] : throw new InvalidRouteException($"There's no such route that passes via '{to}' town.");

        public bool HasRoute(Town to) => RoutesByDestinationTown.ContainsKey(to);

        public bool Equals(Town other) => !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || string.Equals(Name, other.Name));

        public override bool Equals(object obj) =>
            !ReferenceEquals(null, obj) &&
            (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Town) obj));

        public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;

        public override string ToString() => Name;
    }
}