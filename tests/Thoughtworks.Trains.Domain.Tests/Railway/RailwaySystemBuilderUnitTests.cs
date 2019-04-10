using System;
using System.Linq;
using Thoughtworks.Trains.Domain.Railway;
using Xunit;

namespace Thoughtworks.Trains.Domain.Tests.Railway
{
    public class RailwaySystemBuilderUnitTests
    {
        public RailwaySystemBuilderUnitTests()
        {
            Builder = new RailwaySystemBuilder();
        }

        private RailwaySystemBuilder Builder { get; }

        [Theory]
        [InlineData("AB1,CD2,EB3AB5")]
        [InlineData("AAB1,CD2,EB3AB5")]
        public void Build_WhenRouteSetIsInvalid_ShouldThrowArgumentException(string routeSet)
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() => Builder.Build(routeSet));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Build_WhenRouteSetIsNullOrWhitespace_ShouldThrowArgumentNullException(string routeSet)
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => Builder.Build(routeSet));
        }

        [Fact]
        public void Build_ShouldCreateRailwaySystem()
        {
            // Arrange
            var routeSet = "AB1,AC2,BC3,BD4";

            // Act
            var railwaySystem = Builder.Build(routeSet);

            // Assert
            Assert.True(railwaySystem.GetTowns().Count() == 4);
            Assert.True(railwaySystem.GetTowns().Select(_ => _.Name).Distinct().SequenceEqual(new[] { "A", "B", "C", "D" }));
            Assert.True(railwaySystem.GetTowns().SelectMany(_ => _.Routes).Count() == 4);
            Assert.True(string.Join(",", railwaySystem.GetTowns().SelectMany(_ => _.Routes).Select(_ => _.ToString())) == routeSet);
        }
    }
}
