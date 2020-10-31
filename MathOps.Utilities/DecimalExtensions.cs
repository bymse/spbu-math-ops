using System;

namespace MathOps.Utilities
{
    public static class DecimalExtensions
    {
        public static decimal RoundTo(this decimal @decimal, int precision)
        {
            return Math.Round(@decimal, precision);
        }
    }
}