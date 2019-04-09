using System.Linq;
using Thoughtworks.Trains.Application.Trips;
using Thoughtworks.Trains.Domain.Railway;
using Thoughtworks.Trains.Domain.Towns;
using Thoughtworks.Trains.Domain.Towns.Exceptions;
using Xunit;

namespace Thoughtworks.Trains.Application.Tests
{
    public class TripServiceUnitTests
    {
        public TripServiceUnitTests()
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

            // AB5
            townA.AddRoute(new Route(townA, townB, 5));
            // BC4
            townB.AddRoute(new Route(townB, townC, 4));
            // CD8
            townC.AddRoute(new Route(townC, townD, 8));
            // DC8
            townD.AddRoute(new Route(townD, townC, 8));
            // DE6
            townD.AddRoute(new Route(townD, townE, 6));
            // AD5
            townA.AddRoute(new Route(townA, townD, 5));
            // CE2
            townC.AddRoute(new Route(townC, townE, 2));
            // EB3
            townE.AddRoute(new Route(townE, townB, 3));
            // AE7
            townA.AddRoute(new Route(townA, townE, 7));
        }

        private RailwaySystem Railway { get; } = new RailwaySystem();

        [Theory]
        [InlineData(9, "A", "B", "C")]
        [InlineData(5, "A", "D")]
        [InlineData(13, "A", "D", "C")]
        [InlineData(22, "A", "E", "B", "C", "D")]
        public void ResolveDistance_ShouldReturnDistanceBetweenABC(int expectedDistance, params string[] towns)
        {
            // Arrange
            var path = new Path();
            foreach (var town in towns) path.AddStop(Railway.GetTownByName(town));

            // Act
            var actualDistance = TripService.ResolveDistance(path);

            // Assert
            Assert.Equal(expectedDistance, actualDistance);
        }

        [Fact]
        public void ResolveDistance_WhenRouteDoesNotExists_ShouldThrowInvalidRouteException()
        {
            // Arrange
            var path = new Path();
            path.AddStop(Railway.GetTownByName("A"));
            path.AddStop(Railway.GetTownByName("E"));
            path.AddStop(Railway.GetTownByName("D"));

            // Act and assert
            Assert.Throws<InvalidRouteException>(() => TripService.ResolveDistance(path));
        }

        [Theory]
        [InlineData("C", "C", 3, 2)]
        [InlineData("A", "C", 1, 0)]
        [InlineData("A", "B", 1, 1)]
        [InlineData("C", "A", 5, 0)]
        [InlineData("C", "A", 1, 0)]

        public void Search_ShouldReturnExpectedMaxTrips_WithMaximumStops(string from, string to, int maxStops, int expectedTrips)
        {
            // Act
            var trips = TripService
                .Search(Railway.GetTownByName(from), Railway.GetTownByName(to), trip => trip.Stops > maxStops);

            // Assert
            Assert.Equal(expectedTrips, trips.Count());
        }

        [Theory]
        [InlineData("A", "C", 4, 3)]
        [InlineData("A", "C", 2, 2)]
        [InlineData("C", "A", 1, 0)]
        [InlineData("C", "A", 5, 0)]
        public void Search_ShouldReturnExpectedMaxTrips_WithExactStops(string from, string to, int stops, int expectedTrips)
        {
            // Act
            var trips = TripService
                .Search(Railway.GetTownByName(from), Railway.GetTownByName(to), trip => trip.Stops > stops, trip => trip.Stops == stops);

            // Assert
            Assert.Equal(expectedTrips, trips.Count());
        }


        [Theory]
        [InlineData("C", "C", 30, 7)]
        public void Search_ShouldReturnExpectedMaxTrips_WithDistanceLessThanMaxDistance(string from, string to, int maxDistance, int expectedTrips)
        {
            // Act
            var trips = TripService.Search(Railway.GetTownByName(from), Railway.GetTownByName(to), trip => trip.TotalDistance >= maxDistance);

            // Assert
            Assert.Equal(expectedTrips, trips.Count());
        }

        [Theory]
        [InlineData("B", "B", 9)]
        [InlineData("A", "B", 5)]
        [InlineData("D", "B", 9)]
        [InlineData("A", "C", 9)]
        public void ResolveShortestTrip_ShouldReturnExpectedDistanceBasedOnTheRoute(string from, string to, int expectedDistance)
        {
            // Act
            var path = TripService.FindShortest(Railway, Railway.GetTownByName(from), Railway.GetTownByName(to));

            // Assert
            Assert.Equal(expectedDistance, path.TotalDistance);
        }

        [Fact]
        public void ResolveShortestTrip_WhenRouteDoesNotExist_ShouldThrowInvalidRouteException()
        {
            // Act and Assert
            Assert.Throws<InvalidRouteException>(() => TripService.FindShortest(Railway,
                Railway.GetTownByName("C"), Railway.GetTownByName("A")));
        }
    }
}