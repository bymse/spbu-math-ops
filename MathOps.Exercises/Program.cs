using System;
using MathOps.Exercises.MultiDimensional;
using MathOps.Exercises.OneDimensional;

namespace MathOps.Exercises
{
    class Program
    {
        public static void Main()
        {
            ExerciseConsole.ExecuteInLoopWithPoll(Run);
        }

        private static void Run()
        {
            
            Console.WriteLine("1. Dichotomy");
            Console.WriteLine("2. Golden-section search");
            Console.WriteLine("3. Nelder-Mead method");
            Console.WriteLine("4. Fast gradient method");
            
            Console.Write("Enter task number (1-6): ");
            var number = ExerciseConsole.GetNumberInput();

            switch (number)
            {
                case 1:
                    DichotomyTaskRunner.Run();
                    break;
                case 2:
                    GoldenSectionSearchTaskRunner.Run();
                    break;
                case 3:
                    NelderMeadMethodTaskRunner.Run();
                    break;
                case 4:
                    new FastGradientMethodRunner().Run();
                    break;
                case 5:
                    new FletcherReevesMethodRunner().Run();
                    break;
                case 6:
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}