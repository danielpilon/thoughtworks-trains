using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
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

        public Town GetOrAddTownByName(string town) => HasTown(town) ? GetTownByName(town) : CreateTown(town);

        public bool HasTown(string town) => TownsByName.ContainsKey(town);

        public IEnumerable<Town> GetTowns() => TownsByName.Values;

        public IEnumerable<Route> GetRoutesByTown(Town town) => RoutesByTown[town];
        private Town CreateTown(string townName)
        {
            var town = new Town(townName);
            AddTown(town);
            return town;
        }

    }
}
