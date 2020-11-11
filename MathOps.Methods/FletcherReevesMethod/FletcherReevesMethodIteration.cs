using MathOps.Utilities;

namespace MathOps.Methods.FletcherReevesMethod
{
    public class FletcherReevesMethodIteration
    {
        public int Iteration { get; set; }
        
        public Vector2 GradientIterationValue { get; set; }
        
        public decimal? Step { get; set; }
        
        public decimal? BetaValue { get; set; }
        
        public Vector2 Direction { get; set; }  
        
        public Vector2 NextArg { get; set; }
    }
}