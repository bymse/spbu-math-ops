using System;

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
    }
}