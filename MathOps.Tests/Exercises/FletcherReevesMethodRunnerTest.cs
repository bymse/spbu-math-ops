using System;
using MathOps.Exercises.MultiDimensional;
using MathOps.Methods.FletcherReevesMethod;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.Exercises
{
    [TestFixture]
    public class FletcherReevesMethodRunnerTest : MultiDimensionalMethodsTestBase<FletcherReevesMethodRunner, FletcherReevesMethodIteration>
    {
        protected override Vector2 SecondStartPoint => new Vector2(1.7M, 1.7M);

        protected override void Observer(FletcherReevesMethodIteration iteration)
        {
            Console.WriteLine($"Итерация {iteration.Iteration}");
            Console.WriteLine($"Норма градиента: {iteration.GradientIterationValue.Norm()}");
            Console.WriteLine($"Значение аргумента: {iteration.Arg}");
            
            Console.WriteLine();
        }
    }
}