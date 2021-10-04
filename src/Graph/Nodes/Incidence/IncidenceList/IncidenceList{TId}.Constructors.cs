using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Incidence
{
    public sealed partial class IncidenceList<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IncidenceList(TId id)
        {
            this.Id = id;
        }

        public IncidenceList(TId id, IEnumerable<TId> edges)
            : this(id)
        {
            this.edges = edges.ToImmutableHashSet();
        }

        private IncidenceList(IncidenceList<TId> other)
            : this(other.Id)
        {
            this.edges = other.edges;
        }

        public object Clone()
        {
            return new IncidenceList<TId>(this);
        }
    }
}
