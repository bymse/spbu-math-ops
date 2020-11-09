using System;
using System.Collections.Generic;
using System.Linq;
using MathOps.Utilities;

namespace MathOps.Methods.NelderMeadMethod
{
    public class NelderMeadMethodExecutor
    {
        private readonly Func<Vector2, decimal> function;
        private readonly Action<NelderMeadMethodIteration> observer;

        public NelderMeadMethodExecutor(Func<Vector2, decimal> function, Action<NelderMeadMethodIteration> observer)
        {
            this.function = function;
            this.observer = observer;
        }

        public TwoDimensionalApproximateResult Execute(
            IReadOnlyList<Vector2> startVertexes,
            NelderMeadMethodCoeffs coeffs,
            decimal boundaryCoeff)
        {
            var modificator = new VertexesModificationsHandler(function, coeffs);
            var prevIteration = new NelderMeadMethodIteration
            {
                NextVertexList = startVertexes
            };
            for (var iteration = 0;; iteration++)
            {
                var tempIteration = HandleFirstIterationPart(iteration, prevIteration.NextVertexList);
                if (HasResult(tempIteration, prevIteration.NextVertexList, boundaryCoeff, out var result))
                {
                    return result;
                }

                prevIteration = HandleSecondIterationPart(tempIteration, prevIteration.NextVertexList, modificator);
            }
        }

        private bool HasResult(
            NelderMeadMethodIteration iteration,
            IReadOnlyList<Vector2> vertexes,
            decimal boundaryCoeff,
            out TwoDimensionalApproximateResult result)
        {
            Func<decimal, Vector2, decimal> sumFunc = (accum, vector) =>
            {
                var val = function(vector) - function(iteration.BalancePointVertex);
                return val * val + accum;
            };

            var boundaryCheckValue = vertexes.Aggregate(0, sumFunc) * 1 / vertexes.Count;

            if (Math.Sqrt((double) boundaryCheckValue) <= (double) boundaryCoeff)
            {
                result = new TwoDimensionalApproximateResult
                {
                    IterationsCount = iteration.Iteration,
                    Arg = iteration.MinValueVertex,
                };

                result.Value = function(result.Arg);
                return true;
            }

            result = null;
            return false;
        }

        private NelderMeadMethodIteration HandleFirstIterationPart(int iterationIndex, IReadOnlyList<Vector2> vertexes)
        {
            var iteration = new NelderMeadMethodIteration
            {
                Iteration = iterationIndex,
            };
            var ordered = vertexes.OrderByDescending(function).ToArray();
            iteration.MaxValueVertex = ordered.First();
            iteration.MinValueVertex = ordered.Last();
            iteration.SecondMaxValueVertex = ordered.Skip(1).First();

            iteration.BalancePointVertex = vertexes
                .Where(e => e != iteration.MaxValueVertex)
                .Aggregate((accum, cur) => accum + cur);
            // ReSharper disable once PossibleLossOfFraction
            iteration.BalancePointVertex *= 1M / (vertexes.Count - 1);

            return iteration;
        }

        private NelderMeadMethodIteration HandleSecondIterationPart(
            NelderMeadMethodIteration iteration,
            IReadOnlyList<Vector2> inputVertexes,
            VertexesModificationsHandler handler)
        {
            var vertexes = handler.ModifyVertexes(iteration, inputVertexes);
            iteration.NextVertexList = vertexes;
            observer(iteration);
            return iteration;
        }
    }
}