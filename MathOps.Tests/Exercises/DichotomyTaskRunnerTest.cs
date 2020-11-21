using System;
using MathOps.Exercises.OneDimensional;
using MathOps.Methods.Dichotomy;
using MathOps.Utilities;

namespace MathOps.Tests.Exercises
{
    public class DichotomyTaskRunnerTest : OneDimensionalMethodsTestBase
    {
        protected override OneDimensionalApproximateResult Execute(
            decimal a, decimal b, decimal precision,
            Boundaries boundaries)
        {
            return DichotomyTaskRunner.Execute(a, b, precision, 0.1M, boundaries, Observer);
        }

        private static void Observer(DichotomyIterationInfo iteration)
        {
            Console.WriteLine($"Итерация {iteration.Iteration}");
            Console.WriteLine($"Левый аргумент для итерации: {iteration.LeftArg}");
            Console.WriteLine($"Правый аргумент для итерации: {iteration.RightArg}");
            Console.WriteLine($"Границы для итерации: {iteration.IterationBoundaries}");
            Console.WriteLine();
        }
    }
}