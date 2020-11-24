using System;
using MathOps.Methods.GoldenSectionSearch;
using MathOps.Utilities;

namespace MathOps.Methods.DavidonFletcherPowellMethod
{
    public class DavidonFletcherPowellMethodExecutor
    {
        private readonly Boundaries stepBoundaries;
        private readonly Func<Vector2, decimal> function;
        private readonly TwoDimensionalGradient gradient;
        private readonly Func<Vector2, Vector2, Vector2Matrix, decimal> stepFunction;
        private readonly Action<DavidonFletcherPowellMethodIteration> observer;
        private readonly int precision;

        public DavidonFletcherPowellMethodExecutor(
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<DavidonFletcherPowellMethodIteration> observer,
            Boundaries stepBoundaries,
            int precision = 28) : this(function, gradient, observer, precision)
        {
            this.stepBoundaries = stepBoundaries;
        }

        public DavidonFletcherPowellMethodExecutor(
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<DavidonFletcherPowellMethodIteration> observer,
            Func<Vector2, Vector2, Vector2Matrix, decimal> stepFunction,
            int precision = 5) : this(function, gradient, observer, precision)
        {
            this.stepFunction = (v, a, b) => stepFunction(v, a, b);
        }

        public DavidonFletcherPowellMethodExecutor(
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<DavidonFletcherPowellMethodIteration> observer,
            int precision = 28)
        {
            this.function = v => function(v);
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
            var hadResult = false;
            var prevIteration = new DavidonFletcherPowellMethodIteration
            {
                NextIterationArg = startPoint,
                IterationMatrix = new Vector2Matrix()
                {
                    FirstLine = new Vector2(1, 0),
                    SecondLine = new Vector2(0, 1),
                }
            };

            for (var iteration = 0;; iteration++)
            {
                var model = new DavidonFletcherPowellMethodIteration
                {
                    Iteration = iteration,
                    IterationArg = prevIteration.NextIterationArg,
                };

                var result = HandleFirstPart(model, firstEpsilon, maxIterationsCount, out var skipMatrix);

                if (result != null)
                    return result;

                result = HandleSecondPart(model, prevIteration, secondEpsilon, skipMatrix);

                observer(model);

                if (hadResult && result != null)
                    return result;

                hadResult = result != null;

                prevIteration = model;
            }
        }

        private TwoDimensionalApproximateResult HandleFirstPart(
            DavidonFletcherPowellMethodIteration iteration,
            decimal firstEpsilon,
            int maxIterationsCount,
            out bool skipMatrix)
        {
            skipMatrix = false;
            iteration.GradientIterationValue = gradient.Calculate(iteration.IterationArg, precision);
            if (iteration.GradientIterationValue.Norm() < firstEpsilon || iteration.Iteration >= maxIterationsCount)
            {
                return new TwoDimensionalApproximateResult
                {
                    Arg = iteration.IterationArg,
                    Value = function(iteration.IterationArg),
                    IterationsCount = iteration.Iteration
                };
            }

            if (iteration.Iteration == 0)
                skipMatrix = true;

            return null;
        }


        private TwoDimensionalApproximateResult HandleSecondPart(
            DavidonFletcherPowellMethodIteration iteration,
            DavidonFletcherPowellMethodIteration prevIteration,
            decimal secondEpsilon,
            bool skipMatrix)
        {
            if (!skipMatrix)
                CalculateNextIterationMatrix(iteration, prevIteration);
            else
            {
                iteration.IterationMatrix = prevIteration.IterationMatrix;
            }

            iteration.Step = stepFunction?.Invoke(
                                 iteration.IterationArg,
                                 iteration.GradientIterationValue,
                                 iteration.IterationMatrix)
                             
                             ?? GetStepValue(iteration.IterationArg,
                                 iteration.GradientIterationValue,
                                 iteration.IterationMatrix, secondEpsilon);

            iteration.NextIterationArg = iteration.IterationArg -
                                         iteration.IterationMatrix * iteration.Step *
                                         iteration.GradientIterationValue;

            iteration.NextIterationArg = new Vector2(iteration.NextIterationArg.First,
                iteration.NextIterationArg.Second);

            iteration.FuncVal = function(iteration.IterationArg);
            
            if ((iteration.NextIterationArg - iteration.IterationArg).Norm() < secondEpsilon
                && Math.Abs(function(iteration.NextIterationArg) - function(iteration.IterationArg)) < secondEpsilon)
            {
                return new TwoDimensionalApproximateResult
                {
                    Arg = iteration.NextIterationArg,
                    Value = function(iteration.NextIterationArg),
                    IterationsCount = iteration.Iteration + 1
                };
            }

            return null;
        }

        private void CalculateNextIterationMatrix(
            DavidonFletcherPowellMethodIteration iteration,
            DavidonFletcherPowellMethodIteration prevIteration)
        {
            var gradDiff =
                iteration.GradientIterationValue - prevIteration.GradientIterationValue;

            iteration.GradientsValuesDifference = gradDiff;

            var argsDiff = iteration.IterationArg - prevIteration.IterationArg;


            var leftPart = argsDiff.MultiplyByTransposed(argsDiff, precision) /
                           argsDiff.TransposeAndMultiply(gradDiff, precision);

            var rightPartNumerator = (prevIteration.IterationMatrix *
                                      gradDiff.MultiplyByTransposed(gradDiff, precision) *
                                      prevIteration.IterationMatrix);

            var m = prevIteration.IterationMatrix;

            var rightPartDenominator = new Vector2(
                    gradDiff.First * m.FirstLine.First + gradDiff.Second * m.SecondLine.First,
                    gradDiff.First * m.FirstLine.Second + gradDiff.Second * m.SecondLine.Second)
                .TransposeAndMultiply(gradDiff, precision);

            iteration.IterationAdditionalMatrix = leftPart - (rightPartNumerator / rightPartDenominator);

            iteration.IterationAdditionalMatrix.FirstLine
                = new Vector2(iteration.IterationAdditionalMatrix.FirstLine.First,
                    iteration.IterationAdditionalMatrix.FirstLine.Second);

            iteration.IterationAdditionalMatrix.SecondLine
                = new Vector2(iteration.IterationAdditionalMatrix.SecondLine.First,
                    iteration.IterationAdditionalMatrix.SecondLine.Second);

            iteration.IterationMatrix = prevIteration.IterationMatrix + iteration.IterationAdditionalMatrix;
        }

        private decimal GetStepValue(Vector2 arg, Vector2 gradientVal, Vector2Matrix iterationIterationMatrix,
            decimal epsilon2)
        {
            var goldenSectionExecutor = new GoldenSectionSearchExecutor(
                t => function(arg - t * (iterationIterationMatrix * gradientVal)),
                it => { });
            return goldenSectionExecutor.Execute(0.0001M, stepBoundaries).Arg;
        }
    }
}