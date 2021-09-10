using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Qualifiers
{
    /// <inheritdoc/>
    [JsonArray]
    internal sealed class QualificationCollection
        : IQualifiable
        , IEnumerable<(string Key, SerializableValue Value)>
    {
        /// <inheritdoc/>
        public event EventHandler<QualifiedEventArgs> Qualified;

        /// <inheritdoc/>
        public event EventHandler<DisqualifiedEventArgs> Disqualified;

        private ImmutableDictionary<string, SerializableValue> qualifications = ImmutableDictionary<string, SerializableValue>.Empty;

        public static QualificationCollection Empty => new();

        private QualificationCollection() { }

        private QualificationCollection([DisallowNull, Pure] QualificationCollection other)
        {
            this.qualifications = other.qualifications;
        }

        [JsonConstructor]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used for serialization.")]
        private QualificationCollection([DisallowNull, Pure] IEnumerable<(string Key, SerializableValue Value)> qualifications)
        {
            this.qualifications = qualifications
                .ToImmutableDictionary(q => q.Key, q => q.Value);
        }

        /// <inheritdoc/>
        [Pure]
        public object Clone()
        {
            return new QualificationCollection(this);
        }

        /// <inheritdoc/>
        public IQualifiable Disqualify(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.Remove(name);
            Disqualified?.Invoke(this, new DisqualifiedEventArgs(name));
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public bool HasQuality(string name)
        {
            return String.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name))
                : this.qualifications.ContainsKey(name);
        }

        /// <inheritdoc/>
        [Pure]
        public IEnumerator<(string Key, SerializableValue Value)> GetEnumerator()
        {
            foreach (var kvp in this.qualifications)
            {
                yield return (kvp.Key, kvp.Value);
            }
        }

        /// <inheritdoc/>
        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, bool value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, sbyte value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, byte value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, short value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, ushort value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, int value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, uint value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, long value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, ulong value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, float value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, double value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, decimal value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, DateTime value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, string value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public bool TryGetValue(string name, out object value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            value = null;
            if (this.qualifications.TryGetValue(name, out var serializableValue))
            {
                value = serializableValue.Value;
                return true;
            }

            return false;
        }
    }
}
