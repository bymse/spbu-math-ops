using System;
using MathOps.Utilities;
using static MathOps.Exercises.ExerciseConsole;
using static MathOps.Exercises.MultiDimensional.MultiDimensionalFuncs;


namespace MathOps.Exercises.MultiDimensional
{
    public abstract class MultiDimensionalMethodsBase<T>
    {
        public Action<T> Observer { get; set; } = obj => { }; 
        
        public void Run()
        {
            ExecuteInLoopWithPoll(() => RunInner(GetFuncNumber()));
        }

        private void RunInner(int numb)
        {
            var first = GetUserDecimal("start point x_1");
            var second = GetUserDecimal("start point x_2");
            var startPoint = new Vector2(first, second);

            var firstEpsilon = GetUserDecimal("first epsilon");
            var secondEpsilon = GetUserDecimal("second epsilon");
            var maxIterationsCount = (int) GetUserDecimal("max iterations count");


            var boundaries = RequestBoundaries();
            var result = numb == 1
                ? RunForFirst(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount, boundaries)
                : RunForSecond(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount, boundaries);

            WriteResult(result);
        }

        public TwoDimensionalApproximateResult RunForFirst(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount,
            Boundaries boundaries) =>
            ExecuteMethod(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount, boundaries, FirstFunc,
                FirstFuncGradient, Observer);

        public TwoDimensionalApproximateResult RunForSecond(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount,
            Boundaries boundaries) =>
            ExecuteMethod(startPoint, firstEpsilon, secondEpsilon, maxIterationsCount, boundaries, SecondFunc,
                SecondFuncGradient, Observer);

        protected abstract TwoDimensionalApproximateResult ExecuteMethod(
            Vector2 startPoint,
            decimal firstEpsilon,
            decimal secondEpsilon,
            int maxIterationsCount,
            Boundaries boundaries,
            Func<Vector2, decimal> function,
            TwoDimensionalGradient gradient,
            Action<T> observer);
    }
}