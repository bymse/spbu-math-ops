using MathOps.Exercises.MultiDimensional;
using MathOps.Methods.FletcherReevesMethod;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.Exercises
{
    [TestFixture]
    public class FletcherReevesMethodRunnerTest : MultiDimensionalMethodsTestBase<FletcherReevesMethodRunner, FletcherReevesMethodIteration>
    {
        protected override Vector2 SecondStartPoint => new Vector2(4, 4);
        protected override Boundaries FirstBoundaries => new Boundaries(-1 ,1);
    }
}