using System;

namespace Graphs.Elements.Qualifiers
{
    public sealed class QualifiedEventArgs<TId>
        : QualifiedEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public QualifiedEventArgs(string name, object value, TId id)
            : base(name, value)
        {
            this.Id = id;
        }

        public TId Id { get; }
    }
}
