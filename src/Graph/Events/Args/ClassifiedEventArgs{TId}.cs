using System;

namespace Graphs.Events
{
    public class ClassifiedEventArgs<TId>
        : GraphEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public ClassifiedEventArgs(string label, TId id)
            : base()
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.ElementId = id;
            this.Label = label;
        }

        public TId ElementId { get; }

        public string Label { get; }
    }
}
