using System;
using System.Collections.Generic;
using MathOps.Methods.DavidonFletcherPowellMethod;
using MathOps.Utilities;

namespace MathOps.Tests.MultiDimensional
{
    public class DavidonFletcherPowellMethodExecutorTest
        : ExecutorTestBase<TwoDimensionalApproximateResult, DavidonFletcherPowellMethodIteration>
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

        private static decimal StepFunctions(Vector2 arg, Vector2 gradientVal, Vector2Matrix matrix)
        {
            var a = arg.First;
            var c = gradientVal.First;
            var f = matrix.FirstLine.First;
            var l = matrix.SecondLine.First;
            var d = gradientVal.Second;
            var g = matrix.FirstLine.Second;
            var m = matrix.SecondLine.Second;
            var b = arg.Second;

            var numerator = 4 * a * c * f + a * c * l + 4 * a * d * g + a * d * m + b * c * f + 2 * b * c * l +
                            b * d * g + 2 * b * d * m;

            var denominator = 2 * (2 * c.Square() * f.Square() + c.Square() * f * l + c.Square() * l.Square() +
                                   4 * c * d * f * g + c * d * f * m + c * d * g * l + 2 * c * d * l * m +
                                   2 * d.Square() * g.Square() + d.Square() * g * m + d.Square() * m.Square());

            return numerator / denominator;
        }


        protected override TwoDimensionalApproximateResult ExpectedResult => new TwoDimensionalApproximateResult
        {
            Arg = new Vector2(0, 0),
            IterationsCount = 2,
            Value = 0
        };

        protected override IReadOnlyList<DavidonFletcherPowellMethodIteration> ExpectedIterationsList => new[]
        {
            new DavidonFletcherPowellMethodIteration
            {
                Iteration = 0,
                GradientIterationValue = new Vector2(3, 2.5M),
                Step = 0.24015M,
                GradientsValuesDifference = null,
                NextIterationArg = new Vector2(-0.22045M, 0.39962M),
                IterationArg = StartPoint,
                IterationMatrix = new Vector2Matrix()
                {
                    FirstLine = new Vector2(1, 0),
                    SecondLine = new Vector2(0, 1),
                }
            },
            new DavidonFletcherPowellMethodIteration
            {
                Iteration = 1,
                IterationArg = new Vector2(-0.22045M, 0.39962M),
                GradientIterationValue = new Vector2(-0.48218M, 0.57879M),
                GradientsValuesDifference = new Vector2(-3.48218M, -1.92121M),
                IterationAdditionalMatrix = new Vector2Matrix
                {
                    FirstLine = new Vector2(-0.62490M, -0.30486M),
                    SecondLine = new Vector2(-0.30486M, -0.13493M)
                },
                IterationMatrix = new Vector2Matrix
                {
                    FirstLine = new Vector2(0.37510M, -0.30486M),
                    SecondLine = new Vector2(-0.30486M, 0.86507M)
                },
                Step = 0.61698M,
                NextIterationArg = new Vector2(0, 0)
            },
        };

        protected override TwoDimensionalApproximateResult ExecuteWithObserver(
            Action<DavidonFletcherPowellMethodIteration> observer)
        {
            return new DavidonFletcherPowellMethodExecutor(Function, Gradient, observer, StepFunctions)
                .Execute(StartPoint, FIRST_EPSILON, SECOND_EPSILON, MAX_ITERATIONS_COUNT);
        }
    }
}