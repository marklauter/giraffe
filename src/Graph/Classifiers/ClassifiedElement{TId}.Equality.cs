using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Classifiers
{
    public sealed partial class ClassifiedElement<TId>
        : IEquatable<ClassifiedElement<TId>>
        , IEqualityComparer<ClassifiedElement<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public bool Equals(ClassifiedElement<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(ClassifiedElement<TId> x, ClassifiedElement<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is ClassifiedElement<TId> element
                && this.Equals(element);
        }

        public int GetHashCode([DisallowNull] ClassifiedElement<TId> obj)
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
