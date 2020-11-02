using System;
using MathOps.Exercises.FirstExercise;
using MathOps.Exercises.SecondExercise;

namespace MathOps.Exercises
{
    class Program
    {
        public static void Main()
        {
            Console.Write("Enter exercise number: ");
            var number = ExerciseConsole.GetNumberInput();

            switch (number)
            {
                case 1:
                    FirstExercise();
                    break;
                case 2:
                    SecondExercise();
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
            var numb = ExerciseConsole.GetNumberInput();
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
        
        private static void SecondExercise()
        {
            Console.WriteLine("1. Nelder-Mead method");

            Console.Write("Enter task: ");
            var numb = ExerciseConsole.GetNumberInput();
            switch (numb)
            {
                case 1:
                    NelderMeadMethodTaskRunner.Run();
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}