using System;
using MathOps.GoldenSectionSearch;
using MathOps.Utilities;
using static MathOps.Exercises.ExerciseConsole;

namespace MathOps.Exercises.FirstExercise
{
    public static class GoldenSectionSearchTaskRunner
    {
        public static void Run() => ExecuteInLoopWithPoll(RunInner);
        
        private static void RunInner()
        {
            var a = GetUserDouble("a");
            var b = GetUserDecimal("b");
            var l = GetUserDecimal("l");

            var left = GetUserDecimal("left boundary");
            var right = GetUserDecimal("right boundary");
            var verbose = UseVerboseLogging();

            Console.WriteLine($"Execute golden-section search for function {a}/e^x + {b}*x");

            decimal Function(decimal x)
            {
                return (decimal) (a / Math.Exp((double) x)) + b * x;
            }

            var executor = new GoldenSectionSearchExecutor(Function,
                verbose
                    ? VerboseObserver
                    : (Action<GoldenSectionSearchIteration>) (info => { }));

            var result = executor.Execute(l, new Boundaries(left, right));
            WriteResult(result);
        }
    }
}