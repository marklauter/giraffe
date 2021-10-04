using Graphs.Events;
using System;

namespace Graphs.Classifications
{
    public class ClassifiedEventArgs<TId>
        : GraphEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public ClassifiedEventArgs(TId elementId, string label)
            : base()
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.ElementId = elementId;
            this.Label = label;
        }

        public TId ElementId { get; }

        public string Label { get; }
    }
}
