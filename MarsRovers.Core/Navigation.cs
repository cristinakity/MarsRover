using MarsRovers.Core.Enums;
using MarsRovers.Core.Extensions;
using MarsRovers.Core.Models;
using System.Collections.Generic;

namespace MarsRovers.Core
{
    public class Navigation
    {
        private bool _showLog = false;
        private Logger _log;

        public Navigation(bool showLog = false)
        {
            _showLog = showLog;
            _log = new Logger(_showLog);
        }

        public string Navigate(string inputData)
        {
            string results = "";
            List<string> resultList = new List<string>();
            var lines = inputData.Split("\n");
            var plateau = lines[0].Split(" ");
            if(lines.Length > 0)
            {
                Rover rover = null;
                for(int i = 1; i < lines.Length; i++)
                {
                    if(i%2 == 0)
                    {
                        //even number means it is the rover navigation instructions
                        if(rover != null)
                        {
                            try
                            {
                                rover.Start(lines[i]);
                            }
                            catch (System.Exception ex)
                            {
                                _log.Log(ex);
                            }
                            resultList.Add(rover.Results);
                        }
                    }
                    else
                    {
                        //odd number means it is the rover coordinates
                        var arraySettings = lines[i].Split(" ");
                        RoverSettings settings = new RoverSettings()
                        {
                            LimitX = int.Parse(plateau[0]),
                            LimitY = int.Parse(plateau[1]),
                            X = int.Parse(arraySettings[0]),
                            Y = int.Parse(arraySettings[1]),
                            FacingTo = arraySettings[2][0].ToEnum<CardinalPointsEnum>(),
                            ShowLog = _showLog
                        };
                        rover = new Rover(settings);
                    }
                }
            }
            results = string.Join('\n', resultList);
            return results;
        }
    }
}
