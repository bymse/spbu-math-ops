using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MathOps.Utilities;

namespace MathOps.NelderMeadMethod
{
    internal class VertexesModificationsHandler
    {
        private readonly Func<Vector2, decimal> function;
        private readonly NelderMeadMethodCoeffs coeffs;

        public VertexesModificationsHandler(Func<Vector2, decimal> function,
            NelderMeadMethodCoeffs coeffs)
        {
            this.function = function;
            this.coeffs = coeffs;
        }

        public IReadOnlyList<Vector2> ModifyVertexes(NelderMeadMethodIteration iteration,
            IReadOnlyList<Vector2> inputVertexes)
        {
            var (_, max, balance) = iteration;
            iteration.ReflectedVertex = balance + coeffs.ReflectionCoeff * (balance - max);

            return HandleExpansion(iteration, inputVertexes)
                   ?? HandleContraction(iteration, inputVertexes)
                   ?? HandleMaxSubstitutionToReflected(iteration, inputVertexes)
                   ?? HandleShrink(iteration, inputVertexes)
                   ?? throw new ArgumentException("No handlers available");
            ;
        }

        [CanBeNull]
        private IReadOnlyList<Vector2> HandleExpansion(NelderMeadMethodIteration iteration,
            IReadOnlyList<Vector2> inputVertexes)
        {
            var (min, max, balance) = iteration;
            var minVal = function(min);
            if (function(iteration.ReflectedVertex) <= minVal)
            {
                iteration.ExpandedVertex = balance + coeffs.ExpansionCoeff * (iteration.ReflectedVertex - balance);

                var substitute = function(iteration.ExpandedVertex.Value) < minVal
                    ? iteration.ExpandedVertex.Value
                    : iteration.ReflectedVertex;

                return inputVertexes.Select(e => e == max
                    ? substitute
                    : e).ToArray();
            }

            return null;
        }

        [CanBeNull]
        private IReadOnlyList<Vector2> HandleContraction(NelderMeadMethodIteration iteration,
            IReadOnlyList<Vector2> inputVertexes)
        {
            var (_, max, balance) = iteration;
            var reflectedPointValue = function(iteration.ReflectedVertex);
            if (function(iteration.SecondMaxValueVertex) < reflectedPointValue &&
                reflectedPointValue <= function(max))
            {
                var substitute = balance + coeffs.ContractionCoeff * (max - balance);

                return inputVertexes.Select(e => e == max
                    ? substitute
                    : e).ToArray();
            }

            return null;
        }

        [CanBeNull]
        private IReadOnlyList<Vector2> HandleMaxSubstitutionToReflected(NelderMeadMethodIteration iteration,
            IReadOnlyList<Vector2> inputVertexes)
        {
            var (min, max, _) = iteration;
            var reflectedPointValue = function(iteration.ReflectedVertex);
            if (function(min) < reflectedPointValue &&
                reflectedPointValue <= function(iteration.SecondMaxValueVertex))
            {
                return inputVertexes.Select(e => e == max
                    ? iteration.ReflectedVertex
                    : e).ToArray();
            }

            return null;
        }

        [CanBeNull]
        private IReadOnlyList<Vector2> HandleShrink(NelderMeadMethodIteration iteration,
            IReadOnlyList<Vector2> inputVertexes)
        {
            if (function(iteration.ReflectedVertex) > function(iteration.MaxValueVertex))
            {
                return inputVertexes
                    .Select(vertex => iteration.MinValueVertex +
                                      0.5M * (vertex - iteration.MinValueVertex))
                    .ToArray();
            }

            return null;
        }
    }
}