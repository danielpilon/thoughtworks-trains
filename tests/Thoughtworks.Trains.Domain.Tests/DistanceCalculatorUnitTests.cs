using System;
using Xunit;
using Xunit.Sdk;

namespace Thoughtworks.Trains.Domain.Tests
{
    public class DistanceCalculatorUnitTests
    {
        public DistanceCalculatorUnitTests()
        {
            var townA = new Town("A");
            Railway.AddTown(townA);
            var townB = new Town("B");
            Railway.AddTown(townB);
            var townC = new Town("C");
            Railway.AddTown(townC);
            var townD = new Town("D");
            Railway.AddTown(townD);
            var townE = new Town("E");
            Railway.AddTown(townE);

            townA.AddRoute(new Route(townB, 5));
            townA.AddRoute(new Route(townD, 5));
            townA.AddRoute(new Route(townE, 7));

            townB.AddRoute(new Route(townC, 4));

            townC.AddRoute(new Route(townD, 8));
            townC.AddRoute(new Route(townE, 2));

            townD.AddRoute(new Route(townC, 8));

            townE.AddRoute(new Route(townB, 3));
        }

        private RailwaySystem Railway { get; } = new RailwaySystem();

        [Fact]
        public void ResolveDistance_ShouldReturnDistanceBetweenABC()
        {
            // Arrange
            var path = new Path();
            path.AddStop(Railway.GetTownByName("A"));
            path.AddStop(Railway.GetTownByName("B"));
            path.AddStop(Railway.GetTownByName("C"));

            // Act
            var distance = DistanceCalculator.ResolveDistance(path);

            // Assert
            Assert.Equal(9, distance);
        }

        [Fact]
        public void ResolveDistance_ShouldReturnDistanceBetweenAD()
        {
            // Arrange
            var path = new Path();
            path.AddStop(Railway.GetTownByName("A"));
            path.AddStop(Railway.GetTownByName("D"));

            // Act
            var distance = DistanceCalculator.ResolveDistance(path);

            // Assert
            Assert.Equal(5, distance);
        }

        [Fact]
        public void ResolveDistance_ShouldReturnDistanceBetweenADC()
        {
            // Arrange
            var path = new Path();
            path.AddStop(Railway.GetTownByName("A"));
            path.AddStop(Railway.GetTownByName("D"));
            path.AddStop(Railway.GetTownByName("C"));

            // Act
            var distance = DistanceCalculator.ResolveDistance(path);

            // Assert
            Assert.Equal(13, distance);
        }

        [Fact]
        public void ResolveDistance_ShouldReturnDistanceBetweenAEBCD()
        {
            // Arrange
            var path = new Path();
            path.AddStop(Railway.GetTownByName("A"));
            path.AddStop(Railway.GetTownByName("E"));
            path.AddStop(Railway.GetTownByName("B"));
            path.AddStop(Railway.GetTownByName("C"));
            path.AddStop(Railway.GetTownByName("D"));

            // Act
            var distance = DistanceCalculator.ResolveDistance(path);

            // Assert
            Assert.Equal(22, distance);
        }

        [Fact]
        public void ResolveDistance_ShouldReturnDistanceBetweenAED()
        {
            // Arrange
            var path = new Path();
            path.AddStop(Railway.GetTownByName("A"));
            path.AddStop(Railway.GetTownByName("E"));
            path.AddStop(Railway.GetTownByName("D"));

            // Act and assert
            Assert.Throws<InvalidRouteException>(() => DistanceCalculator.ResolveDistance(path));
        }

        [Fact]
        public void ResolveTripsWithMaxStops_ShouldReturnMaxTripsStartingInCAndEndingInC_With3MaximumStops()
        {
            // Arrange
            var townC = Railway.GetTownByName("C");

            // Act
            var trips = DistanceCalculator.ResolveTripsWithMaxStops(townC, townC, 3);

            // Assert
            Assert.Equal(2, trips);

        }

        [Fact]
        public void ResolveTripsWithMaxStops_ShouldReturnMaxTripsStartingInAAndEndingInC_With4MaximumStops()
        {
            // Arrange
            var townC = Railway.GetTownByName("C");

            // Act
            var trips = DistanceCalculator.ResolveTripsWithMaxStops(townC, townC, 4);

            // Assert
            Assert.Equal(3, trips);

        }

        [Fact]
        public void ResolveTripsWithMaxStops_ShouldReturnMaxTripsStartingInCAndEndingInC_With30MaximumStops()
        {
            // Arrange
            var townC = Railway.GetTownByName("C");

            // Act
            var trips = DistanceCalculator.ResolveTripsWithMaxStops(townC, townC, 30);

            // Assert
            Assert.Equal(7, trips);

        }

        [Fact]
        public void ResolveShortestDistance_ShouldReturnLengthOfTheShortestRouteBetweenAAndC()
        {
            // Arrange

            // Act
            var distance = DistanceCalculator.ResolveShortestDistance(Railway,
                Railway.GetTownByName("A"), Railway.GetTownByName("C"));
            // Assert
            Assert.Equal(9, distance);
        }

        [Fact]
        public void ResolveShortestDistance_ShouldReturnLengthOfTheShortestRouteBetweenDAndB()
        {
            // Arrange

            // Act
            var distance = DistanceCalculator.ResolveShortestDistance(Railway,
                Railway.GetTownByName("D"), Railway.GetTownByName("B"));
            // Assert
            Assert.Equal(13, distance);
        }

        [Fact]
        public void ResolveShortestDistance_ShouldReturnLengthOfTheShortestRouteBetweenAAndB()
        {
            // Arrange

            // Act
            var distance = DistanceCalculator.ResolveShortestDistance(Railway,
                Railway.GetTownByName("A"), Railway.GetTownByName("B"));
            // Assert
            Assert.Equal(5, distance);
        }

        [Fact]
        public void ResolveShortestDistance_WhenRouteDoesNotExist_ShouldThrowInvalidRouteException()
        {
            // Arrange

            // Act
            Assert.Throws<InvalidRouteException>(() => DistanceCalculator.ResolveShortestDistance(Railway,
                Railway.GetTownByName("C"), Railway.GetTownByName("A")));
            // Assert
        }

        [Fact]
        public void ResolveShortestDistance_ShouldReturnLengthOfTheShortestRouteBetweenBAndB()
        {
            // Arrange

            // Act
            var distance = DistanceCalculator.ResolveShortestDistance(Railway,
                Railway.GetTownByName("B"), Railway.GetTownByName("B"));
            // Assert
            Assert.Equal(9, distance);
        }
    }
}