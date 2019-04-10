using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Thoughtworks.Trains.Domain.Towns;

[assembly: InternalsVisibleTo("Thoughtworks.Trains.Application.Tests")]

namespace Thoughtworks.Trains.Application.Paths
{
    internal sealed class Path : IPath
    {
        private readonly List<Town> _towns = new List<Town>();

        public IEnumerable<Town> Towns => _towns;

        public void AddStop(Town town) => _towns.Add(town);
    }
}
