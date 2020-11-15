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
        protected override Boundaries FirstBoundaries => new Boundaries(0,1);
        protected override Vector2 FirstStartPoint => new Vector2(-1, -1);

        protected override void Observer(AcceleratedGradientDescentIteration iteration)
        {
            Console.WriteLine($"Gradient value: {iteration.GradientIterationValue}");
            Console.WriteLine($"Func value: {iteration.FuncValue}");
        }
    }
}