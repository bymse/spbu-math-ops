using System;
using FluentAssertions;
using MathOps.Methods.GoldenSectionSearch;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.Exercises
{
    [TestFixture]
    public class DemoTest
    {
        private const decimal PRECISION = 0.015M;
        private const decimal RESULT = 0.0954745M;

        private static decimal Function(decimal arg)
        {
            return 13 * arg.Square().Square() - arg + 5 * arg.Square() - 10;
        }

        [Test]
        public void Test()
        {
            var executor = new GoldenSectionSearchExecutor(
                Function,
                iteration =>
                {
                    Console.WriteLine($"{iteration.NextBoundaries.Left}");
                    Console.WriteLine($"{iteration.NextBoundaries.Right}");
                    Console.WriteLine($"{iteration.Iteration}");
                    Console.WriteLine($"------------------------");
                });
            var res = executor.Execute(PRECISION, new Boundaries(0, 10));

            res.Arg.Should().BeApproximately(RESULT, PRECISION);
            Console.WriteLine(res.Arg);
        }
    }
}