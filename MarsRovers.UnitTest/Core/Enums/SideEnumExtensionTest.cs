using MarsRovers.Core.Enums;
using MarsRovers.Core.Extensions;
using Xunit;
using FluentAssertions;

namespace MarsRovers.UnitTest
{
    public class SideEnumExtensionTest
    {
        [Theory]
        [InlineData(SideEnum.Left,  'L')]
        [InlineData(SideEnum.Right, 'R')]
        public void ToCharShouldBe(SideEnum facingTo, char expected)
        {
            var result = facingTo.ToChar();
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(SideEnum.Left,  'R')]
        [InlineData(SideEnum.Right, 'L')]
        public void ToCharShouldNotBe(SideEnum facingTo, char expected)
        {
            var result = facingTo.ToChar();
            result.Should().NotBe(expected);
        }

        [Theory]
        [InlineData('R', true)]
        [InlineData('L', true)]
        [InlineData('r', true)]
        [InlineData('l', true)]
        [InlineData('M', false)]
        [InlineData('X', false)]
        public void IsSide(char instruction, bool expected) {
            var result = instruction.IsSide();
            result.Should().Be(expected);
        }
    }
}
