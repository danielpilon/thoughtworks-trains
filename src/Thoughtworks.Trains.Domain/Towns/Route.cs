using System;

namespace Thoughtworks.Trains.Domain.Towns
{
    /// <summary>
    /// Represents a route between two towns.
    /// </summary>
    public struct Route
    {
        /// <summary>
        /// Creates a new instance of <see cref="Route"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="from"/> instance is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="to"/> instance is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="distance"/> value is lower or equal to zero.</exception>
        /// <param name="from">The instance of the origin <see cref="Town"/> in this route.</param>
        /// <param name="to">The instance of the destination <see cref="Town"/> in this route.</param>
        /// <param name="distance">The total distance between the <paramref name="from"/> and <see cref="to"/> towns.</param>
        public Route(ITown from, ITown to, int distance)
        {
            Distance = distance > 0 ? distance : throw new ArgumentException($"Argument {nameof(distance)} value cannot be lower than 1.");
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
        }

        /// <summary>
        /// Gets an <see cref="int"/> representing the distance between the two towns.
        /// </summary>
        public int Distance { get; }

        /// <summary>
        /// Gets the origin <see cref="Town"/> of this route. 
        /// </summary>
        public ITown From { get; }

        /// <summary>
        /// Gets the destination <see cref="Town"/> of this route. 
        /// </summary>
        public ITown To { get; }
    }
}