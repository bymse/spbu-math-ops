using System;
using FluentAssertions;
using FluentAssertions.Execution;
using MathOps.Utilities;
using NUnit.Framework;

namespace MathOps.Tests.Exercises
{
    [TestFixture]
    public abstract class OneDimensionalMethodsTestBase
    {
        private const int A = 5;
        private const int B = 10;
        private const decimal PRECISION = 0.15M;
        protected virtual Boundaries Boundaries => new Boundaries(-1, 1);
        
        protected abstract OneDimensionalApproximateResult Execute(decimal a, decimal b, decimal precision, Boundaries boundaries);
        
        [Test]
        public void Test()
        {
            var result = Execute(A, B, PRECISION, Boundaries);
            result.Arg.Should().BeApproximately(-0.693M, PRECISION);
        }
    }
}