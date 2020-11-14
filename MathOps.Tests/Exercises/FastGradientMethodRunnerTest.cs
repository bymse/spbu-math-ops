using FluentAssertions;
using MathOps.Exercises.MultiDimensional;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.Exercises
{
    [TestFixture]
    public class FastGradientMethodRunnerTest
    {
        private const decimal FIRST_EPSILON = 0.1M;
        private const decimal SECOND_EPSILON = 0.15M;
        private const int MAX_ITERATIONS_COUNT = 10;
        private static readonly Vector2 FirstStartPoint = new Vector2(1, 1);
        private static readonly Vector2 SecondStartPoint = new Vector2(0.5M, 1);


        [Test]
        public void TestFirstFunction()
        {
            var result = new FastGradientMethodRunner().RunForFirst(
                FirstStartPoint, 
                FIRST_EPSILON, SECOND_EPSILON, MAX_ITERATIONS_COUNT, 
                new Boundaries(-100, 100));

            result.Arg.First.Should().BeApproximately(1, SECOND_EPSILON);
            result.Arg.Second.Should().BeApproximately(1, SECOND_EPSILON);
        }

        [Test]
        public void TestSecondFunction()
        {
            var result = new FastGradientMethodRunner().RunForSecond(
                SecondStartPoint,
                FIRST_EPSILON, SECOND_EPSILON, MAX_ITERATIONS_COUNT,
                new Boundaries(-1, 1));

            result.Arg.First.Should().BeApproximately(3, SECOND_EPSILON);
            result.Arg.Second.Should().BeApproximately(2, SECOND_EPSILON);
        }
    }
}