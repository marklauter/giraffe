using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Adjacency
{
    public sealed partial class AdjacencyList<TId>
        : IEquatable<AdjacencyList<TId>>
        , IEqualityComparer<AdjacencyList<TId>>
       where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public bool Equals(AdjacencyList<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(AdjacencyList<TId> x, AdjacencyList<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is AdjacencyList<TId> element
                && this.Equals(element);
        }

        public int GetHashCode([DisallowNull] AdjacencyList<TId> obj)
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
