using MarsRovers.Core.Enums;
using MarsRovers.Core.Extensions;
using MarsRovers.Core.Constants;
using MarsRovers.Core.Models;

namespace MarsRovers.Core
{
    public class Rover
    {
        public RoverSettings Settings { get; }
        private Logger _log;

        public string Results
        {
            get
            {
                return $"{Settings.X} {Settings.Y} {Settings.FacingTo.ToChar()}";
            }
        }

        /// <summary>
        /// This is the Rover Class represent on Mars Rover, it should be initialized with the curren position
        /// </summary>
        /// <param name="x">X Position | Horizontal</param>
        /// <param name="y">Y Position | Vertical</param>
        /// <param name="facingTo">One of the Cardinal Points this rover is facing to</param>
        public Rover(int x, int y, CardinalPointsEnum facingTo) : this(new RoverSettings(){ X = x, Y = y, FacingTo = facingTo })
        {
        }

        /// <summary>
        /// This is the Rover Class represent on Mars Rover, it should be initialized with the curren position
        /// </summary>
        /// <param name="settings">RoverSettings class which include start position and cardinal point in where the rover is facing to</param>
        public Rover(RoverSettings settings)
        {            
            Settings = settings;
            _log = new Logger(Settings.ShowLog);
            _log.Log($"Rover({Settings.Name}) initialized.");
            LogCurrentPosition();
        }

        private void LogCurrentPosition()
        {
            _log.Log($"Position: X={Settings.X}, Y={Settings.Y}, Facing to={Settings.FacingTo.ToString()}");
        }

        /// <summary>
        /// Start the processing of the instrucctions sended in the sequence variable
        /// </summary>
        /// <param name="sequence">It contains the list of instructions the rover need to follow</param>
        /// <returns></returns>
        public (int X, int Y, CardinalPointsEnum FacingTo) Start(string sequence)
        {
            foreach(char instruction in sequence)
            {
                switch (instruction)
                {
                    case CommonConstants.Left:
                        _log.Log($"Rover({Settings.Name}) Left turn.");
                        this.Turn(SideEnum.Left);
                        LogCurrentPosition();
                        break;
                    case CommonConstants.Right:
                        _log.Log($"Rover({Settings.Name}) Right turn.");
                        this.Turn(SideEnum.Right);
                        LogCurrentPosition();
                        break;
                    case CommonConstants.Move:
                        _log.Log($"Rover({Settings.Name}) Move.");
                        this.Move();
                        LogCurrentPosition();
                        break;
                }
            }
            return (Settings.X, Settings.Y, Settings.FacingTo);
        }

        /// <summary>
        /// It will turn the Rover according to the side (left or right)
        /// </summary>
        /// <param name="side">Side in which direction will the rover turn (Left or Right) </param>
        private void Turn(SideEnum side)
        {
            switch (Settings.FacingTo, side)
            {
                case (CardinalPointsEnum cp, SideEnum s) when (cp == CardinalPointsEnum.North && s == SideEnum.Left):
                    Settings.FacingTo = CardinalPointsEnum.West;
                    break;
                case (CardinalPointsEnum cp, SideEnum s) when (cp == CardinalPointsEnum.North && s == SideEnum.Right):
                    Settings.FacingTo = CardinalPointsEnum.East;
                    break;
                case (CardinalPointsEnum cp, SideEnum s) when (cp == CardinalPointsEnum.South && s == SideEnum.Left):
                    Settings.FacingTo = CardinalPointsEnum.East;
                    break;
                case (CardinalPointsEnum cp, SideEnum s) when (cp == CardinalPointsEnum.South && s == SideEnum.Right):
                    Settings.FacingTo = CardinalPointsEnum.West;
                    break;
                case (CardinalPointsEnum cp, SideEnum s) when (cp == CardinalPointsEnum.East && s == SideEnum.Left):
                    Settings.FacingTo = CardinalPointsEnum.North;
                    break;
                case (CardinalPointsEnum cp, SideEnum s) when (cp == CardinalPointsEnum.East && s == SideEnum.Right):
                    Settings.FacingTo = CardinalPointsEnum.South;
                    break;
                case (CardinalPointsEnum cp, SideEnum s) when (cp == CardinalPointsEnum.West && s == SideEnum.Left):
                    Settings.FacingTo = CardinalPointsEnum.South;
                    break;
                case (CardinalPointsEnum cp, SideEnum s) when (cp == CardinalPointsEnum.West && s == SideEnum.Right):
                    Settings.FacingTo = CardinalPointsEnum.North;
                    break;
            }
        }

        /// <summary>
        /// Move the rover one position from its current position
        /// </summary>
        private void Move()
        {
            var newPosition = 0;
            switch (Settings.FacingTo)
            {
                case CardinalPointsEnum.North:
                    newPosition = Settings.Y + 1;
                    if(newPosition > Settings.LimitY)
                    {
                        newPosition = Settings.LimitY;
                        LogOutOfLimmit(Settings.LimitY);
                    }
                    Settings.Y = newPosition;
                    break;
                case CardinalPointsEnum.South:
                    newPosition = Settings.Y - 1;
                    if (newPosition < 0)
                    {
                        newPosition = 0;
                        LogOutOfLimmit(0);
                    }
                    Settings.Y = newPosition;
                    break;
                case CardinalPointsEnum.East:
                    newPosition = Settings.X + 1;
                    if (newPosition > Settings.LimitX)
                    {
                        newPosition = Settings.LimitX;
                        LogOutOfLimmit(Settings.LimitX);
                    }
                    Settings.X = newPosition;
                    break;
                case CardinalPointsEnum.West:
                    newPosition = Settings.X - 1;
                    if (newPosition < 0)
                    {
                        newPosition = 0;
                        LogOutOfLimmit(0);
                    }
                    Settings.X = newPosition;
                    break;
            }
        }

        private void LogOutOfLimmit(int limit)
        {
            _log.Log($"Cannot move out of the limit={limit}", MessageType.Warning);
        }
    }
}
