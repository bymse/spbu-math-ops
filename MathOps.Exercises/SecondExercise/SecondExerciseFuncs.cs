using System;
using MathOps.Utilities;

namespace MathOps.Exercises.SecondExercise
{
    public static class SecondExerciseFuncs
    {
        public static int GetFuncNumber()
        {
            Console.WriteLine("1. 100 * (x_2 - (x_1)^2 )^2 + (1 - x_1)^2");
            Console.WriteLine("2. ((x_1)^2 + x_2 - 11)^2 + (x_1 + (x_2)^2 - 7)^2");
            Console.Write("Enter task: ");
            return ExerciseConsole.GetNumberInput();
        }

        public static Func<Vector2, decimal> FirstFunc => vector =>
        {
            var (first, second) = vector;
            return 100 * (second - first.Square()).Square() + (1 - first).Square();
        };

        public static TwoDimensionalGradient FirstFuncGradient => new TwoDimensionalGradient
        {
            First = vector2 =>
            {
                var (x, y) = vector2;
                return 2 * (-1 + x + 200 * x.Square() * x - 200 * x * y);
            },
            Second = vector2 => 200 * (vector2.Second - vector2.First.Square())
        };

        public static Func<Vector2, decimal> SecondFunc => vector =>
        {
            var (first, second) = vector;
            var leftPart = first.Square() + second - 11;
            var rightPart = first + second.Square() - 7;

            return leftPart.Square() + rightPart.Square();
        };

        public static TwoDimensionalGradient SecondFuncGradient => new TwoDimensionalGradient
        {
            First = vector2 =>
            {
                var (first, second) = vector2;
                return 2 * (2 * first * (first.Square() + second - 11) + first + second.Square() - 7);
            },
            Second = vector2 =>
            {
                var (first, second) = vector2;
                return 2 * (first.Square() + 2 * second * (first + second.Square() - 7) + second - 11);
            }
        };
    }
}