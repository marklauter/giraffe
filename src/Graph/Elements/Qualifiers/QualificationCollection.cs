using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graphs.Elements.Qualifiers
{
    /// <inheritdoc/>
    internal sealed class QualificationCollection
        : IQualifier
        , IEnumerable<KeyValuePair<string, object>>
    {
        /// <inheritdoc/>
        public event EventHandler<QualifiedEventArgs> Qualified;

        /// <inheritdoc/>
        public event EventHandler<DisqualifiedEventArgs> Disqualified;

        private readonly ConcurrentDictionary<string, object> qualifications;

        public static QualificationCollection Empty => new();

        private QualificationCollection()
        {
            this.qualifications = new();
        }

        private QualificationCollection([DisallowNull, Pure] QualificationCollection other)
        {
            this.qualifications = new(other.qualifications);
        }

        internal QualificationCollection([DisallowNull, Pure] IEnumerable<KeyValuePair<string, object>> qualifications)
        {
            this.qualifications = new(qualifications);
        }

        /// <inheritdoc/>
        [Pure]
        public object Clone()
        {
            return new QualificationCollection(this);
        }

        /// <inheritdoc/>
        public IQualifier Disqualify(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (this.qualifications.TryRemove(name, out var _))
            {
                Disqualified?.Invoke(this, new DisqualifiedEventArgs(name));
            }

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
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
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

        /// <inheritdoc/>
        public IQualifier Qualify(string name, object value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.qualifications.AddOrUpdate(name, value, (name, original) => value);
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
            if (this.qualifications.TryGetValue(name, out var obj))
            {
                value = obj;
                return true;
            }

            return false;
        }
    }
}
