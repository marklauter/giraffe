using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Connections
{
    public sealed partial class Neighbors<TId>
        : IEquatable<Neighbors<TId>>
        , IEqualityComparer<Neighbors<TId>>
       where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public bool Equals(Neighbors<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(Neighbors<TId> x, Neighbors<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is Neighbors<TId> element
                && this.Equals(element);
        }

        public int GetHashCode([DisallowNull] Neighbors<TId> obj)
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
