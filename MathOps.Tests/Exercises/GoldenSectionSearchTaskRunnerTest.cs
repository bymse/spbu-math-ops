using System;
using MathOps.Exercises.OneDimensional;
using MathOps.Methods.GoldenSectionSearch;
using MathOps.Utilities;

namespace MathOps.Tests.Exercises
{
    public class GoldenSectionSearchTaskRunnerTest : OneDimensionalMethodsTestBase
    {
        protected override OneDimensionalApproximateResult Execute(decimal a, decimal b, decimal precision, Boundaries boundaries)
        {
            return GoldenSectionSearchTaskRunner.Execute(a, b, precision, boundaries, Observer);
        }

        private static void Observer(GoldenSectionSearchIteration iteration)
        {
            Console.WriteLine($"Итерация {iteration.Iteration}");
            Console.WriteLine($"Левый аргумент для итерации: {iteration.LeftArg}");
            Console.WriteLine($"Правый аргумент для итерации: {iteration.RightArg}");
            Console.WriteLine($"Границы для итерации: {iteration.Boundaries}");
            Console.WriteLine();
        }
    }
}