﻿using System;
using System.Collections.Generic;
using Thoughtworks.Trains.Domain.Towns.Exceptions;

namespace Thoughtworks.Trains.Domain.Towns
{
    /// <summary>
    /// Represents a town in a railway system.
    /// </summary>
    internal sealed class Town : ITown
    {
        /// <summary>
        /// Creates a new instance of <see cref="Town"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="name"/> is null or whitespace.</exception>
        /// <param name="name">A <see cref="string"/> representing the town name.</param>
        internal Town(string name) => Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey,TValue}"/> of <see cref="Town"/> and <see cref="Route"/>
        /// with the instance of <see cref="Route"/> for a given destination <see cref="Town"/>.
        /// </summary>
        private Dictionary<ITown, Route> RoutesByDestinationTown { get; } = new Dictionary<ITown, Route>();

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public IEnumerable<Route> Routes => RoutesByDestinationTown.Values;

        /// <summary>
        /// Adds a new <see cref="Route"/>. 
        /// </summary>
        /// <exception cref="InconsistentRouteException">Throws whenever the given <paramref name="route"/> from <see cref="Town"/> is not the current instance.</exception>
        /// <param name="route">An instance of <see cref="Route"/>.</param>
        public void AddRoute(Route route)
        {
            if (route.From.Equals(this))
                RoutesByDestinationTown.Add(route.To, route);
            else
                throw new InconsistentRouteException(route, this);
        }

        /// <inheritdoc />
        public Route GetRoute(ITown to) =>
            RoutesByDestinationTown.ContainsKey(to) ? RoutesByDestinationTown[to] : throw new InvalidRouteException($"There's no such route that goes to '{to}' town.");

        /// <inheritdoc />
        public bool Equals(ITown other) => !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || string.Equals(Name, other.Name));

        /// <inheritdoc />
        public override bool Equals(object obj) =>
            !ReferenceEquals(null, obj) &&
            (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Town)obj));

        /// <inheritdoc />
        public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;

        /// <inheritdoc />
        public override string ToString() => Name;
    }
}