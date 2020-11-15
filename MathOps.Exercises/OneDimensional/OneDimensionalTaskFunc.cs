using System;
using static MathOps.Exercises.ExerciseConsole;

namespace MathOps.Exercises.OneDimensional
{
    public static class OneDimensionalTaskFunc
    {
        public static Func<decimal, decimal> BuildFunc(decimal a, decimal b)
        {
            return x => (decimal) ((double)a / Math.Exp((double) x)) + b * x;
        }
    }
}