using System;
using FluentAssertions;
using MathOps.Exercises.MultiDimensional;
using MathOps.Methods.AcceleratedGradientDescent;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.Exercises
{
    [TestFixture]
    public class AcceleratedGradientDescentRunnerTest : MultiDimensionalMethodsTestBase<AcceleratedGradientDescentRunner, AcceleratedGradientDescentIteration>
    {
        protected override decimal FirstEpsilonForFirst => 0.0000005M;
        protected override decimal SecondEpsilonForFirst => 0.000001M;

        protected override Boundaries FirstBoundaries => new Boundaries(0, 1000);

        protected override void Observer(AcceleratedGradientDescentIteration iteration)
        {
            Console.WriteLine($"Итерация {iteration.Iteration}");
            Console.WriteLine($"Норма градиента: {iteration.GradientIterationValue.Norm()}");
            Console.WriteLine($"Значение аргумента: {iteration.Arg}");
            
            Console.WriteLine();
        }
    }
}