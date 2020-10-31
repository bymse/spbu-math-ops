using MathOps.Utilities;

namespace Dichotomy
{
    public struct DichotomyIterationInfo
    {
        public int Iteration { get; set; }
        
        public decimal LeftArg { get; set; }
        public decimal RightArg { get; set; }
        
        public decimal LeftArgResult { get; set; }
        public decimal RightArgResult { get; set; }
        
        public Boundaries NextIterationBoundaries { get; set; }
    }
}