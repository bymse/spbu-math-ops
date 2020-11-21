using System;
using System.Collections.Generic;
using FluentAssertions;
using MathOps.Exercises.MultiDimensional;
using MathOps.Methods.NelderMeadMethod;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.Exercises
{
    [TestFixture]
    public class NelderMeadMethodTaskRunnerTest
    {
        private const decimal BOUNDARY_COEFF = 0.001M;

        private readonly NelderMeadMethodCoeffs coeffs = new NelderMeadMethodCoeffs()
        {
            ReflectionCoeff = 1,
            ContractionCoeff = 0.5M,
            ExpansionCoeff = 2
        };

        private readonly IReadOnlyList<Vector2> startVertexes = new[]
        {
            new Vector2(-1, 0.3M),
            new Vector2(0.5M, 4),
            new Vector2(-3, 0),
        };

        private static void Observe(NelderMeadMethodIteration iteration)
        {
            Console.WriteLine($"Итерация {iteration.Iteration}.");
            Console.WriteLine($"Вершина 1: {iteration.VertexList[0]}");
            Console.WriteLine($"Вершина 2: {iteration.VertexList[1]}");
            Console.WriteLine($"Вершина 3: {iteration.VertexList[2]}");
            
            Console.WriteLine();
        }

        [Test]
        public void TestFirstFunction()
        {
            var result = NelderMeadMethodTaskRunner.RunForFirst(startVertexes, coeffs, BOUNDARY_COEFF, Observe);

            result.Arg.First.Should().BeApproximately(1, 0.15M);
            result.Arg.Second.Should().BeApproximately(1, 0.15M);
            
            WriteResult(result);
        }

        [Test]
        public void TestSecondFunction()
        {
            var result = NelderMeadMethodTaskRunner.RunForSecond(startVertexes, coeffs, BOUNDARY_COEFF, Observe);

            result.Arg.First.Should().BeApproximately(-2.80512M, 0.15M);
            result.Arg.Second.Should().BeApproximately(3.13131M, 0.15M);

            WriteResult(result);
        }

        private void WriteResult(TwoDimensionalApproximateResult result)
        {
            Console.WriteLine($"Результат. Значение аргумента: {result.Arg}");
        }
    }
}