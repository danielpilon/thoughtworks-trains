using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Thoughtworks.Trains.Domain.Towns;

[assembly: InternalsVisibleTo("Thoughtworks.Trains.Application.Tests")]

namespace Thoughtworks.Trains.Domain.Railway
{
    public interface IRailwaySystem
    {
        ITown GetTownByName(string town);
        bool HasTown(string town);
        IEnumerable<ITown> GetTowns();
    }

    /// <summary>
    /// 
    /// </summary>
    internal class RailwaySystem : IRailwaySystem
    {
        internal RailwaySystem()
        {

        }

        private Dictionary<string, ITown> TownsByName { get; } = new Dictionary<string, ITown>();

        public void AddTown(Town town)
        {
            if (TownsByName.ContainsKey(town.Name)) return;
            TownsByName.Add(town.Name, town);
        }
        

        public ITown GetTownByName(string town) => TownsByName[town];

        public bool HasTown(string town) => TownsByName.ContainsKey(town);

        public IEnumerable<ITown> GetTowns() => TownsByName.Values;
    }
}
