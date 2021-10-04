using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Nodes
{
    public sealed partial class AdjancencyList<TId>
        : IEquatable<AdjancencyList<TId>>
        , IEqualityComparer<AdjancencyList<TId>>
       where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public bool Equals(AdjancencyList<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(AdjancencyList<TId> x, AdjancencyList<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is AdjancencyList<TId> element
                && this.Equals(element);
        }

        public int GetHashCode([DisallowNull] AdjancencyList<TId> obj)
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
