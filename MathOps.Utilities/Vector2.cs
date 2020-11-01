namespace MathOps.Utilities
{
    public struct Vector2 
    {
        public Vector2(decimal first, decimal second) => (First, Second) = (first, second);
        
        public decimal First { get; private set; }
        public decimal Second { get; private set; }

        public void Deconstruct(out decimal first, out decimal second) => (first, second) = (First, Second);

        public static Vector2 operator *(decimal val, Vector2 vector)
        {
            var (first, second) = vector;
            return new Vector2
            {
                First = val * first,
                Second = val * second
            };
        }
        
        public static Vector2 operator *(Vector2 vector, decimal val)
        {
            var (first, second) = vector;
            return new Vector2
            {
                First = first * val,
                Second = second * val
            };
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            var (firstLeft, secondLeft) = left;
            var (firstRight, secondRight) = right;
            return new Vector2
            {
                First = firstLeft - firstRight,
                Second = secondLeft - secondRight
            };
        }
        
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            var (firstLeft, secondLeft) = left;
            var (firstRight, secondRight) = right;
            return new Vector2
            {
                First = firstLeft + firstRight,
                Second = secondLeft + secondRight
            };
        }
    }
}