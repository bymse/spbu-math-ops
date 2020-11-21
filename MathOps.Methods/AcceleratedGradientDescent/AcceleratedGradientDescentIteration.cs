using MathOps.Utilities;

namespace MathOps.Methods.AcceleratedGradientDescent
{
    public class AcceleratedGradientDescentIteration
    {
        public int Iteration { get; set; }
        
        public Vector2 GradientIterationValue { get; set; }
        
        public decimal? Step { get; set; }
        
        public Vector2 NextArg { get; set; }
        public Vector2 Arg { get; set; }
        
        public decimal FuncValue { get; set; }
    }
}