using MathOps.Utilities;

namespace MathOps.Methods.FastGradientMethod
{
    public class FastGradientMethodIteration
    {
        public int Iteration { get; set; }
        
        public Vector2 GradientIterationValue { get; set; }
        
        public decimal? Step { get; set; }
        
        public Vector2 NextArg { get; set; }
    }
}