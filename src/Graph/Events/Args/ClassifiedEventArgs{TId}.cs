using Graphs.Elements;
using System;

namespace Graphs.Events
{
    public class ClassifiedEventArgs<TId>
        : GraphEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public ClassifiedEventArgs(Element<TId> element, string label)
            : base()
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.Element = element ?? throw new ArgumentNullException(nameof(element));
            this.Label = label;
        }

        public Element<TId> Element { get; }

        public string Label { get; }
    }
}
