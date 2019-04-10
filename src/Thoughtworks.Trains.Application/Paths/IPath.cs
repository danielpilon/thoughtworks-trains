using System.Collections.Generic;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Application.Paths
{
    /// <summary>
    /// Represents the stops in a path between two <see cref="ITown"/>.
    /// </summary>
    public interface IPath
    {
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="ITown"/> with the sequence of stops between two <see cref="ITown"/>s.
        /// </summary>
        IEnumerable<ITown> Stops { get; }
    }
}