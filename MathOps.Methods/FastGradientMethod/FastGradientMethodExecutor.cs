using System;
using MathOps.Methods.GoldenSectionSearch;
using MathOps.Utilities;

namespace MathOps.Methods.FastGradientMethod
{
    public class FastGradientMethodExecutor
    {
        private readonly Boundaries stepBoundaries;
        private readonly Func<Vector2, decimal> function;
        private readonly TwoDimensionalGradient gradient;
        private readonly Func<Vector2, Vector2, decimal> stepFunction;
        private readonly Action<FastGradientMethodIteration> observer;
        private readonly int precision;

        public FastGradientMethodExecutor(
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<FastGradientMethodIteration> observer,
            Func<Vector2, Vector2, decimal> stepFunction,
            int precision = 5) : this(function, gradient, observer, precision)
        {
            this.stepFunction = (a, b) => stepFunction(a, b).RoundTo(precision);
        }

        public FastGradientMethodExecutor(
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<FastGradientMethodIteration> observer,
            Boundaries stepBoundaries,
            int precision = 5) : this(function, gradient, observer, precision)
        {
            this.stepBoundaries = stepBoundaries;
        }

        public FastGradientMethodExecutor(
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<FastGradientMethodIteration> observer,
            int precision = 5)
        {
            this.function = v => function(v).RoundTo(precision);
            this.observer = observer;
            this.precision = precision;
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

                observer(model);
                if (result != null)
                    return result;

                iterationArg = model.NextArg;
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
            iteration.Step = stepFunction?.Invoke(iterationArg, iteration.GradientIterationValue)
                             ?? GetStepValue(iterationArg, iteration.GradientIterationValue, secondEpsilon);
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

        private decimal GetStepValue(Vector2 arg, Vector2 gradientVal, decimal epsilon2)
        {
            var goldenSectionExecutor = new GoldenSectionSearchExecutor(
                t => function(arg - t * gradientVal).RoundTo(precision),
                iteration => { });
            return goldenSectionExecutor.Execute(epsilon2 / 2, stepBoundaries).Arg;
        }
    }
}