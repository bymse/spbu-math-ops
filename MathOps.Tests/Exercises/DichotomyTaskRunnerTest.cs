using System;
using MathOps.Exercises.OneDimensional;
using MathOps.Utilities;

namespace MathOps.Tests.Exercises
{
    public class DichotomyTaskRunnerTest : OneDimensionalMethodsTestBase
    {
        protected override OneDimensionalApproximateResult Execute(
            decimal a, decimal b, decimal precision, Boundaries boundaries)
        {
            return DichotomyTaskRunner.Execute(a, b, precision, 0.1M, boundaries);
        }
    }
}