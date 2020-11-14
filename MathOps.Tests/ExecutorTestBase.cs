using System;
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MathOps.Tests
{
    [TestFixture]
    public abstract class ExecutorTestBase<TResult, TIteration>
    {
        protected abstract TResult ExpectedResult { get; }
        protected abstract IReadOnlyList<TIteration> ExpectedIterationsList { get; }

        protected abstract TResult ExecuteWithObserver(Action<TIteration> observer);

        [TestCase(TestName = "Result test based on example from book")]
        public void TestResult()
        {
            var result = ExecuteWithObserver(v => { });
            result.Should().BeEquivalentTo(ExpectedResult);
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        [TestCase(TestName = "Iterations test based on example from book")]
        public void TestIterations()
        {
            var count = 0;
            var tester = BuildIterationsTester(ExpectedIterationsList);
            ExecuteWithObserver((v) =>
            {
                tester(v);
                count++;
            });

            count.Should().Be(ExpectedIterationsList.Count);
        }

        private static Action<TIteration> BuildIterationsTester(
            IReadOnlyList<TIteration> expectedValues)
        {
            var index = 0;
            return actualValue =>
            {
                Assert.Less(index, expectedValues.Count);
                actualValue.Should().BeEquivalentTo(expectedValues[index]);
                index++;
            };
        }
    }
}