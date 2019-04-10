using System.Linq;
using Thoughtworks.Trains.Domain.Towns;
using Thoughtworks.Trains.Domain.Towns.Extensions;
using Xunit;

namespace Thoughtworks.Trains.Domain.Tests.Towns
{
    public class TownExtensionsUnitTests
    {
        [Fact]
        public void CloneIfEquals_WhenTownsAreSame_ShouldClone_PreservingName()
        {
            // Arrange
            var townA = new Town("A");
            var otherTownA = new Town("A");
            var townB = new Town("B");
            var route = new Route(townA, townB, 1);
            townA.AddRoute(route);

            // Act
            var result = townA.CloneIfEquals(otherTownA, out var cloned);

            // Assert
            Assert.True(result);
            Assert.False(ReferenceEquals(townA, cloned));
            Assert.Equal(cloned.Name, townA.Name);
            Assert.True(cloned.Routes.SequenceEqual(new []{route}));
        }

        [Fact]
        public void CloneIfEquals_WhenTownsAreSame_ShouldClone_WithDifferentName()
        {
            // Arrange
            var townA = new Town("A");
            var otherTownA = new Town("A");

            // Act
            var result = townA.CloneIfEquals(otherTownA, out var cloned, "Clone of A");

            // Assert
            Assert.True(result);
            Assert.False(ReferenceEquals(townA, cloned));
            Assert.NotEqual(cloned.Name, townA.Name);
        }

        [Fact]
        public void CloneIfEquals_WhenTownsAreDifferent_ShouldNotClone()
        {
            // Arrange
            var townA = new Town("A");
            var townB = new Town("B");

            // Act
            var result = townA.CloneIfEquals(townB, out var cloned);

            // Assert
            Assert.False(result);
            Assert.True(ReferenceEquals(townA, cloned));
        }
    }
}
