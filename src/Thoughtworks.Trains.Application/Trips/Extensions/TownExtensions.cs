using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Application.Trips.Extensions
{
    public static class TownExtensions
    {
        public static bool CloneIfEquals(this Town town, Town other, out Town clone, string newName = "")
        {
            clone = town;

            if (!town.Equals(other))
                return false;

            var routes = town.Routes;
            newName = string.IsNullOrWhiteSpace(newName) ? town.Name : newName;
            clone = new Town(newName);
            foreach (var route in routes)
            {
                var cloneRoute = new Route(clone, route.To, route.Distance);
                clone.AddRoute(cloneRoute);
            }

            return true;
        }
    }
}
