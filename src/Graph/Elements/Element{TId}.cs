using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Graphs.Elements
{
    [DebuggerDisplay("{Id}")]
    public abstract class Element<TId>
        : IQualifiable
        , IQualifierEventSource<TId>
        , IClassifiable
        , IClassifierEventSource<TId>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private ImmutableHashSet<string> labels = ImmutableHashSet<string>.Empty;
        private ImmutableDictionary<string, object> attributes = ImmutableDictionary<string, object>.Empty;

        public event EventHandler<QualifiedEventArgs<TId>> Qualified;
        public event EventHandler<DisqualifiedEventArgs<TId>> Disqualified;

        public event EventHandler<ClassifiedEventArgs<TId>> Classified;
        public event EventHandler<DeclassifiedEventArgs<TId>> Declassified;

        public TId Id { get; }

        public IEnumerable<string> Labels => this.labels;

        public IEnumerable<KeyValuePair<string, object>> Attributes => this.attributes;

        protected Element(TId id)
        {
            this.Id = id;
        }

        protected Element(Element<TId> other)
            : this(other.Id)
        {
            this.labels = other.labels;
            this.attributes = other.attributes;
        }

        protected Element(
            TId id,
            IEnumerable<string> labels,
            IEnumerable<KeyValuePair<string, object>> attributes)
            : this(id)
        {
            this.labels = labels.ToImmutableHashSet();
            this.attributes = attributes.ToImmutableDictionary();
        }

        public abstract object Clone();

        public void Classify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.labels = this.labels.Add(label);

            this.Classified?.Invoke(this, new ClassifiedEventArgs<TId>(label, this.Id));
        }

        public void Declassify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.labels = this.labels.Remove(label);

            this.Classified?.Invoke(this, new DeclassifiedEventArgs<TId>(label, this.Id));
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
                : labels.All(l => this.Is(l));
        }

        public void Qualify(string name, object value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes = this.attributes.SetItem(name, value);
            this.Qualified?.Invoke(this, new QualifiedEventArgs<TId>(name, value, this.Id));
        }

        public void Disqualify(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (this.attributes.TryGetValue(name, out var value))
            {
                this.attributes = this.attributes.Remove(name);
                this.Qualified?.Invoke(this, new DisqualifiedEventArgs<TId>(name, value, this.Id));
            }
        }

        public bool HasAttribute(string name)
        {
            return String.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name))
                : this.attributes.ContainsKey(name);
        }

        public bool TryGetValue(string name, out object value)
        {
            return String.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name))
                : this.attributes.TryGetValue(name, out value);
        }
    }
}
