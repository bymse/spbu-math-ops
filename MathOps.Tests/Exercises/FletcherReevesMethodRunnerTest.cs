using MathOps.Exercises.MultiDimensional;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.Exercises
{
    [TestFixture]
    public class FletcherReevesMethodRunnerTest : MultiDimensionalMethodsTestBase<FletcherReevesMethodRunner>
    {
        protected override Vector2 SecondStartPoint => new Vector2(4, 4);
        protected override Boundaries FirstBoundaries => new Boundaries(0 ,1);
    }
}