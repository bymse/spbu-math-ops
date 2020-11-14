using System;
using System.Collections.Generic;
using MathOps.Methods.FastGradientMethod;
using MathOps.Utilities;

namespace MathOps.Tests.MultiDimensional
{
    public class FastGradientMethodExecutorTest
        : ExecutorTestBase<TwoDimensionalApproximateResult, FastGradientMethodIteration>
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

        private static readonly Func<Vector2, decimal> StepFunction = vector2 =>
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
            IterationsCount = 3,
            Arg = new Vector2(-0.0185890147M, 0.0337061288M),
            Value = 0.0012M
        };

        protected override IReadOnlyList<FastGradientMethodIteration> ExpectedIterationsList => new[]
        {
            new FastGradientMethodIteration
            {
                Iteration = 0,
                GradientIterationValue = new Vector2(3, 2.5M),
                Step = 0.24015M,
                NextArg = new Vector2(-0.220450M, 0.399625M),
            },
            new FastGradientMethodIteration
            {
                Iteration = 1,
                GradientIterationValue = new Vector2(-0.48217M, 0.57880M),
                Step = 0.54471M,
                NextArg = new Vector2(0.0421928207M, 0.0843468520M)
            },
            new FastGradientMethodIteration
            {
                Iteration = 2,
                GradientIterationValue = new Vector2(0.25311M, 0.21088M),
                Step = 0.24014M,
                NextArg = new Vector2(-0.0185890147M, 0.0337061288M),
            },
        };

        protected override TwoDimensionalApproximateResult ExecuteWithObserver(
            Action<FastGradientMethodIteration> observer)
        {
            return new FastGradientMethodExecutor(
                    Function,
                    Gradient,
                    StepFunction,
                    observer)
                .Execute(StartPoint, FIRST_EPSILON, SECOND_EPSILON, MAX_ITERATIONS_COUNT);
        }
    }
}