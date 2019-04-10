namespace Thoughtworks.Trains.Domain.Towns.Extensions
{
    public static class TownExtensions
    {
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
