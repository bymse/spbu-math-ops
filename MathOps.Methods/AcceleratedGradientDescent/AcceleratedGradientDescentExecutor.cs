using System;
using MathOps.Methods.GoldenSectionSearch;
using MathOps.Utilities;

namespace MathOps.Methods.AcceleratedGradientDescent
{
    public class AcceleratedGradientDescentExecutor
    {
        private readonly Boundaries stepBoundaries;
        private readonly Func<Vector2, decimal> function;
        private readonly TwoDimensionalGradient gradient;
        private readonly Func<Vector2, Vector2, decimal> stepFunction;
        private readonly Action<AcceleratedGradientDescentIteration> observer;
        private readonly int precision;

        public AcceleratedGradientDescentExecutor(
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<AcceleratedGradientDescentIteration> observer,
            Func<Vector2, Vector2, decimal> stepFunction,
            int precision = 28) : this(function, gradient, observer, precision)
        {
            this.stepFunction = (a, b) => stepFunction(a, b);
        }

        public AcceleratedGradientDescentExecutor(
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<AcceleratedGradientDescentIteration> observer,
            Boundaries stepBoundaries,
            int precision = 28) : this(function, gradient, observer, precision)
        {
            this.stepBoundaries = stepBoundaries;
        }

        public AcceleratedGradientDescentExecutor(
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<AcceleratedGradientDescentIteration> observer,
            int precision = 28)
        {
            this.function = function;
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
            var hadResult = false;
            for (var iteration = 0;; iteration++)
            {
                var model = new AcceleratedGradientDescentIteration
                {
                    Iteration = iteration,
                    Arg = iterationArg,
                };

                var result = HandleFirstPart(firstEpsilon, maxIterationsCount, model, iterationArg);

                if (result != null)
                    return result;

                result = HandleSecondPart(secondEpsilon, model, iterationArg);
                
                observer(model);
                if (result != null && hadResult)
                    return result;

                hadResult = result != null;

                iterationArg = model.NextArg;
            }
        }

        private TwoDimensionalApproximateResult HandleFirstPart(
            decimal firstEpsilon,
            int maxIterationsCount,
            AcceleratedGradientDescentIteration iteration,
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
            AcceleratedGradientDescentIteration iteration,
            Vector2 iterationArg)
        {
            iteration.Step = stepFunction?.Invoke(iterationArg, iteration.GradientIterationValue)
                             ?? GetStepValue(iterationArg, iteration.GradientIterationValue);
            iteration.NextArg = iterationArg - iteration.Step.Value * iteration.GradientIterationValue;

            iteration.FuncValue = function(iterationArg);
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

        private decimal GetStepValue(Vector2 arg, Vector2 gradientVal)
        {
            var goldenSectionExecutor = new GoldenSectionSearchExecutor(
                t => function(arg - t * gradientVal),
                it => { });
            var v = goldenSectionExecutor.Execute(0.000000000001M, stepBoundaries).Arg;
            return v;
        }
    }
}