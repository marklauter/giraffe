using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Edges
{
    public sealed partial class IncidentEdges<TId>
        : IEquatable<IncidentEdges<TId>>
        , IEqualityComparer<IncidentEdges<TId>>
    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public bool Equals(IncidentEdges<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(IncidentEdges<TId> x, IncidentEdges<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is IncidentEdges<TId> element
                && this.Equals(element);
        }

        public int GetHashCode([DisallowNull] IncidentEdges<TId> obj)
        {
            return obj is null
                ? throw new ArgumentNullException(nameof(obj))
                : obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }
    }
}
