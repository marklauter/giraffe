using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Attributes
{
    public sealed partial class QualifiedElement<TId>
        : IEquatable<QualifiedElement<TId>>
        , IEqualityComparer<QualifiedElement<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public bool Equals(QualifiedElement<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(QualifiedElement<TId> x, QualifiedElement<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is QualifiedElement<TId> element
                && this.Equals(element);
        }

        public int GetHashCode([DisallowNull] QualifiedElement<TId> obj)
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
