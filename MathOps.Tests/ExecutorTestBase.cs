using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace MathOps.Tests
{
    [TestFixture]
    public abstract class ExecutorTestBase<TResult, TIteration>
    {
        protected abstract TResult ExpectedResult { get; }
        protected abstract IReadOnlyList<TIteration> ExpectedIterationsList { get; }

        protected abstract TResult ExecuteWithObserver(Action<TIteration> observer);

        [TestCase(TestName = "Iterations test based on example from book")]
        public void TestResult() => ExecuteWithObserver(v => {}).Should().Be(ExpectedResult);

        [TestCase(TestName = "Iterations test based on example from book")]
        public void TestIterations() => ExecuteWithObserver(BuildIterationsTester(ExpectedIterationsList));

        private static Action<TIteration> BuildIterationsTester(
            IReadOnlyList<TIteration> expectedValues)
        {
            var index = 0;
            return actualValue =>
            {
                Assert.Less(index, expectedValues.Count);
                actualValue.Should().Be(expectedValues[index]);
                index++;
            };
        }
    }
}