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
        , IEnumerable<(string Key, object Value)>
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
        private QualificationCollection([DisallowNull, Pure] IEnumerable<KeyValuePair<string, SerializableValue>> qualifications)
        {
            this.qualifications = this.qualifications.AddRange(qualifications);
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
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            return this.qualifications.ContainsKey(name);
        }

        /// <inheritdoc/>
        [Pure]
        public IEnumerator<(string Key, object Value)> GetEnumerator()
        {
            foreach (var kvp in this.qualifications)
            {
                yield return (kvp.Key, kvp.Value.Value);
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
            return this.SetQualification(name, value);
        }

        /// <inheritdoc/>
        [Pure]
        public object Value(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            return this.qualifications.TryGetValue(name, out var value)
                ? value
                : null;
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
    }
}
