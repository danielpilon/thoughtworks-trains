using System;

namespace Thoughtworks.Trains.Domain.Towns.Exceptions
{
    /// <summary>
    /// An <see cref="Exception"/> that represents an invalid route.
    /// </summary>
    public class InvalidRouteException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="InvalidRouteException"/>.
        /// </summary>
        /// <param name="message">The message with the details of the exception.</param>
        public InvalidRouteException(string message) : base(message)
        {

        }
    }
}