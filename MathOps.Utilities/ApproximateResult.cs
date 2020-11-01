namespace MathOps.Utilities
{
    public struct ApproximateResult
    {
        public decimal Value { get; set; }
        public decimal Arg { get; set; }
        public Boundaries Boundaries { get; set; }
        public int IterationsCount { get; set; }
    }
}