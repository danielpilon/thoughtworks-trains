using Thoughtworks.Trains.Domain.Railway;

namespace Thoughtworks.Trains.Application.Paths
{
    public class PathBuilder
    {
        public IPath Build(RailwaySystem railwaySystem, params string[] townsNames)
        {
            var path = new Path();
            foreach (var townName in townsNames) path.AddStop(railwaySystem.GetTownByName(townName));
            return path;
        }
    }
}
