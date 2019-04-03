using System;
using System.Collections.Generic;

namespace Thoughtworks.Trains.Domain
{
    public class Town : IEquatable<Town>
    {
        public Town(string name)
        {
            Name = name;
        }

        private Dictionary<string, Route> RoutesByName { get; } = new Dictionary<string, Route>();

        public string Name { get; }

        public IEnumerable<Route> Routes => RoutesByName.Values;

        public void AddRoute(Route route) => RoutesByName.Add(route.To.Name, route);

        public Route GetRouteByName(string name) =>
            RoutesByName.ContainsKey(name) ? RoutesByName[name] : throw new InvalidRouteException($"There's no such route that passes via '{name}' town.");

        public bool HasRoute(string name) => RoutesByName.ContainsKey(name);

        public bool Equals(Town other) => !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || string.Equals(Name, other.Name));

        public override bool Equals(object obj) =>
            !ReferenceEquals(null, obj) &&
            (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Town) obj));

        public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;

        public override string ToString() => Name;
    }

    public class InvalidRouteException : Exception
    {
        public InvalidRouteException(string s) : base(s)
        {

        }
    }
}