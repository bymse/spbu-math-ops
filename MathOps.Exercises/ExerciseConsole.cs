using System;
using MathOps.Utilities;
using Newtonsoft.Json;

namespace MathOps.Exercises
{
    public static class ExerciseConsole
    {
        public static void ExecuteInLoopWithPoll(Action action)
        {
            bool @continue;
            do
            {
                action();
                Console.WriteLine("Enter \"q\" for exit to the main menu");
                @continue = !Console.ReadLine()?.Equals("q", StringComparison.OrdinalIgnoreCase) ?? true;
            } while (@continue);
        }

        public static decimal GetUserDecimal(string name)
        {
            Console.Write($"Enter {name}: ");
            return decimal.Parse(Console.ReadLine() ?? "1");
        }

        public static double GetUserDouble(string name)
        {
            Console.Write($"Enter {name}: ");
            return double.Parse(Console.ReadLine() ?? "1");
        }

        public static bool UseVerboseLogging()
        {
            Console.Write("Use verbose logging (y/n): ");
            var answer = Console.ReadLine() ?? "n";
            return answer.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        public static void WriteResult(OneDimensionalApproximateResult result)
        {
            Console.WriteLine($"Iterations count: {result.IterationsCount}");
            Console.WriteLine($"Approximate x: {result.Arg}");
            Console.WriteLine($"Function value: {result.Value}");
            Console.WriteLine($"Approximate left boundary: {result.Boundaries.Right}");
            Console.WriteLine($"Approximate right boundary: {result.Boundaries.Right}");
        }

        public static void VerboseObserver<T>(T iterationInfo)
        {
            Console.WriteLine(JsonConvert.SerializeObject(iterationInfo, Formatting.Indented));
        }
    }
}