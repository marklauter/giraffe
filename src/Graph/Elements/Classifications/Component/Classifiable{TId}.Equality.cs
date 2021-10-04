using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Classifications
{
    public sealed partial class Classifiable<TId>
        : IEquatable<Classifiable<TId>>
        , IEqualityComparer<Classifiable<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public bool Equals(Classifiable<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(Classifiable<TId> x, Classifiable<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is Classifiable<TId> element
                && this.Equals(element);
        }

        public int GetHashCode([DisallowNull] Classifiable<TId> obj)
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
