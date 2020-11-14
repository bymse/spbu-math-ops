using System;

namespace MathOps.Utilities
{
    public static class DecimalExtensions
    {
        public static decimal RoundTo(this decimal @decimal, int precision)
        {
            return Math.Round(@decimal, precision, MidpointRounding.ToZero);
        }

        public static decimal Square(this decimal @decimal) => @decimal * @decimal;
    }
}