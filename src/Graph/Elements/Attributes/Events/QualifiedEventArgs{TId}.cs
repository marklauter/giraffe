using System;

namespace Graphs.Attributes
{
    public class QualifiedEventArgs<TId>
        : QualifiableEventArgs<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public QualifiedEventArgs(TId elementId, string name, object value)
            : base(elementId, name)
        {
            this.Value = value;
        }

        public object Value { get; }
    }
}
