using System;
using System.Collections.Generic;
using Thoughtworks.Trains.Domain.Towns.Exceptions;

namespace Thoughtworks.Trains.Domain.Towns
{
    /// <summary>
    /// Represents a town in a railway system.
    /// </summary>
    public interface ITown : IEquatable<ITown>
    {
        /// <summary>
        /// Gets a <see cref="string"/> representing the name of the town.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> of <see cref="Route"/> with all the instances of <see cref="Route"/>
        /// for this instance of <see cref="Town"/>.
        /// </summary>
        IEnumerable<Route> Routes { get; }

        /// <summary>
        /// Gets an instance of <see cref="Route"/> for a given destination <see cref="Town"/>.
        /// </summary>
        /// <exception cref="InvalidRouteException">Throws whenever the <paramref name="to"/> <see cref="Town"/>
        /// is not a destination from this <see cref="Town"/>.</exception>
        /// <param name="to">An instance of <see cref="Town"/>.</param>
        /// <returns>An instance of <see cref="Route"/>.</returns>
        Route GetRoute(ITown to);
    }
}