using Thoughtworks.Trains.Domain.Railway;

namespace Thoughtworks.Trains.Application.Paths
{
    public interface IPathBuilder
    {
        IPath Build(IRailwaySystem railwaySystem, params string[] townsNames);
    }
}