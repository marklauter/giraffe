using System;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Elements.Classifiers
{
    public sealed class DeclassifiedEventArgs<TId>
        : DeclassifiedEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DeclassifiedEventArgs([DisallowNull] string label, TId id)
            : base(label)
        {
            this.Id = id;
        }

        public TId Id { get; }
    }
}
