using System;
using FluentAssertions;
using MathOps.Exercises;
using MathOps.Exercises.MultiDimensional;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.Exercises
{
    [TestFixture]
    public abstract class MultiDimensionalMethodsTestBase<T, TI> where T : TwoEpsilonsMethodsBase<TI>, new()
    {
        private const decimal FIRST_EPSILON = 0.1M;
        private const decimal SECOND_EPSILON = 0.15M;
        private const int MAX_ITERATIONS_COUNT = 10;
        protected virtual Vector2 FirstStartPoint => new Vector2(0, 0);
        protected virtual Vector2 SecondStartPoint => new Vector2(0.5M, 1);

        protected virtual Boundaries FirstBoundaries => new Boundaries(0, 1); 
        protected virtual Boundaries SecondBoundaries => new Boundaries(-1, 1);

        protected virtual void Observer(TI iteration)
        {
            ExerciseConsole.VerboseObserver(iteration);
        }

        [Test]
        public void TestFirstFunction()
        {
            var result = new T {Observer = Observer}.RunForFirst(
                FirstStartPoint, 
                FIRST_EPSILON, SECOND_EPSILON, MAX_ITERATIONS_COUNT, 
                FirstBoundaries);

            Console.WriteLine($"Result: {result.Arg}");
            
            result.Arg.First.Should().BeApproximately(1, SECOND_EPSILON);
            result.Arg.Second.Should().BeApproximately(1, SECOND_EPSILON);
        }

        [Test]
        public void TestSecondFunction()
        {
            var result = new T {Observer = Observer}.RunForSecond(
                SecondStartPoint,
                FIRST_EPSILON, SECOND_EPSILON, MAX_ITERATIONS_COUNT,
                SecondBoundaries);

            result.Arg.First.Should().BeApproximately(3, SECOND_EPSILON);
            result.Arg.Second.Should().BeApproximately(2, SECOND_EPSILON);
        }
    }
}