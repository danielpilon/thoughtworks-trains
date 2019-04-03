using Xunit;
using Xunit.Sdk;

namespace Thoughtworks.Trains.Domain.Tests
{
    public class DistanceCalculatorUnitTests
    {
        private RailwaySystem _railway;

        public DistanceCalculatorUnitTests()
        {
            _railway = new RailwaySystem();
            // Arrange
            var stationA = new Station("A");
            _railway.AddStation(stationA);
            var stationB = new Station("B");
            _railway.AddStation(stationB);
            var stationC = new Station("C");
            _railway.AddStation(stationC);
            var stationD = new Station("D");
            _railway.AddStation(stationD);
            var stationE = new Station("E");
            _railway.AddStation(stationE);

            stationA.AddDestination(new Destination(stationB, 5));
            stationA.AddDestination(new Destination(stationD, 5));
            stationA.AddDestination(new Destination(stationE, 7));

            stationB.AddDestination(new Destination(stationC, 4));

            stationC.AddDestination(new Destination(stationD, 8));
            stationC.AddDestination(new Destination(stationE, 2));

            stationD.AddDestination(new Destination(stationC, 8));

            stationE.AddDestination(new Destination(stationB, 3));
        }

        [Fact]
        public void ResolveDistance_ShouldReturnDistanceBetweenABC()
        {
            // Arrange
            var path = new Path();
            path.AddStop(_railway.GetStationByName("A"));
            path.AddStop(_railway.GetStationByName("B"));
            path.AddStop(_railway.GetStationByName("C"));

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
            path.AddStop(_railway.GetStationByName("A"));
            path.AddStop(_railway.GetStationByName("D"));

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
            path.AddStop(_railway.GetStationByName("A"));
            path.AddStop(_railway.GetStationByName("D"));
            path.AddStop(_railway.GetStationByName("C"));

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
            path.AddStop(_railway.GetStationByName("A"));
            path.AddStop(_railway.GetStationByName("E"));
            path.AddStop(_railway.GetStationByName("B"));
            path.AddStop(_railway.GetStationByName("C"));
            path.AddStop(_railway.GetStationByName("D"));

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
            path.AddStop(_railway.GetStationByName("A"));
            path.AddStop(_railway.GetStationByName("E"));
            path.AddStop(_railway.GetStationByName("D"));

            // Act and assert
            Assert.Throws<InvalidDestinationException>(() => DistanceCalculator.ResolveDistance(path));
        }

        [Fact]
        public void ResolveTripsWithMaxStops_ShouldReturnMaxTripsStartingInCAndEndingInC_With3MaximumStops()
        {
            // Arrange
            var stationC = _railway.GetStationByName("C");

            // Act
            var trips = DistanceCalculator.ResolveTripsWithMaxStops(stationC, stationC, 3);

            // Assert
            Assert.Equal(2, trips);

        }

        [Fact]
        public void ResolveTripsWithMaxStops_ShouldReturnMaxTripsStartingInAAndEndingInC_With4MaximumStops()
        {
            // Arrange
            var stationC = _railway.GetStationByName("C");

            // Act
            var trips = DistanceCalculator.ResolveTripsWithMaxStops(stationC, stationC, 4);

            // Assert
            Assert.Equal(3, trips);

        }
    }
}