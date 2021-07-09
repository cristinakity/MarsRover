using MarsRovers.Core.Enums;
using System;

namespace MarsRovers.Core.Models
{
    public class RoverSettings
    {
        /// <summary>
        /// Name or Identifier fo this rover
        /// </summary>
        public string Name { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// X position of the Rover
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y position of the Rover
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Where the Rover is facing to (North, South, East or West)
        /// </summary>
        public CardinalPointsEnum FacingTo { get; set; }
        /// <summary>
        /// Indicate if the program will log what is happening
        /// </summary>
        public bool ShowLog { get; set; } = false;
        /// <summary>
        /// X limit for the Plateau
        /// </summary>
        public int LimitX { get; set; } = Int32.MaxValue;
        /// <summary>
        /// Y limit for the Plateau
        /// </summary>
        public int LimitY { get; set; } = Int32.MaxValue;
    }
}
