using System;
using MathOps.Exercises.MultiDimensional;
using MathOps.Methods.DavidonFletcherPowellMethod;

namespace MathOps.Tests.Exercises
{
    public class DavidonFletcherPowellMethodRunnerTest : MultiDimensionalMethodsTestBase<
        DavidonFletcherPowellMethodRunner, DavidonFletcherPowellMethodIteration>
    {
        protected override void Observer(DavidonFletcherPowellMethodIteration iteration)
        {
            Console.WriteLine($"Итерация {iteration.Iteration}");
            Console.WriteLine($"Норма градиента: {iteration.GradientIterationValue.Norm()}");
            Console.WriteLine($"Значение аргумента: {iteration.IterationArg}");
            
            Console.WriteLine();
        }
    }
}