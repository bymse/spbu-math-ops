using System;
using System.Collections.Generic;
using MathOps.NelderMeadMethod;
using MathOps.Utilities;
using static MathOps.Exercises.ExerciseConsole;

namespace MathOps.Exercises.SecondExercise
{
    public static class NelderMeadMethodTaskRunner
    {
        public static void Run()
        {
            Action action = () =>
            {
                Console.WriteLine("1. 100 * (x_2 * (x_1)^2 )^2 + (1 - x_1)^2");
                Console.WriteLine("2. ((x_1)^2 + x_2 - 11)^2 + (x_1 + (x_2)^2 - 7)^2");

                Console.Write("Enter task: ");
                var numb = GetNumberInput();
                switch (numb)
                {
                    case 1:
                        RunInner(vector =>
                        {
                            var (first, second) = vector;
                            var leftPart = second - first * first;
                            leftPart *= leftPart;

                            var rightPart = 1 - first;
                            rightPart *= rightPart;

                            return 100 * leftPart + rightPart;
                        });
                        break;
                    case 2:
                        RunInner(vector =>
                        {
                            var (first, second) = vector;
                            var leftPart = first * first + second - 11;
                            var rightPart = first + second * second - 7;

                            return leftPart * leftPart + rightPart * rightPart;
                        });
                        break;
                }
            };
            ExecuteInLoopWithPoll(action);
        }

        private static void RunInner(Func<Vector2, decimal> function)
        {
            var vertexes = new List<Vector2>();
            for (var index = 0; index < 3; index++)
            {
                var first = GetUserDecimal($"{index + 1} vertex X");
                var second = GetUserDecimal($"{index + 1} vertex Y");
                vertexes.Add(new Vector2(first, second));
            }
            
            var coeffs = new NelderMeadMethodCoeffs
            {
                ReflectionCoeff = GetUserDecimal("reflection coefficient"),
                ContractionCoeff = GetUserDecimal("contraction coefficient"),
                ExpansionCoeff = GetUserDecimal("expansion coefficient"),
            };

            var boundaryCoeff = GetUserDecimal("boundary coefficient");
            var executor = new NelderMeadMethodExecutor(function, v => { });
            var result = executor.Execute(vertexes, coeffs, boundaryCoeff);
            WriteResult(result);
        }
    }
}