using System;
using MathOps.Methods.Dichotomy;
using MathOps.Utilities;
using static MathOps.Exercises.ExerciseConsole;

namespace MathOps.Exercises.OneDimensional
{
    public static class DichotomyTaskRunner
    {
        public static void Run() => ExecuteInLoopWithPoll(RunInner);

        private static void RunInner()
        {
            var a = GetUserDecimal("a");
            var b = GetUserDecimal("b");
            var l = GetUserDecimal("l");
            var epsilon = GetUserDecimal("epsilon");

            var boundaries = RequestBoundaries();
            Console.WriteLine($"Execute dichotomy method for function {a}/e^x + {b}*x");

            var result = Execute(a, b, l, epsilon, boundaries, (info => { }));
            WriteResult(result);
        }

        public static OneDimensionalApproximateResult Execute(decimal a,
            decimal b,
            decimal precision,
            decimal epsilon,
            Boundaries boundaries,
            Action<DichotomyIterationInfo> observer)
        {
            var executor = new DichotomyMethodExecutor(OneDimensionalTaskFunc.BuildFunc(a, b),
                observer);

            return executor.ExecuteMethod(precision, epsilon, boundaries);
        }
    }
}