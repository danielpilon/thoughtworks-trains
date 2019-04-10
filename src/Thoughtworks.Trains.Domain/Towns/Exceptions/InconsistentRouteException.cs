using System;

namespace Thoughtworks.Trains.Domain.Towns.Exceptions
{
    /// <summary>
    /// An <see cref="Exception"/> that represents an inconsistent route.
    /// </summary>
    public class InconsistentRouteException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="InconsistentRouteException"/>.
        /// </summary>
        /// <param name="route">The instance of the inconsistent <see cref="InconsistentRouteException"/>.</param>
        /// <param name="origin">The expected instance of <see cref="ITown"/>.</param>
        public InconsistentRouteException(Route route, ITown origin) : base($"Inconsistent route detected. Origin {route.From} is not {origin}.") { }
    }
}
