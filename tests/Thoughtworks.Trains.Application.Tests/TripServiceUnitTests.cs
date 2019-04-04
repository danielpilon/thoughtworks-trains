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

            townA.AddRoute(new Route(townA, townB, 5));
            townA.AddRoute(new Route(townA, townD, 5));
            townA.AddRoute(new Route(townA, townE, 7));

            townB.AddRoute(new Route(townB, townC, 4));

            townC.AddRoute(new Route(townC, townD, 8));
            townC.AddRoute(new Route(townC, townE, 2));

            townD.AddRoute(new Route(townD, townC, 8));
            townD.AddRoute(new Route(townD, townE, 6));

            townE.AddRoute(new Route(townE, townB, 3));
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
            var distance = TripService.ResolveDistance(path);

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
            var distance = TripService.ResolveDistance(path);

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
            var distance = TripService.ResolveDistance(path);

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
            var distance = TripService.ResolveDistance(path);

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
            Assert.Throws<InvalidRouteException>(() => TripService.ResolveDistance(path));
        }

        [Theory]
        [InlineData("C", "C", 3, 2)]
        [InlineData("A", "C", 1, 0)]
        [InlineData("A", "B", 1, 1)]

        public void ResolveTripsWithMaxStops_ShouldReturnExpectedMaxTrips_WithMaximumStops(string from, string to, int maxStops, int expectedTrips)
        {
            // Act
            var trips = TripService
                .ResolveTripsUpToMaxStops(Railway.GetTownByName(from), Railway.GetTownByName(to), maxStops)
                .Where(_ => _.Stops <= maxStops);

            // Assert
            Assert.Equal(expectedTrips, trips.Count());
        }

        [Theory]
        [InlineData("A", "C", 4, 3)]
        [InlineData("A", "C", 2, 2)]

        public void ResolveTripsWithExactNumberOfStops_ShouldReturnExpectedMaxTrips_WithExactStops(string from, string to, int stops, int expectedTrips)
        {
            // Act
            var trips = TripService
                .ResolveTripsUpToMaxStops(Railway.GetTownByName(from), Railway.GetTownByName(to), stops)
                .Where(_ => _.Stops == stops);

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
            var path = TripService.ResolveShortestTrip(Railway, Railway.GetTownByName(from), Railway.GetTownByName(to));

            // Assert
            Assert.Equal(expectedDistance, path.TotalDistance);
        }

        [Fact]
        public void ResolveShortestTrip_WhenRouteDoesNotExist_ShouldThrowInvalidRouteException()
        {
            // Act and Assert
            Assert.Throws<InvalidRouteException>(() => TripService.ResolveShortestTrip(Railway,
                Railway.GetTownByName("C"), Railway.GetTownByName("A")));
        }
        
        [Fact]
        public void ResolveRoutesWithDistanceLessThan_ShouldReturnMaxTripsStartingInCAndEndingInC_With30MaximumDistance()
        {
            // Arrange
            var townC = Railway.GetTownByName("C");

            // Act
            var trips = TripService.ResolveRoutesWithDistanceLessThan(townC, townC, 30);

            // Assert
            Assert.Equal(7, trips.Count());
        }

    }
}