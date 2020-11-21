using MathOps.Utilities;

namespace MathOps.Methods.GoldenSectionSearch
{
    public class GoldenSectionSearchIteration
    {
        public int Iteration { get; set; }
        
        public decimal NextLeftArg { get; set; }
        public decimal NextRightArg { get; set; }
        
        public decimal LeftArgResult { get; set; }
        public decimal RightArgResult { get; set; }
        
        public Boundaries Boundaries { get; set; }
        public decimal LeftArg { get; set; }
        public decimal RightArg { get; set; }
        
        public Boundaries NextBoundaries { get; set; }
    }
}