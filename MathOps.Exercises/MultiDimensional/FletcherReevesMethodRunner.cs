using System;
using MathOps.Methods.FletcherReevesMethod;
using MathOps.Utilities;

namespace MathOps.Exercises.MultiDimensional
{
    public class FletcherReevesMethodRunner : TwoEpsilonsMethodsBase<FletcherReevesMethodIteration>
    {
        protected override TwoDimensionalApproximateResult ExecuteMethod(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount,
            Boundaries boundaries,
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<FletcherReevesMethodIteration> observer)
        {
            return new FletcherReevesMethodExecutor(
                    function,
                    gradient,
                    observer,
                    boundaries)
                .Execute(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount);
        }
    }
}