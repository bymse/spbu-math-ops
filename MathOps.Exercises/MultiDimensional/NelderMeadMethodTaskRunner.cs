using System;
using System.Collections.Generic;
using MathOps.Methods.NelderMeadMethod;
using MathOps.Utilities;
using static MathOps.Exercises.ExerciseConsole;

namespace MathOps.Exercises.MultiDimensional
{
    public static class NelderMeadMethodTaskRunner
    {
        public static void Run()
        {
            ExecuteInLoopWithPoll(Action);
        }

        private static void Action()
        {
            var numb = MultiDimensionalFuncs.GetFuncNumber();
            switch (numb)
            {
                case 1:
                    RunInner(MultiDimensionalFuncs.FirstFunc);
                    break;
                case 2:
                    RunInner(MultiDimensionalFuncs.SecondFunc);
                    break;
            }
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