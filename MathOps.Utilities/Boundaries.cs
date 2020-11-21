namespace MathOps.Utilities
{
    public struct Boundaries
    {
        public Boundaries(decimal left, decimal right) => (Left, Right) = (left, right);

        public decimal Left;
        public decimal Right;

        public void Deconstruct(out decimal left, out decimal right) => (left, right) = (Left, Right);

        public override string ToString()
        {
            return $"<{Left};{Right}>";
        }
    }
}