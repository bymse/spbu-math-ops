using System;
using MathOps.Methods.AcceleratedGradientDescent;
using MathOps.Utilities;

namespace MathOps.Exercises.MultiDimensional
{
    public class AcceleratedGradientDescentRunner : MultiDimensionalMethodsBase<AcceleratedGradientDescentIteration>
    {
        protected override TwoDimensionalApproximateResult ExecuteMethod(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount,
            Boundaries boundaries,
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<AcceleratedGradientDescentIteration> observer)
        {
            return new AcceleratedGradientDescentExecutor(
                    function,
                    gradient,
                    observer,
                    boundaries)
                .Execute(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount);
        }
    }
}