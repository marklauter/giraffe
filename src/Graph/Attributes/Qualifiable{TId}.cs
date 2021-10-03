using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Graphs.Attributes
{
    [DebuggerDisplay("{Id}")]
    public sealed partial class Qualifiable<TId>
        : IQualifiable<TId>
        , IEnumerable<KeyValuePair<string, object>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private ImmutableDictionary<string, object> attributes = ImmutableDictionary<string, object>.Empty;

        public event EventHandler<QualifiedEventArgs<TId>> Qualified;
        public event EventHandler<DisqualifiedEventArgs<TId>> Disqualified;

        /// <summary>
        /// Element Id
        /// </summary>
        public TId Id { get; }

        public void Disqualify(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes = this.attributes.Remove(name);
            this.Disqualified?.Invoke(this, new DisqualifiedEventArgs<TId>(this.Id, name));
        }

        public bool HasAttribute(string name)
        {
            return String.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name))
                : this.attributes.ContainsKey(name);
        }

        public void Qualify(string name, object value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.attributes = this.attributes.SetItem(name, value);
            this.Qualified?.Invoke(this, new QualifiedEventArgs<TId>(this.Id, name, value));
        }

        public bool TryGetValue(string name, out object value)
        {
            return String.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name))
                : this.attributes.TryGetValue(name, out value);
        }

        public bool Equals(string name, object other)
        {
            return this.TryGetValue(name, out var value)
                && value.Equals(other);
        }
    }
}
