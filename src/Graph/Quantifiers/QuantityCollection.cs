using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Quantifiers
{
    /// <inheritdoc/>
    [JsonArray]
    internal sealed class QuantityCollection
        : IQuantifiable
        , IEnumerable<IQuantity>
    {
        /// <inheritdoc/>
        public event EventHandler<QuantificationChangedEventArgs> QuantificationChanged;

        /// <inheritdoc/>
        public event EventHandler<QuantificationIgnoredEventArgs> QuantificationIngnored;

        private ImmutableDictionary<string, IQuantity> quantities = ImmutableDictionary<string, IQuantity>.Empty;

        public static QuantityCollection Empty => new();

        private QuantityCollection() { }

        private QuantityCollection([DisallowNull, Pure] QuantityCollection other)
        {
            this.quantities = other.quantities;
        }

        [JsonConstructor]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used for serialization.")]
        private QuantityCollection([DisallowNull, Pure] IEnumerable<IQuantity> quantities)
        {
            this.quantities = quantities
                .ToImmutableDictionary(q => q.Name);
        }

        /// <inheritdoc/>
        [Pure]
        public object Clone()
        {
            return new QuantityCollection(this);
        }

        /// <inheritdoc/>
        [Pure]
        public bool HasQuantity(string name)
        {
            return this.quantities.ContainsKey(name);
        }

        /// <inheritdoc/>
        public IQuantifiable Ignore(string name)
        {
            this.quantities = this.quantities.Remove(name);
            QuantificationIngnored?.Invoke(this, new QuantificationIgnoredEventArgs(name));
            return this;
        }

        /// <inheritdoc/>
        public IQuantifiable Quantify([DisallowNull, Pure] IQuantity quantity)
        {
            this.quantities = this.quantities.SetItem(quantity.Name, quantity);
            QuantificationChanged?.Invoke(this, new QuantificationChangedEventArgs(quantity));
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public IQuantity Quantity(string name)
        {
            return this.quantities.TryGetValue(name, out var quantity)
                ? quantity
                : null;
        }

        /// <inheritdoc/>
        [Pure]
        public bool TryGetValue<T>(string name, out T value) where T : struct, IComparable, IComparable<T>, IEquatable<T>, IFormattable
        {
            value = default;
            if (this.quantities.TryGetValue(name, out var quantity))
            {
                value = quantity.ParseValue<T>();
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        [Pure]
        public IEnumerator<IQuantity> GetEnumerator()
        {
            foreach (var kvp in this.quantities)
            {
                yield return kvp.Value;
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
