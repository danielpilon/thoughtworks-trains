using System.Collections.Generic;
using Thoughtworks.Trains.Domain.Railway.Exceptions;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Domain.Railway
{
    /// <summary>
    /// Represents a full railway system.
    /// </summary>
    public interface IRailwaySystem
    {
        /// <summary>
        /// Gets an instance of <see cref="ITown"/> by its name.
        /// </summary>
        /// <exception cref="UnknownTownException">Thrown whenever the <paramref name="town"/> doesn't exists.</exception>
        /// <param name="town">An <see cref="string"/> representing the town name.</param>
        /// <returns>An instance of <see cref="Town"/>.</returns>
        ITown GetTownByName(string town);

        /// <summary>
        /// Get all known towns in this <see cref="IRailwaySystem"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ITown"/> with all the known towns.</returns>
        IEnumerable<ITown> GetTowns();
    }
}