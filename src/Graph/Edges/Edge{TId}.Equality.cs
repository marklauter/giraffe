using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Edges
{
    public sealed partial class Edge<TId>
        : IEquatable<Edge<TId>>
        , IEqualityComparer<Edge<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public bool Equals(Edge<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(Edge<TId> x, Edge<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is Edge<TId> element
                && this.Equals(element);
        }

        public int GetHashCode([DisallowNull] Edge<TId> obj)
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
