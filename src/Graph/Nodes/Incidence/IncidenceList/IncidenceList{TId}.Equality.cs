using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Incidence
{
    public sealed partial class IncidenceList<TId>
        : IEquatable<IncidenceList<TId>>
        , IEqualityComparer<IncidenceList<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public bool Equals(IncidenceList<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(IncidenceList<TId> x, IncidenceList<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is IncidenceList<TId> element
                && this.Equals(element);
        }

        public int GetHashCode([DisallowNull] IncidenceList<TId> obj)
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
