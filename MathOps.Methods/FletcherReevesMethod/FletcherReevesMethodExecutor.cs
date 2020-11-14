using System;
using MathOps.Methods.FastGradientMethod;
using MathOps.Utilities;

namespace MathOps.Methods.FletcherReevesMethod
{
    public class FletcherReevesMethodExecutor
    {
        private readonly Func<Vector2, decimal> function;
        private readonly TwoDimensionalGradient gradient;
        private readonly Func<Vector2, Vector2, decimal> stepFunction;
        private readonly Func<Vector2, decimal> firstStepFunction;
        private readonly Action<FletcherReevesMethodIteration> observer;
        private readonly int precision;

        public FletcherReevesMethodExecutor(Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Func<Vector2, Vector2, decimal> stepFunction,
            Action<FletcherReevesMethodIteration> observer,
            Func<Vector2, decimal> firstStepFunction,
            int precision = 5)
        {
            this.function = v => function(v).RoundTo(precision);
            this.observer = observer;
            this.firstStepFunction = (v) => firstStepFunction(v).RoundTo(precision);
            this.precision = precision;
            this.stepFunction = (v, e) => stepFunction(v, e).RoundTo(precision);
            this.gradient = gradient;
        }

        public TwoDimensionalApproximateResult Execute(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount)
        {
            var prevIteration = new FletcherReevesMethodIteration
            {
                NextArg = startPoint
            };
            var hasResultInPrevIteration = false;
            for (var iteration = 0;; iteration++)
            {
                var model = new FletcherReevesMethodIteration
                {
                    Iteration = iteration,
                };

                var result = HandleFirstPart(firstEpsilon, maxIterationsCount, model, prevIteration.NextArg);
                if (result != null)
                    return result;

                result = HandleSecondPart(secondEpsilon, model, prevIteration);

                if (hasResultInPrevIteration && result != null)
                    return result;

                hasResultInPrevIteration = result != null;

                prevIteration = model;
                observer(model);
            }
        }

        private TwoDimensionalApproximateResult HandleFirstPart(decimal firstEpsilon,
            int maxIterationsCount,
            FletcherReevesMethodIteration iteration,
            Vector2 iterationArg)
        {
            iteration.GradientIterationValue = gradient.Calculate(iterationArg, precision);

            if (iteration.GradientIterationValue.Norm() < firstEpsilon || iteration.Iteration >= maxIterationsCount)
            {
                return new TwoDimensionalApproximateResult
                {
                    Arg = iterationArg,
                    Value = function(iterationArg),
                    IterationsCount = iteration.Iteration
                };
            }

            return null;
        }

        private TwoDimensionalApproximateResult HandleSecondPart(decimal secondEpsilon,
            FletcherReevesMethodIteration iteration,
            FletcherReevesMethodIteration prevIteration)
        {
            if (iteration.Iteration == 0)
            {
                iteration.Direction = -iteration.GradientIterationValue;

                iteration.Step = firstStepFunction(prevIteration.NextArg);
            }
            else
            {
                iteration.BetaValue = (iteration.GradientIterationValue.Norm().Square() /
                                       prevIteration.GradientIterationValue.Norm().Square()).RoundTo(precision);

                iteration.Direction = -iteration.GradientIterationValue
                                      + iteration.BetaValue.Value * prevIteration.Direction;

                iteration.Step = stepFunction(prevIteration.NextArg, iteration.Direction);
            }

            iteration.NextArg = prevIteration.NextArg + iteration.Step.Value * iteration.Direction;

            if ((iteration.NextArg - prevIteration.NextArg).Norm() < secondEpsilon
                && Math.Abs(function(iteration.NextArg) - function(prevIteration.NextArg)) < secondEpsilon)
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