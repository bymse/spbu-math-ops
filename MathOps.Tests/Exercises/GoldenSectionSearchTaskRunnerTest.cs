using MathOps.Exercises.OneDimensional;
using MathOps.Utilities;

namespace MathOps.Tests.Exercises
{
    public class GoldenSectionSearchTaskRunnerTest : OneDimensionalMethodsTestBase
    {
        protected override OneDimensionalApproximateResult Execute(decimal a, decimal b, decimal precision, Boundaries boundaries)
        {
            return GoldenSectionSearchTaskRunner.Execute(a, b, precision, boundaries);
        }
    }
}