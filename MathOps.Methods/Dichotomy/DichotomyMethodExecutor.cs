using System;
using MathOps.Utilities;

namespace MathOps.Methods.Dichotomy
{
    public class DichotomyMethodExecutor
    {
        private readonly Func<decimal, decimal> function;
        private readonly Action<DichotomyIterationInfo> observer;

        public DichotomyMethodExecutor(Func<decimal, decimal> function, Action<DichotomyIterationInfo> observer)
        {
            this.function = function;
            this.observer = observer;
        }

        public OneDimensionalApproximateResult ExecuteMethod(decimal precision, decimal epsilon, Boundaries boundaries)
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
            
            observer(iterationInfo);
            return iterationInfo;
        }

        private bool HasResult(DichotomyIterationInfo iterationInfo, decimal precision, out OneDimensionalApproximateResult result)
        {
            var (left, right) = iterationInfo.NextIterationBoundaries;

            var iterationPrecision = Math.Abs(left - right);
            if (iterationPrecision <= precision)
            {
                result = new OneDimensionalApproximateResult
                {
                    IterationsCount = iterationInfo.Iteration + 1,
                    Boundaries = iterationInfo.NextIterationBoundaries,
                    Arg = (left + right) / 2,
                };

                result.Value = function(result.Arg);
                return true;
            }
            
            result = new OneDimensionalApproximateResult();
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