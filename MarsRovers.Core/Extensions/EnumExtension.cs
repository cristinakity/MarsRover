using MarsRovers.Core.Constants;
using System;

namespace MarsRovers.Core.Extensions
{
    public static class EnumExtension
    {
        public static char ToChar<T>(this T enumItem)
        where T : Enum
        {
            return enumItem.ToString()[0];
        }
        
        public static T ToEnum<T>(this Char enumItem)
        where T : Enum
        {
            foreach(string name in Enum.GetNames(typeof(T)))
            {
                if(name[0] == enumItem)
                {
                    T key = (T)Enum.Parse(typeof(T), name);
                    return key;
                }
            }
            throw new Exception("Enum item was no found");
        }

        public static bool IsSide(this char item)
        {
            item = item.ToString().ToUpper()[0];
            return item == CommonConstants.Left || item == CommonConstants.Right;
        }

        public static bool IsCardinalPoint(this char item)
        {
            item = item.ToString().ToUpper()[0];
            return item == CommonConstants.North || item == CommonConstants.South || item == CommonConstants.East || item == CommonConstants.West;
        }
    }
}
