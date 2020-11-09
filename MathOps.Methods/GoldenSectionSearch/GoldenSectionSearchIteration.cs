using MathOps.Utilities;

namespace MathOps.Methods.GoldenSectionSearch
{
    public struct GoldenSectionSearchIteration
    {
        public int Iteration { get; set; }
        
        public decimal NextLeftArg { get; set; }
        public decimal NextRightArg { get; set; }
        
        public decimal LeftArgResult { get; set; }
        public decimal RightArgResult { get; set; }
        
        
        public Boundaries NextBoundaries { get; set; }
    }
}