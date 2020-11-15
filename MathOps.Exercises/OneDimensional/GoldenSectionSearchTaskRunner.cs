using System;
using MathOps.Methods.GoldenSectionSearch;
using MathOps.Utilities;
using static MathOps.Exercises.ExerciseConsole;

namespace MathOps.Exercises.OneDimensional
{
    public static class GoldenSectionSearchTaskRunner
    {
        public static void Run() => ExecuteInLoopWithPoll(RunInner);

        private static void RunInner()
        {
            var a = GetUserDecimal("a");
            var b = GetUserDecimal("b");
            var l = GetUserDecimal("l");

            var boundaries = RequestBoundaries();

            var result = Execute(a, b, l, boundaries);
            WriteResult(result);
        }

        public static OneDimensionalApproximateResult Execute(decimal a,
            decimal b,
            decimal precision,
            Boundaries boundaries)
        {
            var executor = new GoldenSectionSearchExecutor(OneDimensionalTaskFunc.BuildFunc(a, b),
                (info => { }));

            return executor.Execute(precision, boundaries);
        }
    }
}