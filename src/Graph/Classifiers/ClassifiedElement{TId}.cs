using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Graphs.Classifiers
{
    [DebuggerDisplay("{Id}")]
    public sealed partial class ClassifiedElement<TId>
        : IClassifiable<TId>
        , IClassifierEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private ImmutableHashSet<string> labels = ImmutableHashSet<string>.Empty;

        public event EventHandler<ClassifiedEventArgs<TId>> Classified;
        public event EventHandler<DeclassifiedEventArgs<TId>> Declassified;

        public TId Id { get; }

        public void Classify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.labels = this.labels.Add(label);
            this.Classified?.Invoke(this, new ClassifiedEventArgs<TId>(this.Id, label));
        }

        public void Declassify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.labels = this.labels.Remove(label);
            this.Declassified?.Invoke(this, new DeclassifiedEventArgs<TId>(this.Id, label));
        }

        public bool Is(string label)
        {
            return String.IsNullOrWhiteSpace(label)
                ? throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label))
                : this.labels.Contains(label);
        }

        public bool Is(IEnumerable<string> labels)
        {
            return labels is null
                ? throw new ArgumentNullException(nameof(labels))
                : this.labels.IsSupersetOf(labels.Where(label => !String.IsNullOrWhiteSpace(label)));
        }
    }
}
