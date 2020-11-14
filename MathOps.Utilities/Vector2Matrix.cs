namespace MathOps.Utilities
{
    public class Vector2Matrix
    {
        public Vector2 FirstLine { get; set; }
        public Vector2 SecondLine { get; set; }


        public static Vector2 operator *(Vector2Matrix matrix, Vector2 vector)
        {
            var first = matrix.FirstLine.First * vector.First + matrix.FirstLine.Second * vector.Second;
            var second = matrix.SecondLine.First * vector.First + matrix.SecondLine.Second * vector.Second;
            return new Vector2(first, second);
        }
        
        public static Vector2Matrix operator /(Vector2Matrix matrix, decimal numb)
        {
            return new Vector2Matrix
            {
                FirstLine = new Vector2(matrix.FirstLine.First / numb, matrix.FirstLine.Second / numb),
                SecondLine = new Vector2(matrix.SecondLine.First / numb, matrix.SecondLine.Second / numb),
            };
        }
        
        public static Vector2Matrix operator *(Vector2Matrix matrix, decimal numb)
        {
            return new Vector2Matrix
            {
                FirstLine = new Vector2(matrix.FirstLine.First * numb, matrix.FirstLine.Second * numb),
                SecondLine = new Vector2(matrix.SecondLine.First * numb, matrix.SecondLine.Second * numb),
            };
        }

        public static Vector2 operator *(Vector2 vector, Vector2Matrix matrix)
        {
            var first = vector.First * matrix.FirstLine.First + vector.Second * matrix.SecondLine.First;
            var second = vector.First * matrix.FirstLine.Second + vector.Second * matrix.SecondLine.Second;
            return new Vector2(first, second);
        }

        public static Vector2Matrix operator +(Vector2Matrix left, Vector2Matrix right)
        {
            return new Vector2Matrix
            {
                FirstLine = new Vector2(
                    left.FirstLine.First + right.FirstLine.First,
                    left.FirstLine.Second + right.FirstLine.Second),
                
                SecondLine = new Vector2(
                    left.SecondLine.First + right.SecondLine.First,
                    left.SecondLine.Second + right.SecondLine.Second),
            };
        }
        
        public static Vector2Matrix operator -(Vector2Matrix left, Vector2Matrix right)
        {
            return new Vector2Matrix
            {
                FirstLine = new Vector2(
                    left.FirstLine.First - right.FirstLine.First,
                    left.FirstLine.Second - right.FirstLine.Second),
                
                SecondLine = new Vector2(
                    left.SecondLine.First - right.SecondLine.First,
                    left.SecondLine.Second - right.SecondLine.Second),
            };
        }
        
        public static Vector2Matrix operator *(Vector2Matrix left, Vector2Matrix right)
        {
            return new Vector2Matrix
            {
                FirstLine = new Vector2(
                    left.FirstLine.First * right.FirstLine.First + left.FirstLine.Second * right.SecondLine.First,
                    left.FirstLine.First * right.FirstLine.Second + left.FirstLine.Second * right.SecondLine.Second),
                
                SecondLine = new Vector2(
                    left.SecondLine.First * right.FirstLine.First + left.SecondLine.Second * right.SecondLine.First,
                    left.SecondLine.First * right.FirstLine.Second + left.SecondLine.Second * right.SecondLine.Second),
            };
        }
    }
}