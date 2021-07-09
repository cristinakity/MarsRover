using MarsRovers.Core.Enums;
using MarsRovers.Core.Extensions;
using Xunit;
using FluentAssertions;

namespace MarsRovers.UnitTest
{
    public class CardinalPointsEnumExtensionTest
    {
        [Theory]
        [InlineData(CardinalPointsEnum.North, 'N')]
        [InlineData(CardinalPointsEnum.South, 'S')]
        [InlineData(CardinalPointsEnum.East, 'E')]
        [InlineData(CardinalPointsEnum.West, 'W')]
        public void ToCharShouldBe(CardinalPointsEnum facingTo, char expected)
        {
            var result = facingTo.ToChar();
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(CardinalPointsEnum.North, 'W')]
        [InlineData(CardinalPointsEnum.South, 'E')]
        [InlineData(CardinalPointsEnum.East, 'S')]
        [InlineData(CardinalPointsEnum.West, 'N')]
        public void ToCharShouldNotBe(CardinalPointsEnum facingTo, char expected)
        {
            var result = facingTo.ToChar();
            result.Should().NotBe(expected);
        }

        [Theory]
        [InlineData('N', true)]
        [InlineData('S', true)]
        [InlineData('E', true)]
        [InlineData('W', true)]
        [InlineData('n', true)]
        [InlineData('s', true)]
        [InlineData('e', true)]
        [InlineData('w', true)]
        [InlineData('M', false)]
        [InlineData('X', false)]
        [InlineData('a', false)]
        [InlineData('b', false)]
        [InlineData('c', false)]
        public void IsCardinalPoint(char instruction, bool expected)
        {
            var result = instruction.IsCardinalPoint();
            result.Should().Be(expected);
        }
    }
}
