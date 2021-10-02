using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Attributes
{
    public sealed partial class Qualifiable<TId>
        : IEquatable<Qualifiable<TId>>
        , IEqualityComparer<Qualifiable<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public bool Equals(Qualifiable<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(Qualifiable<TId> x, Qualifiable<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is Qualifiable<TId> element
                && this.Equals(element);
        }

        public int GetHashCode([DisallowNull] Qualifiable<TId> obj)
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
