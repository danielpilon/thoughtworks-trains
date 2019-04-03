using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thoughtworks.Trains.Domain
{
    public class RailwaySystem
    {
        private Dictionary<string, Station> Stations { get; } = new Dictionary<string, Station>();

        public void AddStation(Station station)
        {
            if (Stations.ContainsKey(station.Name)) return;
            Stations.Add(station.Name, station);
        }

        public Station GetStationByName(string station) => Stations[station];
    }
}
