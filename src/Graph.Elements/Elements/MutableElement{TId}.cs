using Graphs.Elements.Classifiers;
using Graphs.Elements.Qualifiers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graphs.Elements
{
    [DebuggerDisplay("{Id}")]
    public abstract class MutableElement<TId>
        : IMutable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [JsonProperty]
        private readonly ClassificationCollection classifications = ClassificationCollection.Empty;

        [JsonProperty]
        private readonly QualificationCollection qualifications = QualificationCollection.Empty;

        protected MutableElement()
        {
            this.classifications.Classified += this.Classifications_Classified;
            this.classifications.Declassified += this.Classifications_Declassified;
            this.qualifications.Disqualified += this.Qualifications_Disqualified;
            this.qualifications.Qualified += this.Qualifications_Qualified;
        }

        protected MutableElement(TId id)
            : this()
        {
            this.Id = id;
        }

        protected MutableElement([DisallowNull, Pure] MutableElement<TId> other)
            : this()
        {
            this.classifications = other.classifications.Clone() as ClassificationCollection;
            this.classifications.Classified += this.Classifications_Classified;
            this.classifications.Declassified += this.Classifications_Declassified;

            this.qualifications = other.qualifications.Clone() as QualificationCollection;
            this.qualifications.Disqualified += this.Qualifications_Disqualified;
            this.qualifications.Qualified += this.Qualifications_Qualified;

            this.Id = other.Id;
        }

        /// <inheritdoc/>
        [Key]
        [Required]
        [JsonProperty("id")]
        public TId Id { get; }

        /// <inheritdoc/>
        public event EventHandler<ClassifiedEventArgs> Classified;

        /// <inheritdoc/>
        public event EventHandler<DeclassifiedEventArgs> Declassified;

        /// <inheritdoc/>
        public event EventHandler<QualifiedEventArgs> Qualified;

        /// <inheritdoc/>
        public event EventHandler<DisqualifiedEventArgs> Disqualified;

        /// <inheritdoc/>
        public IMutable<TId> Classify(string label)
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
        public IMutable<TId> Declassify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            _ = this.classifications.Declassify(label);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Disqualify(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            _ = this.qualifications.Disqualify(name);
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public bool HasProperty(string name)
        {
            return String.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name))
                : this.qualifications.HasQuality(name);
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
        public bool TryGetValue(string name, out object value)
        {
            return String.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name))
                : this.qualifications.TryGetValue(name, out value);
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, bool value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, sbyte value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, byte value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, short value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, ushort value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, int value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, uint value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, long value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, ulong value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, float value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, double value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, decimal value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, DateTime value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IMutable<TId> Qualify(string name, string value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.Qualify(name, value);
            return this;
        }

        private void Qualifications_Disqualified(object sender, DisqualifiedEventArgs e)
        {
            this.Disqualified?.Invoke(this, e);
        }

        private void Qualifications_Qualified(object sender, QualifiedEventArgs e)
        {
            this.Qualified?.Invoke(this, e);
        }

        private void Classifications_Declassified(object sender, DeclassifiedEventArgs e)
        {
            this.Declassified?.Invoke(this, e);
        }

        private void Classifications_Classified(object sender, ClassifiedEventArgs e)
        {
            this.Classified?.Invoke(this, e);
        }
    }
}
