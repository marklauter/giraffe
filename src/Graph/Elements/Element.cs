using Graph.Classifiers;
using Graph.Qualifiers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Elements
{
    [DebuggerDisplay("{Id}")]
    public abstract class Element<TId>
        : IElement<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [JsonProperty]
        private readonly ClassificationCollection classifications = ClassificationCollection.Empty;

        [JsonProperty]
        private readonly AttributeCollection attributes = AttributeCollection.Empty;

        protected Element() { }

        protected Element(TId id)
        {
            this.Id = id;
        }

        protected Element([DisallowNull, Pure] Element<TId> other)
        {
            this.Id = other.Id;
        }

        /// <inheritdoc/>
        [Key]
        [Required]
        [JsonProperty("id")]
        public TId Id { get; }

        /// <inheritdoc/>
        public event EventHandler<ClassifiedEventArgs> Classified
        {
            add { this.classifications.Classified += value; }
            remove { this.classifications.Classified -= value; }
        }

        /// <inheritdoc/>
        public event EventHandler<DeclassifiedEventArgs> Declassified
        {
            add { this.classifications.Declassified += value; }
            remove { this.classifications.Declassified -= value; }
        }

        /// <inheritdoc/>
        public event EventHandler<QualifiedEventArgs> Qualified
        {
            add { this.attributes.Qualified += value; }
            remove { this.attributes.Qualified -= value; }
        }

        /// <inheritdoc/>
        public event EventHandler<DisqualifiedEventArgs> Disqualified
        {
            add { this.attributes.Disqualified += value; }
            remove { this.attributes.Disqualified -= value; }
        }

        /// <inheritdoc/>
        public IElement<TId> Classify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            _ = this.classifications.Classify(label);
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public abstract object Clone();

        /// <inheritdoc/>
        public IElement<TId> Declassify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            _ = this.classifications.Declassify(label);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Disqualify(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Disqualify(name);
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public bool HasProperty(string name)
        {
            return String.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name))
                : this.attributes.HasQuality(name);
        }

        /// <inheritdoc/>
        [Pure]
        public bool Is(string label)
        {
            return String.IsNullOrWhiteSpace(label)
                ? throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label))
                : this.classifications.Is(label);
        }

        /// <inheritdoc/>
        [Pure]
        public bool Is(IEnumerable<string> labels)
        {
            return labels is null
                ? throw new ArgumentNullException(nameof(labels))
                : this.classifications.Is(labels);
        }

        /// <inheritdoc/>
        [Pure]
        public bool TryGetProperty(string name, out object value)
        {
            return String.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name))
                : this.attributes.TryGetValue(name, out value);
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, bool value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, sbyte value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, byte value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, short value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, ushort value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, int value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, uint value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, long value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, ulong value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, float value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, double value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, decimal value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, DateTime value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, string value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes.Qualify(name, value);
            return this;
        }
    }
}
