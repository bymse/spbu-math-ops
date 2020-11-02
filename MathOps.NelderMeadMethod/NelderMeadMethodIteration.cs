using System.Collections.Generic;
using MathOps.Utilities;

namespace MathOps.NelderMeadMethod
{
    public class NelderMeadMethodIteration
    {
        public int Iteration { get; set; }

        public Vector2 MinValueVertex { get; set; }
        public Vector2 SecondMaxValueVertex { get; set; }
        public Vector2 MaxValueVertex { get; set; }
        public Vector2 BalancePointVertex { get; set; }

        public Vector2 ReflectedVertex { get; set; }
        public Vector2? ExpandedVertex { get; set; }
        public Vector2? ContractedVertex { get; set; }

        public IReadOnlyList<Vector2> NextVertexList { get; set; }


        public void Deconstruct(out Vector2 min, out Vector2 max, out Vector2 balance) =>
            (min, max, balance) = (MinValueVertex, MaxValueVertex, BalancePointVertex);
    }
}