using Graphs.Events;
using System;

namespace Graphs.Attributes
{
    public abstract class QualifiableEventArgs<TId>
        : GraphEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        protected QualifiableEventArgs(TId elementId, string name)
            : base()
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.ElementId = elementId;
            this.Name = name;
        }

        public TId ElementId { get; }

        public string Name { get; }
    }
}
