using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Thoughtworks.Trains.Domain.Towns;
[assembly: InternalsVisibleTo("Thoughtworks.Trains.Application.Tests")]

namespace Thoughtworks.Trains.Application.Routes
{
    internal sealed class Path : IPath
    {
        private readonly List<Town> _towns = new List<Town>();

        public IEnumerable<Town> Towns => _towns;

        public void AddStop(Town town) => _towns.Add(town);
    }

    internal sealed class ResolvedPath : IResolvedPath
    {

        public ResolvedPath(Town origin)
        {
            Origin = origin;
        }

        private List<Route> InnerRoutes { get; } = new List<Route>();

        public Town Origin { get; }

        public IEnumerable<Route> Routes => InnerRoutes;

        public int TotalDistance => InnerRoutes.Sum(_ => _.Distance);

        public void AddRoute(Route route) => InnerRoutes.Add(route);
    }

    public interface IPath
    {
        IEnumerable<Town> Towns { get; }
    }

    public interface IResolvedPath
    {
        Town Origin { get; }

        IEnumerable<Route> Routes { get; }

        int TotalDistance { get; }
    }
}
