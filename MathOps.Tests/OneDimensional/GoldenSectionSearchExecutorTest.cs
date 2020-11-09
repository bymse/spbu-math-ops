using System;
using System.Collections.Generic;
using MathOps.Methods.GoldenSectionSearch;
using MathOps.Utilities;

namespace MathOps.Tests.OneDimensional
{
    public class GoldenSectionSearchExecutorTest
        : ExecutorTestBase<OneDimensionalApproximateResult, GoldenSectionSearchIteration>
    {
        private static decimal TestFunction(decimal x) => 2 * x * x - 12 * x;
        private static readonly Boundaries Boundaries = new Boundaries(0, 10);
        private const decimal PRECISION = 1;

        protected override OneDimensionalApproximateResult ExpectedResult => new OneDimensionalApproximateResult
        {
            IterationsCount = 5,
            Arg = 2.81M,
            Value = -17.9278M,
            Boundaries = new Boundaries(2.36M, 3.26M),
        };


        protected override IReadOnlyList<GoldenSectionSearchIteration> ExpectedIterationsList => new[]
        {
            new GoldenSectionSearchIteration
            {
                Iteration = 0,
                NextLeftArg = 2.36M,
                NextRightArg = 3.82M,
                LeftArgResult = -16.6552M,
                RightArgResult = 2.2248M,
                NextBoundaries = new Boundaries(0, 6.18M)
            },
            new GoldenSectionSearchIteration
            {
                Iteration = 1,
                NextLeftArg = 1.46M,
                NextRightArg = 2.36M,
                LeftArgResult = -17.180800M,
                RightArgResult = -16.655200M,
                NextBoundaries = new Boundaries(0, 3.82M)
            },
            new GoldenSectionSearchIteration
            {
                Iteration = 2,
                NextLeftArg = 2.36M,
                NextRightArg = 2.92M,
                LeftArgResult = -13.256800M,
                RightArgResult = -17.180800M,
                NextBoundaries = new Boundaries(1.46M, 3.82M)
            },
            new GoldenSectionSearchIteration
            {
                Iteration = 3,
                NextLeftArg = 2.92M,
                NextRightArg = 3.26M,
                LeftArgResult = -17.180800M,
                RightArgResult = -17.987200M,
                NextBoundaries = new Boundaries(2.36M, 3.82M)
            },
            new GoldenSectionSearchIteration
            {
                Iteration = 4,
                NextLeftArg = 2.7M,
                NextRightArg = 2.92M,
                LeftArgResult = -17.987200M,
                RightArgResult = -17.864800M,
                NextBoundaries = new Boundaries(2.36M, 3.26M)
            },
        };

        protected override OneDimensionalApproximateResult ExecuteWithObserver(Action<GoldenSectionSearchIteration> observer)
        {
            return new GoldenSectionSearchExecutor(TestFunction, observer).Execute(PRECISION, Boundaries);
        }
    }
}