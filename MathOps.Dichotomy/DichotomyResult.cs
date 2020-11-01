using MathOps.Utilities;

namespace MathOps.Dichotomy
{
    public struct DichotomyResult
    {
        public decimal ApproximateResult { get; set; }
        public decimal ApproximateResultArg { get; set; }
        public Boundaries ApproximateResultBoundaries { get; set; }
        public int IterationsCount { get; set; }
    }
}