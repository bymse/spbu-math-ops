using System;
using System.Collections.Generic;
using MathOps.Dichotomy;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.OneDimensional
{
    [TestFixture]
    internal class DichotomyMethodExecutorTest : ExecutorTestBase<OneDimensionalApproximateResult, DichotomyIterationInfo>
    {
        private static decimal TestFunction(decimal x) => 2 * x * x - 12 * x;

        private const decimal PRECISION = 1;
        private const decimal EPSILON = 0.2M;
        private static readonly Boundaries Boundaries = new Boundaries(0, 10);

        protected override OneDimensionalApproximateResult ExecuteWithObserver(Action<DichotomyIterationInfo> observer)
        {
            var executor = new DichotomyMethodExecutor(TestFunction, observer);
            return executor.ExecuteMethod(PRECISION, EPSILON, Boundaries);
        }

        protected override OneDimensionalApproximateResult ExpectedResult => new OneDimensionalApproximateResult
        {
            Value = -17.9586718750M,
            Arg = 2.85625M,
            IterationsCount = 4,
            Boundaries = new Boundaries
            {
                Left = 2.45M,
                Right = 3.2625M
            }
        };

        protected override IReadOnlyList<DichotomyIterationInfo> ExpectedIterationsList => new[]
        {
            new DichotomyIterationInfo
            {
                Iteration = 0,
                LeftArg = 4.9M,
                RightArg = 5.1M,
                LeftArgResult = -10.78M,
                RightArgResult = -9.18M,
                NextIterationBoundaries = new Boundaries(0, 5.1M)
            },
            new DichotomyIterationInfo
            {
                Iteration = 1,
                LeftArg = 2.45M,
                RightArg = 2.65M,
                LeftArgResult = -17.395M,
                RightArgResult = -17.755M,
                NextIterationBoundaries = new Boundaries(2.45M, 5.1M)
            },
            new DichotomyIterationInfo
            {
                Iteration = 2,
                LeftArg = 3.675M,
                RightArg = 3.875M,
                LeftArgResult = -17.08875M,
                RightArgResult = -16.46875M,
                NextIterationBoundaries = new Boundaries(2.45M, 3.875M)
            },
            new DichotomyIterationInfo
            {
                Iteration = 3,
                LeftArg = 3.0625M,
                RightArg = 3.2625M,
                LeftArgResult = -17.99218750M,
                RightArgResult = -17.86218750M,
                NextIterationBoundaries = new Boundaries(2.45M, 3.2625M)
            },
        };
    }
}