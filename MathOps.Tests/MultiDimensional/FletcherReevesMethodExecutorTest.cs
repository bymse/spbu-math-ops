using System;
using System.Collections.Generic;
using MathOps.Methods.FletcherReevesMethod;
using MathOps.Utilities;

namespace MathOps.Tests.MultiDimensional
{
    public class FletcherReevesMethodExecutorTest 
        : ExecutorTestBase<TwoDimensionalApproximateResult, FletcherReevesMethodIteration>
    {
        private const decimal FIRST_EPSILON = 0.1M;
        private const decimal SECOND_EPSILON = 0.15M;
        private const int MAX_ITERATIONS_COUNT = 10;
        private static readonly Vector2 StartPoint = new Vector2(0.5M, 1);

        private static readonly Func<Vector2, decimal> Function = vector2 =>
        {
            var (first, second) = vector2;
            return 2 * first * first + first * second + second * second;
        };

        private static readonly TwoDimensionalGradient Gradient = new TwoDimensionalGradient
        {
            First = vector2 => 4 * vector2.First + vector2.Second,
            Second = vector2 => vector2.First + 2 * vector2.Second
        };

        private static readonly Func<Vector2, Vector2, decimal> StepFunction = (gradVal, direction) =>
        {
            var (gradValFirst, gradValSecond) = gradVal;
            var (directionValFirst, directionValSecond) = direction;
            
            var numerator = gradValFirst * (-4 * directionValFirst - directionValSecond) +
                            gradValSecond * (-directionValFirst - 2 * directionValSecond);

            var denominator = (4 * directionValFirst.Square() + 2 * directionValFirst * directionValSecond +
                               2 * directionValSecond.Square());
            return numerator / denominator;
        };

        
        private static readonly Func<Vector2, decimal> FirstStepFunction = vector2 =>
        {
            var (first, second) = vector2;
            var numeratorLeft = 4 * first + second;
            numeratorLeft *= numeratorLeft;
            var numeratorRight = first + 2 * second;
            numeratorRight *= numeratorRight;
            var numerator = numeratorLeft + numeratorRight;

            var denominatorFirst = 4 * first + second;
            denominatorFirst *= denominatorFirst;

            var denominatorSecond = (4 * first + second) * (first + 2 * second);

            var denominatorThird = (first + 2 * second);
            denominatorThird *= denominatorThird;

            var denominator = 4 * denominatorFirst + 2 * denominatorSecond + 2 * denominatorThird;

            return numerator / denominator;
        };

        protected override TwoDimensionalApproximateResult ExpectedResult => new TwoDimensionalApproximateResult
        {
            Arg = new Vector2(-0.00003057560M, -0.00001681950M),
            Value = 0,
            IterationsCount = 2
        };

        protected override IReadOnlyList<FletcherReevesMethodIteration> ExpectedIterationsList => new[]
        {
            new FletcherReevesMethodIteration
            {
                Iteration = 0,
                GradientIterationValue = new Vector2(3, 2.5M),
                Direction = -new Vector2(3, 2.5M),
                Step = 0.24015M,
                BetaValue = null, 
                NextArg = new Vector2(-0.220450M, 0.399625M),
            },
            new FletcherReevesMethodIteration
            {
                Iteration = 1,
                GradientIterationValue = new Vector2(-0.48217M, 0.5788M),
                BetaValue = 0.03721M,
                Direction = new Vector2(0.370540M, -0.671825M),
                Step = 0.59486M,
                NextArg = new Vector2(-0.00003057560M, -0.00001681950M)
            },
        };


        protected override TwoDimensionalApproximateResult ExecuteWithObserver(
            Action<FletcherReevesMethodIteration> observer)
        {
            return new FletcherReevesMethodExecutor(Function, Gradient, StepFunction, observer, FirstStepFunction)
                .Execute(StartPoint, FIRST_EPSILON, SECOND_EPSILON, MAX_ITERATIONS_COUNT);
        }
    }
}