using System;

namespace ToolBox.Extensions.Randoms
{
    public static class RandomExtensions
    {
        public static DateTime GetDateBetween(this Random random, DateTime from, DateTime to)
        {
            TimeSpan diference = to - from;
            int days = (int)diference.TotalDays;
            int randomDays = random.Next(days);
            DateTime result = from.AddDays(randomDays);
            return result;
        }
    }
}
