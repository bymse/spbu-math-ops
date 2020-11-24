using System;

namespace MathOps.Utilities
{
    public class TwoDimensionalGradient
    {
        public Vector2 Calculate(Vector2 point, int precision) => new Vector2(First(point), Second(point));
        
        public Func<Vector2, decimal> First { get; set; }
        public Func<Vector2, decimal> Second { get; set; }
    }
}