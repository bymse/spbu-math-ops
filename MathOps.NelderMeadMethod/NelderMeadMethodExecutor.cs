using System;
using System.Collections.Generic;
using MathOps.Utilities;

namespace MathOps.NelderMeadMethod
{
    public class NelderMeadMethodExecutor
    {
        private readonly Func<Vector2, decimal> function;
        private readonly Action<NelderMeadMethodIteration> observer;

        public NelderMeadMethodExecutor(Func<Vector2, decimal> function, Action<NelderMeadMethodIteration> observer)
        {
            this.function = function;
            this.observer = observer;
        }

        public TwoDimensionalApproximateResult Execute(
            IReadOnlyList<Vector2> startVertexes,
            NelderMeadMethodCoeffs coeffs,
            decimal boundaryCoeff)
        {
            
        }
    }
}