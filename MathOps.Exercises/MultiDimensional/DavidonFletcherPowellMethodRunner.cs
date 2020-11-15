using System;
using MathOps.Methods.DavidonFletcherPowellMethod;
using MathOps.Utilities;

namespace MathOps.Exercises.MultiDimensional
{
    public class DavidonFletcherPowellMethodRunner : TwoEpsilonsMethodsBase
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
            return new DavidonFletcherPowellMethodExecutor(
                    function,
                    gradient,
                    iteration => { },
                    boundaries)
                .Execute(
                    startPoint,
                    firstEpsilon,
                    secondEpsilon,
                    maxIterationsCount);
        }
    }
}