namespace MathOps.Utilities
{
    public static class Vector2Extensions
    {
        public static Vector2Matrix MultiplyByTransposed(this Vector2 left, Vector2 right, int precision)
        {
            return new Vector2Matrix
            {
                FirstLine = new Vector2((left.First * right.First).RoundTo(precision), (left.First * right.Second).RoundTo(precision)),
                SecondLine = new Vector2((left.Second * right.First).RoundTo(precision), (left.Second * right.Second).RoundTo(precision))
            };
        }
        
        public static decimal TransposeAndMultiply(this Vector2 left, Vector2 right, int precision)
        {
            return (left.First * right.First + left.Second * right.Second).RoundTo(precision);
        }
    }
}