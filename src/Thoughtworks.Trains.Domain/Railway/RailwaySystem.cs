using System.Collections.Generic;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Domain.Railway
{
    public class RailwaySystem
    {
        public void AddTown(Town town)
        {
            if (TownsByName.ContainsKey(town.Name)) return;
            TownsByName.Add(town.Name, town);
            RoutesByTown.Add(town, town.Routes);
        }
        private Dictionary<string, Town> TownsByName { get; } = new Dictionary<string, Town>();
        private Dictionary<Town, IEnumerable<Route>> RoutesByTown { get; } = new Dictionary<Town, IEnumerable<Route>>();

        public Town GetTownByName(string town) => TownsByName[town];

        public IEnumerable<Town> GetTowns() => TownsByName.Values;

        public IEnumerable<Route> GetRoutesByTown(Town town) => RoutesByTown[town];
    }
}
