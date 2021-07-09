using FluentAssertions;
using MarsRovers.Core;
using MarsRovers.Core.Constants;
using MarsRovers.Core.Enums;
using MarsRovers.Core.Models;
using Xunit;

namespace MarsRovers.UnitTest
{
    public class RoverTest
    {
        private int _x; 
        private int _y;

        public RoverTest()
        {
            _x = 0;
            _y = 0;
        }

        [Theory]
        [InlineData(2, 2, CardinalPointsEnum.North, 20, "2 18 N")]
        [InlineData(2, 2, CardinalPointsEnum.North, 35, "2 18 N")]
        [InlineData(2, 2, CardinalPointsEnum.South, 46, "2 0 S")]
        [InlineData(2, 2, CardinalPointsEnum.South, 80, "2 0 S")]
        [InlineData(2, 2, CardinalPointsEnum.East, 30, "20 2 E")]
        [InlineData(2, 2, CardinalPointsEnum.East, 45, "20 2 E")]
        [InlineData(2, 2, CardinalPointsEnum.East, 10, "12 2 E")]
        [InlineData(2, 2, CardinalPointsEnum.West, 102, "0 2 W")]
        [InlineData(2, 2, CardinalPointsEnum.West, 26, "0 2 W")]
        [InlineData(0, 0, CardinalPointsEnum.East, 10, "10 0 E")]
        [InlineData(2, 2, CardinalPointsEnum.North, 50, "2 18 N")]
        public void MoveWithLimitTest(int x, int y, CardinalPointsEnum facingTo, int moveCount, string expected)
        {
            RoverSettings settings = new RoverSettings() {
                LimitX = 20,
                LimitY = 18,
                X = x,
                Y = y,
                FacingTo = facingTo
            };
            Rover rover = new Rover(settings);
            rover.Start(ConcatString("M", moveCount));
            rover.Results.Should().Be(expected);
        }

        [Theory]
        [InlineData(2, 2, CardinalPointsEnum.North, 1,  "2 3 N")]
        [InlineData(2, 2, CardinalPointsEnum.North, 2,  "2 4 N")]
        [InlineData(2, 2, CardinalPointsEnum.South, 1,  "2 1 S")]
        [InlineData(2, 2, CardinalPointsEnum.South, 2,  "2 0 S")]
        [InlineData(2, 2, CardinalPointsEnum.East,  1,  "3 2 E")]
        [InlineData(2, 2, CardinalPointsEnum.East,  2,  "4 2 E")]
        [InlineData(2, 2, CardinalPointsEnum.West,  1,  "1 2 W")]
        [InlineData(2, 2, CardinalPointsEnum.West,  2,  "0 2 W")]
        [InlineData(0, 0, CardinalPointsEnum.East,  10, "10 0 E")]
        [InlineData(2, 2, CardinalPointsEnum.North, 50,  "2 52 N")]
        public void MoveTest(int x, int y, CardinalPointsEnum facingTo, int moveCount, string expected)
        {
            Rover rover = new Rover(x, y, facingTo);
            rover.Start(ConcatString("M", moveCount));
            rover.Results.Should().Be(expected);
        }

        private string ConcatString(string value, int count)
        {
            string result = value;
            for(int i = 0; i < count-1; i++)
            {
                result += value;
            }
            return result;
        }


        [Theory]
        [InlineData(CardinalPointsEnum.North, SideEnum.Left,  CardinalPointsEnum.West)]
        [InlineData(CardinalPointsEnum.North, SideEnum.Right, CardinalPointsEnum.East)]
        [InlineData(CardinalPointsEnum.South, SideEnum.Left,  CardinalPointsEnum.East)]
        [InlineData(CardinalPointsEnum.South, SideEnum.Right, CardinalPointsEnum.West)]
        [InlineData(CardinalPointsEnum.East,  SideEnum.Left,  CardinalPointsEnum.North)]
        [InlineData(CardinalPointsEnum.East,  SideEnum.Right, CardinalPointsEnum.South)]
        [InlineData(CardinalPointsEnum.West,  SideEnum.Left,  CardinalPointsEnum.South)]
        [InlineData(CardinalPointsEnum.West,  SideEnum.Right, CardinalPointsEnum.North)]
        public void TurnTest(CardinalPointsEnum facingTo, SideEnum turnTo, CardinalPointsEnum expected)
        {
            Rover rover = new Rover(_x, _y, facingTo);
            string instruction = (turnTo == SideEnum.Left ? CommonConstants.Left : CommonConstants.Right).ToString();
            var result = rover.Start(instruction);
            result.FacingTo.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 2, CardinalPointsEnum.North, "LMLMLMLMM",    "1 3 N")]
        [InlineData(3, 3, CardinalPointsEnum.East,  "MMRMMRMRRM",   "5 1 E")]
        [InlineData(1, 2, CardinalPointsEnum.North, "LMLMLMLMMMMR", "1 5 E")]
        [InlineData(0, 0, CardinalPointsEnum.North, "L",            "0 0 W")]
        [InlineData(0, 0, CardinalPointsEnum.North, "R",            "0 0 E")]
        [InlineData(0, 0, CardinalPointsEnum.West,  "L",            "0 0 S")]
        [InlineData(0, 0, CardinalPointsEnum.West,  "R",            "0 0 N")]
        [InlineData(0, 0, CardinalPointsEnum.South, "L",            "0 0 E")]
        [InlineData(0, 0, CardinalPointsEnum.South, "R",            "0 0 W")]
        [InlineData(0, 0, CardinalPointsEnum.East,  "L",            "0 0 N")]
        [InlineData(0, 0, CardinalPointsEnum.East,  "R",            "0 0 S")]
        [InlineData(1, 1, CardinalPointsEnum.North, "M",            "1 2 N")]
        [InlineData(1, 1, CardinalPointsEnum.West,  "M",            "0 1 W")]
        [InlineData(1, 1, CardinalPointsEnum.South, "M",            "1 0 S")]
        [InlineData(1, 1, CardinalPointsEnum.East,  "M",            "2 1 E")]
        public void StartTest(int x, int y, CardinalPointsEnum facingTo, string instructions, string expected)
        {
            Rover rover = new Rover(x, y, facingTo);
            rover.Start(instructions);
            string result = rover.Results;
            result.Should().Be(expected);
        }
    }
}
