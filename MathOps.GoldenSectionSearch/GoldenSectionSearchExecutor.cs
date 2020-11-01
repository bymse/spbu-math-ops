using System;
using MathOps.Utilities;

namespace MathOps.GoldenSectionSearch
{
    public class GoldenSectionSearchExecutor
    {
        //approximate (3-sqrt(5))/2
        private const decimal MAGIC_COEFFICIENT = 0.382M;

        private readonly Func<decimal, decimal> function;
        private readonly Action<GoldenSectionSearchIteration> observer;

        public GoldenSectionSearchExecutor(Func<decimal, decimal> function,
            Action<GoldenSectionSearchIteration> observer)
        {
            this.function = function;
            this.observer = observer;
        }

        public ApproximateResult Execute(decimal precision, Boundaries boundaries)
        {
            var iterationModel = HandleFirstIteration(boundaries);
            for (var iteration = 1;; iteration++)
            {

                if (HasResult(iterationModel, precision, out var result))
                {
                    return result;
                }

                iterationModel = HandleIteration(iteration, iterationModel);
            }
        }

        private bool HasResult(GoldenSectionSearchIteration iteration, decimal precision, out ApproximateResult result)
        {
            var (left, right) = iteration.NextBoundaries;
            var boundariesDiff = Math.Abs(left - right);
            if (boundariesDiff <= precision)
            {
                result = new ApproximateResult
                {
                    Arg = (left + right) / 2,
                    Boundaries = iteration.NextBoundaries,
                    IterationsCount = iteration.Iteration + 1,
                };

                result.Value = function(result.Arg);

                return true;
            }

            result = new ApproximateResult();
            return false;
        }

        private GoldenSectionSearchIteration HandleFirstIteration(Boundaries boundaries)
        {
            var (left, right) = boundaries;
            var leftArg = left + MAGIC_COEFFICIENT * (right - left);
            var rightArg = left + right - leftArg;

            return HandleIteration(0, new GoldenSectionSearchIteration
            {
                NextLeftArg = leftArg,
                NextRightArg = rightArg,
                NextBoundaries = boundaries
            });
        }

        private GoldenSectionSearchIteration HandleIteration(int index, GoldenSectionSearchIteration previousIteration)
        {
            var model = new GoldenSectionSearchIteration
            {
                Iteration = index,
            };

            var (left, right) = previousIteration.NextBoundaries;
            var leftArgResult = function(previousIteration.NextLeftArg);
            var rightArgResult = function(previousIteration.NextRightArg);

            if (leftArgResult <= rightArgResult)
            {
                model.NextBoundaries = new Boundaries(left, previousIteration.NextRightArg);
                model.NextLeftArg = model.NextBoundaries.Left + model.NextBoundaries.Right - previousIteration.NextLeftArg;
                model.NextRightArg = previousIteration.NextLeftArg;
            }
            else
            {
                model.NextBoundaries = new Boundaries(previousIteration.NextLeftArg, right);
                model.NextLeftArg = previousIteration.NextRightArg;
                model.NextRightArg = model.NextBoundaries.Left + model.NextBoundaries.Right - previousIteration.NextRightArg;
            }

            model.LeftArgResult = leftArgResult;
            model.RightArgResult = rightArgResult;

            observer(model);
            return model;
        }
    }
}