using System;
using System.Collections.Generic;
using System.Text;

namespace Thoughtworks.Trains.Domain
{
    public class Path
    {
        private readonly List<Station> _routes = new List<Station>();

        public IEnumerable<Station> Routes => _routes;

        public void AddStop(Station station) => _routes.Add(station);
    }
}
