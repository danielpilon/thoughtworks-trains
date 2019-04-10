using System.Linq;
using Thoughtworks.Trains.Application.Paths;
using Thoughtworks.Trains.Domain.Railway;
using Thoughtworks.Trains.Domain.Towns;
using Xunit;

namespace Thoughtworks.Trains.Application.Tests.Paths
{
    public class PathBuilderUnitTests
    {
        [Fact]
        public void Build_ShouldCreatePathInstance()
        {
            // Arrange
            var railwaySystem = new RailwaySystem();
            railwaySystem.AddTown(new Town("A"));
            railwaySystem.AddTown(new Town("B"));
            var builder = new PathBuilder();

            // Act
            var path = builder.Build(railwaySystem, "A", "B");

            // Assert
            Assert.True(path.Stops.Count() == 2);
            Assert.True(path.Stops.ElementAt(0).Name == "A");
            Assert.True(path.Stops.ElementAt(1).Name == "B");
        }
    }
}
