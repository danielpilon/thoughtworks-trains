using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Thoughtworks.Trains.Domain.Railway.Exceptions;
using Thoughtworks.Trains.Domain.Towns;

[assembly: InternalsVisibleTo("Thoughtworks.Trains.Application.Tests")]
[assembly: InternalsVisibleTo("Thoughtworks.Trains.Domain.Tests")]

namespace Thoughtworks.Trains.Domain.Railway
{
    /// <summary>
    /// Represents a full railway system.
    /// </summary>
    internal sealed class RailwaySystem : IRailwaySystem
    {
        /// <summary>
        /// Gets a <see cref="Dictionary{TKey,TValue}"/> of <see cref="string"/> and <see cref="ITown"/> keyed by the town name.
        /// </summary>
        private Dictionary<string, ITown> TownsByName { get; } = new Dictionary<string, ITown>();

        /// <summary>
        /// Adds a new <see cref="Town"/> to this railway system.
        /// </summary>
        /// <param name="town">An instance of <see cref="Town"/>.</param>
        public void AddTown(Town town)
        {
            if (TownsByName.ContainsKey(town.Name)) return;
            TownsByName.Add(town.Name, town);
        }
        
        /// <inheritdoc />
        public ITown GetTownByName(string town) => HasTown(town) ? TownsByName[town] : throw new UnknownTownException(town);
        
        /// <inheritdoc />
        public IEnumerable<ITown> GetTowns() => TownsByName.Values;

        /// <summary>
        /// Verifies if the given town name has an equivalent <see cref="Town"/> instance.
        /// </summary>
        /// <param name="town">An <see cref="string"/> representing the name of the town to find.</param>
        /// <returns><c>True</c> if an instance of <see cref="Town"/> is found; Otherwise, <c>false</c>.</returns>
        public bool HasTown(string town) => TownsByName.ContainsKey(town);
    }
}
