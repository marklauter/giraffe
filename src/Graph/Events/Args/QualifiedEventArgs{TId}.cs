using Graphs.Elements;
using System;

namespace Graphs.Events
{
    public class QualifiedEventArgs<TId>
        : GraphEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public QualifiedEventArgs(Element<TId> element, string name, object value)
            : base()
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.Element = element ?? throw new ArgumentNullException(nameof(element));
            this.Name = name;
            this.Value = value;
        }

        public Element<TId> Element { get; }

        public string Name { get; }

        public object Value { get; }
    }
}
