using MathOps.Utilities;

namespace MathOps.Methods.DavidonFletcherPowellMethod
{
    public class DavidonFletcherPowellMethodIteration
    {
        public int Iteration { get; set; }
        
        public Vector2 GradientIterationValue { get; set; }
        public Vector2? GradientsValuesDifference { get; set; }
        
        public Vector2Matrix IterationAdditionalMatrix { get; set; }
        public Vector2Matrix IterationMatrix { get; set; }
        public decimal Step { get; set; }
        
        public Vector2 NextIterationArg { get; set; }
        public Vector2 IterationArg { get; set; }
        
        public decimal FuncVal { get; set; }
    }
}