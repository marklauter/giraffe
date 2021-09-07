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
        , IEnumerable<KeyValuePair<string, string>>
    {
        /// <inheritdoc/>
        public event EventHandler<QualifiedEventArgs> Qualified;

        /// <inheritdoc/>
        public event EventHandler<DisqualifiedEventArgs> Disqualified;

        private ImmutableDictionary<string, string> qualifications = ImmutableDictionary<string, string>.Empty;

        public static QualificationCollection Empty => new();

        private QualificationCollection() { }

        private QualificationCollection([DisallowNull, Pure] QualificationCollection other)
        {
            this.qualifications = other.qualifications;
        }

        [JsonConstructor]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used for serialization.")]
        private QualificationCollection([DisallowNull, Pure] IEnumerable<KeyValuePair<string, string>> qualifications)
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
        public IQualifiable Qualify(string name, string value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
            }

            this.qualifications = this.qualifications.SetItem(name, value);
            Qualified?.Invoke(this, new QualifiedEventArgs(name, value));
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public string Quality(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            return this.qualifications.TryGetValue(name, out var value)
                ? value
                : null;
        }

        /// <inheritdoc/>
        [Pure]
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            foreach (var kvp in this.qualifications)
            {
                yield return kvp;
            }
        }

        /// <inheritdoc/>
        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
