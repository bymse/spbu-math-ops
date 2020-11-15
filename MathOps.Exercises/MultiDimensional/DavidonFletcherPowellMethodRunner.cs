using System;
using MathOps.Methods.DavidonFletcherPowellMethod;
using MathOps.Utilities;

namespace MathOps.Exercises.MultiDimensional
{
    public class DavidonFletcherPowellMethodRunner : TwoEpsilonsMethodsBase<DavidonFletcherPowellMethodIteration>
    {
        protected override TwoDimensionalApproximateResult ExecuteMethod(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount,
            Boundaries boundaries,
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<DavidonFletcherPowellMethodIteration> observer)
        {
            return new DavidonFletcherPowellMethodExecutor(
                    function,
                    gradient,
                    observer,
                    boundaries)
                .Execute(
                    startPoint,
                    firstEpsilon,
                    secondEpsilon,
                    maxIterationsCount);
        }
    }
}