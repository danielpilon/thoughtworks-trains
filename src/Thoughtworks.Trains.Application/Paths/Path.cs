using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Thoughtworks.Trains.Domain.Towns;

[assembly: InternalsVisibleTo("Thoughtworks.Trains.Application.Tests")]

namespace Thoughtworks.Trains.Application.Paths
{
    /// <summary>
    /// Represents the stops in a path between two <see cref="ITown"/>.
    /// </summary>
    internal sealed class Path : IPath
    {
        /// <summary>
        /// An instance of <see cref="List{T}"/> of <see cref="ITown"/> with the stops between two <see cref="ITown"/>s.
        /// </summary>
        private readonly List<ITown> _stops = new List<ITown>();

        /// <inheritdoc />
        public IEnumerable<ITown> Stops => _stops;

        /// <summary>
        /// Add a new <see cref="ITown"/> stop to this <see cref="IPath"/>.
        /// </summary>
        /// <param name="town">An instance of <see cref="ITown"/>.</param>
        public void AddStop(ITown town) => _stops.Add(town);
    }
}