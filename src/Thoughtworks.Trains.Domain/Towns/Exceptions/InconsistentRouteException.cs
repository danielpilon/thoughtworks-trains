using System;

namespace Thoughtworks.Trains.Domain.Towns.Exceptions
{
    /// <summary>
    /// An <see cref="Exception"/> that represents an incosistent route.
    /// </summary>
    public class InconsistentRouteException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="InconsistentRouteException"/>.
        /// </summary>
        /// <param name="message">The message with the details of the exception.</param>
        /// <param name="route">The instance of the inconsistent <see cref="InconsistentRoute"/>.</param>
        public InconsistentRouteException(string message, Route route) : base(message) => InconsistentRoute = route;

        /// <summary>
        /// Gets the inconsistent instance of <see cref="Route"/>.
        /// </summary>
        public Route InconsistentRoute { get; }
    }
}
