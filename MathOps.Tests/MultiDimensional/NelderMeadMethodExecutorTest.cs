using System;
using System.Collections.Generic;
using MathOps.NelderMeadMethod;
using MathOps.Utilities;

namespace MathOps.Tests.MultiDimensional
{
    public class NelderMeadMethodExecutorTest
        : ExecutorTestBase<TwoDimensionalApproximateResult, NelderMeadMethodIteration>
    {

        private const decimal REFLECTION_COEFF = 1;
        private const decimal CONTRACTION_COEFF = 0.5M;
        private const decimal EXPANSION_COEFF = 2M;
        private const decimal BOUNDARY_COEFF = 0.2M;
        
        private static decimal TestFunction(Vector2 args)
        {
            var (first, second) = args;
            return 4 * (first - 5) * (first - 5) + (second - 6) * (second - 6);
        }

        private static readonly IList<Vector2> StartVertexes = new[]
        {
            new Vector2(8, 9),
            new Vector2(10, 11),
            new Vector2(8, 11), 
        };

        protected override TwoDimensionalApproximateResult ExpectedResult => new TwoDimensionalApproximateResult
        {
            IterationsCount = 7,
            Value = 0.0625M,
            Arg = new Vector2(5, 6.25M)
        };

        protected override IReadOnlyList<NelderMeadMethodIteration> ExpectedIterationsList => new[]
        {
            new NelderMeadMethodIteration
            {
                Iteration = 0,
                MaxValueVertex = new Vector2(10, 11),
                MinValueVertex = new Vector2(8, 9),
                BalancePointVertex = new Vector2(8, 10),
                ReflectedVertex = new Vector2(6, 9),
                ExpandedVertex = new Vector2(4, 8),
                ContractedVertex = null,
                NextVertexList = new[]
                {
                    new Vector2(8, 9),
                    new Vector2(4, 8),
                    new Vector2(8, 11),
                }
            },
            new NelderMeadMethodIteration
            {
                Iteration = 1,
                MaxValueVertex = new Vector2(8, 11),
                MinValueVertex = new Vector2(4, 8),
                BalancePointVertex = new Vector2(6, 8.5M),
                ReflectedVertex = new Vector2(4, 6),
                ExpandedVertex = new Vector2(2, 3.5M),
                NextVertexList = new[]
                {
                    new Vector2(8, 9),
                    new Vector2(4, 8),
                    new Vector2(4, 6),
                }
            },
            new NelderMeadMethodIteration
            {
                Iteration = 2,
                MaxValueVertex = new Vector2(8, 9),
                MinValueVertex = new Vector2(4, 6),
                BalancePointVertex = new Vector2(4, 7),
                ReflectedVertex = new Vector2(0, 5),
                NextVertexList = new[]
                {
                    new Vector2(6, 7.5M),
                    new Vector2(4, 7),
                    new Vector2(4, 6),
                }
            },
            new NelderMeadMethodIteration
            {
                Iteration = 3,
                MaxValueVertex = new Vector2(6, 7.5M),
                MinValueVertex = new Vector2(4, 6),
                BalancePointVertex = new Vector2(4, 6.5M),
                ReflectedVertex = new Vector2(2, 5.5M),
                NextVertexList = new[]
                {
                    new Vector2(5, 6.75M),
                    new Vector2(4, 6.5M),
                    new Vector2(4, 6),
                }
            },
            new NelderMeadMethodIteration
            {
                Iteration = 4,
                MaxValueVertex = new Vector2(4, 6.5M),
                MinValueVertex = new Vector2(5, 6.75M),
                BalancePointVertex = new Vector2(4.5M, 6.375M),
                ReflectedVertex = new Vector2(5, 6.25M),
                ExpandedVertex = new Vector2(5.5M, 6.125M),
                NextVertexList = new[]
                {
                    new Vector2(5, 6.75M),
                    new Vector2(5, 6.25M),
                    new Vector2(4, 6),
                }
            },
            new NelderMeadMethodIteration
            {
                Iteration = 5,
                MaxValueVertex = new Vector2(4, 6),
                MinValueVertex = new Vector2(5, 6.25M),
                BalancePointVertex = new Vector2(5, 6.5M),
                ReflectedVertex = new Vector2(6, 7),
                NextVertexList = new[]
                {
                    new Vector2(5, 6.5M),
                    new Vector2(5, 6.25M),
                    new Vector2(4.5M, 6.125M),
                }
            },
            new NelderMeadMethodIteration
            {
                Iteration = 6,
                MinValueVertex = new Vector2(5, 6.25M),
                MaxValueVertex = new Vector2(4.5M, 6.125M),
                BalancePointVertex = new Vector2(5, 6.375M),
                ReflectedVertex = new Vector2(5.5M, 6.625M),
                NextVertexList = new[]
                {
                    new Vector2(5, 6.375M),
                    new Vector2(5, 6.25M),
                    new Vector2(4.75M, 6.18M),
                }
            },
        };

        protected override TwoDimensionalApproximateResult ExecuteWithObserver(
            Action<NelderMeadMethodIteration> observer)
        {
            return new NelderMeadMethodExecutor(TestFunction, observer)
                .Execute(StartVertexes,
                    REFLECTION_COEFF,
                    CONTRACTION_COEFF,
                    EXPANSION_COEFF,
                    BOUNDARY_COEFF);
        }
    }
}