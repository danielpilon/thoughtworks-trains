using System;

namespace Thoughtworks.Trains.Domain.Railway.Exceptions
{
    /// <summary>
    /// An <see cref="Exception"/> that represents an unknown town.
    /// </summary>
    public class UnknownTownException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="UnknownTownException"/>.
        /// </summary>
        /// <param name="town">The town name that could not be found.</param>
        public UnknownTownException(string town) : base($"Could not find a town name as {town}.")
        {
        }
    }
}