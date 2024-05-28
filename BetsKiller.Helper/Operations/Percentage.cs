using System;

namespace BetsKiller.Helper.Operations
{
    public class Percentage
    {
        public static decimal GetPercentage(decimal value)
        {
            return Math.Round(value * 100, 2);
        }

        /// <param name="c">Current percentage</param>
        /// <param name="p">Past percentage</param>
        public static decimal GetRawPercentage(decimal c, decimal p)
        {
            return Math.Round((c - p) / Math.Abs(p) * 100, 2);
        }
    }
}
