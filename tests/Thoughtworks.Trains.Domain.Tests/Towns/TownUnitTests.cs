using System;
using System.Linq;
using Thoughtworks.Trains.Domain.Towns;
using Thoughtworks.Trains.Domain.Towns.Exceptions;
using Xunit;

namespace Thoughtworks.Trains.Domain.Tests.Towns
{
    public class TownUnitTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Ctor_WhenNameArgumentIsNullOrWhitespace_ShouldThrowArgumentNullException(string name)
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => new Town(name));
        }

        [Fact]
        public void AddRoute_WhenOriginIsNotSameTown_ShouldThrowInconsistentRouteException()
        {
            // Arrange
            var townA = new Town("A");
            var townB = new Town("B");
            var townC = new Town("C");
            var route = new Route(townB, townC, 1);

            // Act and assert
            Assert.Throws<InconsistentRouteException>(() => townA.AddRoute(route));
        }

        [Fact]
        public void AddRoute_ShouldRegisterRoutes()
        {
            // Arrange
            var townA = new Town("A");
            var townB = new Town("B");
            var townC = new Town("C");
            var route1 = new Route(townA, townC, 1);
            var route2 = new Route(townA, townB, 2);

            // Act
            townA.AddRoute(route1);
            townA.AddRoute(route2);

            // Assert
            Assert.True(townA.Routes.SequenceEqual(new[] { route1, route2 }));
        }

        [Fact]
        public void GetRoute_ShouldReturnExpectedRoute()
        {
            // Arrange
            var townA = new Town("A");
            var townC = new Town("C");
            var expectedRoute = new Route(townA, townC, 1);
            townA.AddRoute(expectedRoute);

            // Act
            var route = townA.GetRoute(townC);

            // Assert
            Assert.True(route.Equals(expectedRoute));
        }

        [Fact]
        public void GetRoute_WhenThereIsNoRouteToTheExpectedTown_ShouldReturnInvalidRouteException()
        {
            // Arrange
            var townA = new Town("A");
            var townB = new Town("B");
            var townC = new Town("C");
            var expectedRoute = new Route(townA, townC, 1);
            townA.AddRoute(expectedRoute);

            // Act and assert
            Assert.Throws<InvalidRouteException>(() => townA.GetRoute(townB));
        }
    }
}
