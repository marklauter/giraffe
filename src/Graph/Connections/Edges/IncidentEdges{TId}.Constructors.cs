using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Connections
{
    public sealed partial class IncidentEdges<TId>
        : ICloneable
    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IncidentEdges(TId id)
        {
            this.Id = id;
        }

        public IncidentEdges(IEnumerable<TId> edges)
        {
            this.edges = edges.ToImmutableHashSet();
        }

        private IncidentEdges(IncidentEdges<TId> other)
        {
            this.edges = other.edges;
        }

        public object Clone()
        {
            return new IncidentEdges<TId>(this);
        }
    }
}
