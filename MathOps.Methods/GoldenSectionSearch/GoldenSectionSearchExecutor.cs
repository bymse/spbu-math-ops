using System;
using MathOps.Utilities;

namespace MathOps.Methods.GoldenSectionSearch
{
    public class GoldenSectionSearchExecutor
    {
        //approximate (3-sqrt(5))/2
        private const decimal MAGIC_COEFFICIENT = 1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072072041893911374847540880753868917521266338622235369317931800607667263544333890865959395829056383226613199282902678806752087668925017116962070322210432162695486262963136144381497587012203408058879544547492461856953648644492410443207713449470495658467885098743394422125448770664780915884607499887124007652170575179788341662562494075890697040002812104276217711177780531531714101170466659914669798731761356006708748071013179523689427521M;

        private readonly Func<decimal, decimal> function;
        private readonly Action<GoldenSectionSearchIteration> observer;

        public GoldenSectionSearchExecutor(Func<decimal, decimal> function,
            Action<GoldenSectionSearchIteration> observer)
        {
            this.function = function;
            this.observer = observer;
        }

        public OneDimensionalApproximateResult Execute(decimal precision, Boundaries boundaries)
        {
            var iterationModel = HandleFirstIteration(boundaries);
            for (var iteration = 1;; iteration++)
            {

                observer(iterationModel);
                if (HasResult(iterationModel, precision, out var result))
                {
                    return result;
                }

                iterationModel = HandleIteration(iteration, iterationModel);
            }
        }

        private bool HasResult(GoldenSectionSearchIteration iteration, decimal precision,
            out OneDimensionalApproximateResult result)
        {
            var (left, right) = iteration.NextBoundaries;
            if (Math.Abs(right - left) < precision)
            {
                result = new OneDimensionalApproximateResult
                {
                    Arg = (iteration.NextLeftArg + iteration.NextRightArg) / 2,
                    Boundaries = iteration.NextBoundaries,
                    IterationsCount = iteration.Iteration + 1,
                };

                result.Value = function(result.Arg);

                return true;
            }

            result = new OneDimensionalApproximateResult();
            return false;
        }

        private GoldenSectionSearchIteration HandleFirstIteration(Boundaries boundaries)
        {
            var left = boundaries.Left;
            var right = boundaries.Right;

            return HandleIteration(0, new GoldenSectionSearchIteration
            {
                NextLeftArg = left + (2 - MAGIC_COEFFICIENT) * (right - left),
                NextRightArg = right - (2 - MAGIC_COEFFICIENT) * (right - left),
                NextBoundaries = boundaries
            });
        }

        private GoldenSectionSearchIteration HandleIteration(int index, GoldenSectionSearchIteration previousIteration)
        {
            var model = new GoldenSectionSearchIteration
            {
                Iteration = index,
                LeftArgResult = function(previousIteration.NextLeftArg),
                RightArgResult = function(previousIteration.NextRightArg),
                
                Boundaries = previousIteration.NextBoundaries,
                LeftArg = previousIteration.NextLeftArg,
                RightArg = previousIteration.NextRightArg
            };

            var (left, right) = previousIteration.NextBoundaries;
            
            if (model.LeftArgResult < model.RightArgResult)
            {
                model.NextBoundaries = new Boundaries(left, previousIteration.NextRightArg);
                model.NextRightArg = previousIteration.NextLeftArg;
                model.NextLeftArg = left + (2 - MAGIC_COEFFICIENT) * (model.NextBoundaries.Right - model.NextBoundaries.Left);
            }
            else
            {
                model.NextBoundaries = new Boundaries(previousIteration.NextLeftArg, right);
                model.NextLeftArg = previousIteration.NextRightArg;
                model.NextRightArg = right - (2 - MAGIC_COEFFICIENT) * (model.NextBoundaries.Right - model.NextBoundaries.Left);
            }

            return model;
        }
    }
}