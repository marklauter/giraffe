using System;

namespace Graphs.Events
{
    public class QualifiedEventArgs<TId>
        : GraphEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public QualifiedEventArgs(string name, object value, TId elementId)
            : base()
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.Name = name;
            this.Value = value;
            this.ElementId = elementId;
        }

        public TId ElementId { get; }

        public string Name { get; }

        public object Value { get; }
    }
}
