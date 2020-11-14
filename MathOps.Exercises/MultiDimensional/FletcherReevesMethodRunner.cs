using System;
using MathOps.Methods.FletcherReevesMethod;
using MathOps.Utilities;

namespace MathOps.Exercises.MultiDimensional
{
    public class FletcherReevesMethodRunner : TwoEpsilonsMethodsBase
    {
        protected override TwoDimensionalApproximateResult ExecuteMethod(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount,
            Boundaries boundaries,
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient)
        {
            return new FletcherReevesMethodExecutor(
                    function,
                    gradient,
                    iteration => { },
                    boundaries)
                .Execute(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount);
        }
    }
}