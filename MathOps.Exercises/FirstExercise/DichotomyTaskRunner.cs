using System;
using MathOps.Methods.Dichotomy;
using MathOps.Utilities;
using static MathOps.Exercises.ExerciseConsole;

namespace MathOps.Exercises.FirstExercise
{
    public static class DichotomyTaskRunner
    {
        public static void Run() => ExecuteInLoopWithPoll(RunInner);
        
        private static void RunInner()
        {
            var a = GetUserDouble("a");
            var b = GetUserDecimal("b");
            var l = GetUserDecimal("l");
            var epsilon = GetUserDecimal("epsilon");

            var left = GetUserDecimal("left boundary");
            var right = GetUserDecimal("right boundary");
            var verbose = UseVerboseLogging();

            Console.WriteLine($"Execute dichotomy method for function {a}/e^x + {b}*x");

            decimal Function(decimal x)
            {
                return (decimal) (a / Math.Exp((double) x)) + b * x;
            }

            var executor = new DichotomyMethodExecutor(Function,
                verbose
                    ? VerboseObserver
                    : (Action<DichotomyIterationInfo>) (info => { }));

            var result = executor.ExecuteMethod(l, epsilon, new Boundaries(left, right));
            WriteResult(result);
        }
    }
}