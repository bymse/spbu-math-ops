using System;
using MathOps.Utilities;

namespace Dichotomy
{
    public class DichotomyMethodExecutor
    {
        private readonly Func<decimal, decimal> function;
        private readonly Action<DichotomyIterationInfo> iterationObserver;

        public DichotomyMethodExecutor(Func<decimal, decimal> function,
            Action<DichotomyIterationInfo> iterationObserver = null)
        {
            this.function = function;
            this.iterationObserver = iterationObserver ?? (v => { });
        }

        public DichotomyResult Execute(decimal precision, decimal epsilon, Boundaries boundaries)
        {
            var iterationBoundaries = boundaries;
            for (var iteration = 0;; iteration++)
            {
                var iterationInfo = HandleIteration(iteration, iterationBoundaries, epsilon);

                if (HasResult(iterationInfo, precision, out var result))
                {
                    return result;
                }
                
                iterationBoundaries = iterationInfo.NextIterationBoundaries;
            }
        }

        private DichotomyIterationInfo HandleIteration(int iteration, Boundaries boundaries, decimal epsilon)
        {
            var iterationInfo = new DichotomyIterationInfo
            {
                Iteration = iteration,
                LeftArg = (boundaries.Left + boundaries.Right - epsilon) / 2,
                RightArg = (boundaries.Left + boundaries.Right + epsilon) / 2,
            };

            iterationInfo.LeftArgResult = function(iterationInfo.LeftArg);
            iterationInfo.RightArgResult = function(iterationInfo.RightArg);
            
            iterationInfo.NextIterationBoundaries = BuildNextIterationBoundaries(boundaries, iterationInfo);
            
            iterationObserver(iterationInfo);
            return iterationInfo;
        }

        private bool HasResult(DichotomyIterationInfo iterationInfo, decimal precision, out DichotomyResult result)
        {
            var (left, right) = iterationInfo.NextIterationBoundaries;

            var iterationPrecision = Math.Abs(left - right);
            if (iterationPrecision <= precision)
            {
                result = new DichotomyResult
                {
                    IterationsCount = iterationInfo.Iteration + 1,
                    ApproximateResultBoundaries = iterationInfo.NextIterationBoundaries,
                    ApproximateResultArg = (left + right) / 2,
                };

                result.ApproximateResult = function(result.ApproximateResultArg);
                return true;
            }
            
            result = new DichotomyResult();
            return false;
        }

        private static Boundaries BuildNextIterationBoundaries(Boundaries boundaries, DichotomyIterationInfo iterationInfo)
        {
            var nextIterationBoundaries = new Boundaries();
            if (iterationInfo.LeftArgResult <= iterationInfo.RightArgResult)
            {
                nextIterationBoundaries.Left = boundaries.Left;
                nextIterationBoundaries.Right = iterationInfo.RightArg;
            }
            else
            {
                nextIterationBoundaries.Left = iterationInfo.LeftArg;
                nextIterationBoundaries.Right = boundaries.Right;
            }

            return nextIterationBoundaries;
        }
    }
}