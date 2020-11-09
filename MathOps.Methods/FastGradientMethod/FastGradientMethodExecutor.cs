using System;
using MathOps.Utilities;

namespace MathOps.Methods.FastGradientMethod
{
    public class FastGradientMethodExecutor
    {
        private readonly Func<Vector2, decimal> function;
        private readonly TwoDimensionalGradient gradient;
        private readonly Func<Vector2, decimal> stepFunction;
        private readonly Action<FastGradientMethodIteration> observer;
        private readonly int precision;

        public FastGradientMethodExecutor(
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Func<Vector2, decimal> stepFunction,
            Action<FastGradientMethodIteration> observer,
            int precision = 5)
        {
            this.function = v => function(v).RoundTo(precision);
            this.observer = observer;
            this.precision = precision;
            this.stepFunction = v => stepFunction(v).RoundTo(precision);
            this.gradient = gradient;
        }

        public TwoDimensionalApproximateResult Execute(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount)
        {
            var iterationArg = startPoint;
            for (var iteration = 0;; iteration++)
            {
                var model = new FastGradientMethodIteration
                {
                    Iteration = iteration,
                };

                var result = HandleFirstPart(firstEpsilon, maxIterationsCount, model, iterationArg)
                             ?? HandleSecondPart(secondEpsilon, model, iterationArg);

                if (result != null)
                    return result;

                iterationArg = model.NextArg;
                observer(model);
            }
        }

        private TwoDimensionalApproximateResult HandleFirstPart(
            decimal firstEpsilon,
            int maxIterationsCount,
            FastGradientMethodIteration iteration,
            Vector2 iterationArg)
        {
            iteration.GradientIterationValue = gradient.Calculate(iterationArg, precision);

            if (iteration.GradientIterationValue.Norm() < firstEpsilon || iteration.Iteration >= maxIterationsCount)
            {
                return new TwoDimensionalApproximateResult
                {
                    Arg = iterationArg,
                    IterationsCount = iteration.Iteration,
                    Value = function(iterationArg)
                };
            }

            return null;
        }

        private TwoDimensionalApproximateResult HandleSecondPart(
            decimal secondEpsilon,
            FastGradientMethodIteration iteration,
            Vector2 iterationArg)
        {
            iteration.Step = stepFunction(iterationArg);
            iteration.NextArg = iterationArg - iteration.Step.Value * gradient.Calculate(iterationArg, precision);

            if ((iteration.NextArg - iterationArg).Norm() < secondEpsilon
                && Math.Abs(function(iteration.NextArg) - function(iterationArg)) < secondEpsilon)
            {
                return new TwoDimensionalApproximateResult
                {
                    Arg = iteration.NextArg,
                    Value = function(iteration.NextArg),
                    IterationsCount = iteration.Iteration + 1,
                };
            }

            return null;
        }
    }
}