using System;
using Thoughtworks.Trains.Domain.Towns;
using Xunit;

namespace Thoughtworks.Trains.Domain.Tests.Towns
{
    public class RouteUnitTests
    {
        [Fact]
        public void Ctor_WhenDistanceIsLessOrEqualThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            // Act and assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Route(new Town("A"), new Town("B"), 0));
        }

        [Fact]
        public void Ctor_WhenTownFromIsNull_ShouldThrowArgumentNullException()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => new Route(null, new Town("B"), 1));
        }

        [Fact]
        public void Ctor_WhenTownToIsNull_ShouldThrowArgumentNullException()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => new Route(new Town("A"), null, 1));
        }
    }
}
