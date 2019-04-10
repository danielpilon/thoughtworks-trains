using System.Linq;
using Thoughtworks.Trains.Domain.Railway;
using Thoughtworks.Trains.Domain.Railway.Exceptions;
using Thoughtworks.Trains.Domain.Towns;
using Xunit;

namespace Thoughtworks.Trains.Domain.Tests.Railway
{
    public class RailwaySystemUnitTests
    {
        public RailwaySystemUnitTests()
        {
            RailwaySystem = new RailwaySystem();
            Town = new Town("A");
            RailwaySystem.AddTown(Town);

        }

        private RailwaySystem RailwaySystem { get; }
        private Town Town { get; }

        [Fact]
        public void AddTown_ShouldNotAddTwice()
        {
            // Act
            RailwaySystem.AddTown(Town);

            // Assert
            Assert.True(RailwaySystem.GetTowns().Count() == 1);
        }

        [Fact]
        public void GetTownByName_ShouldReturnTown()
        {
            // Act
            var town = RailwaySystem.GetTownByName("A");

            // Assert
            Assert.Equal(town, Town);
        }

        [Fact]
        public void GetTownByName_WhenTownDoesNotExists_ShouldThrowUnknownTownException()
        {
            // Act and assert
            Assert.Throws<UnknownTownException>(() => RailwaySystem.GetTownByName("B"));
        }
    }
}
