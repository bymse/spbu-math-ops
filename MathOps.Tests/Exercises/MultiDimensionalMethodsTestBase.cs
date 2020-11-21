using System;
using FluentAssertions;
using MathOps.Exercises;
using MathOps.Exercises.MultiDimensional;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.Exercises
{
    [TestFixture]
    public abstract class MultiDimensionalMethodsTestBase<T, TI> where T : MultiDimensionalMethodsBase<TI>, new()
    {
        private const decimal FIRST_EPSILON = 0.1M;
        
        protected virtual decimal SecondEpsilonForFirst => 0.15M;
        protected virtual decimal SecondEpsilonForSecond => 0.15M;
        protected virtual decimal FirstEpsilonForFirst => FIRST_EPSILON;
        
        private const int MAX_ITERATIONS_COUNT = 9000;
        protected virtual Vector2 FirstStartPoint => new Vector2(0, 0);
        protected virtual Vector2 SecondStartPoint => new Vector2(0, 0);

        protected virtual Boundaries FirstBoundaries => new Boundaries(-100, 100); 
        protected virtual Boundaries SecondBoundaries => new Boundaries(-100, 100);

        protected virtual void Observer(TI iteration)
        {
            ExerciseConsole.VerboseObserver(iteration);
        }

        [Test]
        public void TestFirstFunction()
        {
            var result = new T {Observer = Observer}.RunForFirst(
                FirstStartPoint, 
                FirstEpsilonForFirst, SecondEpsilonForFirst, MAX_ITERATIONS_COUNT, 
                FirstBoundaries);

            Console.WriteLine($"Результат. Значение аргумента: {result.Arg}");

            result.Arg.First.Should().BeApproximately(1, SecondEpsilonForFirst);
            result.Arg.Second.Should().BeApproximately(1, SecondEpsilonForFirst);
        }

        [Test]
        public void TestSecondFunction()
        {
            var result = new T {Observer = Observer}.RunForSecond(
                SecondStartPoint,
                FIRST_EPSILON, SecondEpsilonForSecond, MAX_ITERATIONS_COUNT,
                SecondBoundaries);

            result.Arg.First.Should().BeApproximately(3, SecondEpsilonForSecond);
            result.Arg.Second.Should().BeApproximately(2, SecondEpsilonForSecond);
        }
    }
}