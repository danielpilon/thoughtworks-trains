using System;
using System.Collections.Generic;
using System.Text;

namespace Thoughtworks.Trains.Domain
{
    public class Path
    {
        private readonly List<Town> _routes = new List<Town>();

        public IEnumerable<Town> Routes => _routes;

        public void AddStop(Town town) => _routes.Add(town);
    }
}
