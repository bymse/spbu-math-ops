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

            switch (numb)
            {
                case 1:
                    WriteResult(RunForFirst(vertexes, coeffs, boundaryCoeff, iteration => {}));
                    break;
                case 2:
                    WriteResult(RunForSecond(vertexes, coeffs, boundaryCoeff, iteration => {}));
                    break;
            }
        }

        public static TwoDimensionalApproximateResult RunForFirst(
            IReadOnlyList<Vector2> startVertexes,
            NelderMeadMethodCoeffs coeffs,
            decimal boundaryCoeff, Action<NelderMeadMethodIteration> observer)
        {
            var executor = new NelderMeadMethodExecutor(MultiDimensionalFuncs.FirstFunc, observer);
            return executor.Execute(startVertexes, coeffs, boundaryCoeff);
        }

        public static TwoDimensionalApproximateResult RunForSecond(
            IReadOnlyList<Vector2> startVertexes,
            NelderMeadMethodCoeffs coeffs,
            decimal boundaryCoeff, Action<NelderMeadMethodIteration> observer)
        {
            var executor = new NelderMeadMethodExecutor(MultiDimensionalFuncs.SecondFunc, observer);
            return executor.Execute(startVertexes, coeffs, boundaryCoeff);
        }

    }
}