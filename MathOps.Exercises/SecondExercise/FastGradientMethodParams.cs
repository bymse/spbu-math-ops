using System;
using MathOps.Utilities;

namespace MathOps.Exercises.SecondExercise
{
    public class FastGradientMethodParams
    {
        public Func<Vector2, decimal> Function { get; set; }
        public TwoDimensionalGradient Gradient { get; set; }
        public Func<Vector2, Vector2, decimal> StepFunction { get; set; }
    }
}