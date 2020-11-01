using System;
using MathOps.Exercises.FirstExercise;

namespace MathOps.Exercises
{
    class Program
    {
        public static void Main()
        {
            Console.Write("Enter exercise number: ");
            var number = int.Parse(Console.ReadLine() ?? "1");

            switch (number)
            {
                case 1:
                    FirstExercise();
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private static void FirstExercise()
        {
            Console.WriteLine("1. Dichotomy");
            Console.WriteLine("2. Golden-section search");

            Console.Write("Enter task: ");
            var numb = int.Parse(Console.ReadLine() ?? "1");
            switch (numb)
            {
                case 1:
                    DichotomyTaskRunner.Run();
                    break;
                case 2:
                    GoldenSectionSearchTaskRunner.Run();
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}