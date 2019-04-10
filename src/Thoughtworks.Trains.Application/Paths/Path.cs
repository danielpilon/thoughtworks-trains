using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Thoughtworks.Trains.Domain.Towns;

[assembly: InternalsVisibleTo("Thoughtworks.Trains.Application.Tests")]

namespace Thoughtworks.Trains.Application.Paths
{
    internal sealed class Path : IPath
    {
        private readonly List<ITown> _towns = new List<ITown>();

        public IEnumerable<ITown> Towns => _towns;

        public void AddStop(ITown town) => _towns.Add(town);
    }
}
