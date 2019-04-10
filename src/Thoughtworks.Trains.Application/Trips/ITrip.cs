namespace Thoughtworks.Trains.Application.Trips
{
    /// <summary>
    /// Gathers the information of a Trip between an origin and a destination town.
    /// </summary>
    public interface ITrip
    {
        /// <summary>
        /// Gets the total distance of this trip.
        /// </summary>
        int TotalDistance { get; }

        /// <summary>
        /// Gets the number of stops in the route from the origin to the destination.
        /// </summary>
        int Stops { get; }
    }
}