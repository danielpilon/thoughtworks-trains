using Thoughtworks.Trains.Domain.Railway;

namespace Thoughtworks.Trains.Application.Paths
{
    /// <summary>
    /// Provides functionality to create instances of <see cref="IPath"/>.
    /// </summary>
    public class PathBuilder
    {
        /// <summary>
        /// Builds an <see cref="IPath"/> instance based on the provided <paramref name="railwaySystem"/> and <paramref name="townsNames"/>.
        /// </summary>
        /// <param name="railwaySystem">An instance of <see cref="RailwaySystem"/>.</param>
        /// <param name="townsNames">An <see cref="string"/> array with the sequence of towns that constitutes the <see cref="IPath"/>.</param>
        /// <returns>An instance of <see cref="IPath"/>.</returns>
        public IPath Build(IRailwaySystem railwaySystem, params string[] townsNames)
        {
            var path = new Path();
            foreach (var townName in townsNames) path.AddStop(railwaySystem.GetTownByName(townName));
            return path;
        }
    }
}
