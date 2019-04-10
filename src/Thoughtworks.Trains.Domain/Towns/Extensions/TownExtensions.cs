namespace Thoughtworks.Trains.Domain.Towns.Extensions
{
    /// <summary>
    /// Extension methods for instances of <see cref="ITown"/>.
    /// </summary>
    public static class TownExtensions
    {
        /// <summary>
        /// Clones a given <see cref="Town"/> instance if they are equals.
        /// </summary>
        /// <param name="town">The instance of the <see cref="Town"/>.</param>
        /// <param name="other">The other instance of the <see cref="Town"/> to compare.</param>
        /// <param name="clone">An output var with the cloned <see cref="Town"/>.</param>
        /// <param name="newName">(optional) An <see cref="string"/> representing a new name for the cloned <see cref="Town"/>.</param>
        /// <returns><c>True</c> if the <paramref name="town"/> and <paramref name="other"/> are equals; Otherwise, <c>false</c>.</returns>
        public static bool CloneIfEquals(this ITown town, ITown other, out ITown clone, string newName = "")
        {
            clone = town;

            if (!town.Equals(other))
                return false;

            var routes = town.Routes;
            newName = string.IsNullOrWhiteSpace(newName) ? town.Name : newName;
            var cloneTown = new Town(newName);
            foreach (var route in routes)
            {
                var cloneRoute = new Route(cloneTown, route.To, route.Distance);
                cloneTown.AddRoute(cloneRoute);
            }

            clone = cloneTown;
            return true;
        }
    }
}
