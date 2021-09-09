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
    internal sealed class AttributeCollection
        : IQualifiable
        , IEnumerable<(string Key, SerializableValue Value)>
    {
        /// <inheritdoc/>
        public event EventHandler<QualifiedEventArgs> Qualified;

        /// <inheritdoc/>
        public event EventHandler<DisqualifiedEventArgs> Disqualified;

        private ImmutableDictionary<string, SerializableValue> qualifications = ImmutableDictionary<string, SerializableValue>.Empty;

        public static AttributeCollection Empty => new();

        private AttributeCollection() { }

        private AttributeCollection([DisallowNull, Pure] AttributeCollection other)
        {
            this.qualifications = other.qualifications;
        }

        [JsonConstructor]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used for serialization.")]
        private AttributeCollection([DisallowNull, Pure] IEnumerable<(string Key, SerializableValue Value)> qualifications)
        {
            this.qualifications = qualifications
                .ToImmutableDictionary(q => q.Key, q => q.Value);
        }

        /// <inheritdoc/>
        [Pure]
        public object Clone()
        {
            return new AttributeCollection(this);
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
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, sbyte value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, byte value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, short value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, ushort value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, int value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, uint value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, long value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, ulong value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, float value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, double value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, decimal value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, DateTime value)
        {
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        public IQualifiable Qualify(string name, string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
            }

            return this.SetQualification(name, value);
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

        private IQualifiable SetQualification(string name, object value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <summary>
        /// this method is required because passing the string as an object breaks the explicit cast to SerializableValue
        /// </summary>
        private IQualifiable SetQualification(string name, string value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications = this.qualifications.SetItem(name, (SerializableValue)value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }
    }
}
