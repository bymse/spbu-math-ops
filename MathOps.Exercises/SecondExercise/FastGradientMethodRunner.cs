using System;
using MathOps.Methods.FastGradientMethod;
using MathOps.Methods.NelderMeadMethod;
using MathOps.Utilities;
using static MathOps.Exercises.ExerciseConsole;
using static MathOps.Exercises.SecondExercise.SecondExerciseFuncs;

namespace MathOps.Exercises.SecondExercise
{
    public static class FastGradientMethodRunner
    {
        public static void Run()
        {
            ExecuteInLoopWithPoll(() => RunInner(GetFuncNumber()));
        }

        private static void RunInner(int numb)
        {
            var first = GetUserDecimal("start point x_1");
            var second = GetUserDecimal("start point x_2");
            var startPoint = new Vector2(first, second);

            var firstEpsilon = GetUserDecimal("first epsilon");
            var secondEpsilon = GetUserDecimal("second epsilon");
            var maxIterationsCount = (int) GetUserDecimal("max iterations count");


            TwoDimensionalApproximateResult result;
            if (numb == 1)
            {
                var boundaries = RequestBoundaries();
                result = RunForFirst(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount, boundaries);
            }
            else
            {
                var boundaries = RequestBoundaries();
                result = RunForSecond(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount, boundaries);
            }

            WriteResult(result);
        }

        public static TwoDimensionalApproximateResult RunForFirst(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount,
            Boundaries boundaries)
        {
            return new FastGradientMethodExecutor(
                    FirstFuncParams.Function,
                    FirstFuncParams.Gradient,
                    iteration => { },
                    boundaries)
                .Execute(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount);
        }

        public static TwoDimensionalApproximateResult RunForSecond(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount,
            Boundaries boundaries)
        {
            return new FastGradientMethodExecutor(
                    SecondFuncParams.Function,
                    SecondFuncParams.Gradient,
                    iteration => { },
                    boundaries)
                .Execute(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount);
        }

        private static readonly FastGradientMethodParams FirstFuncParams = new FastGradientMethodParams
        {
            Function = FirstFunc,
            Gradient = FirstFuncGradient,
        };

        private static readonly FastGradientMethodParams SecondFuncParams = new FastGradientMethodParams
        {
            Function = SecondFunc,
            Gradient = SecondFuncGradient,
        };
    }
}