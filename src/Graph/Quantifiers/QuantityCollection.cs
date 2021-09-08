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
        , IEnumerable<Quantity>
    {
        /// <inheritdoc/>
        public event EventHandler<QuantifiedEventArgs> Quantified;

        /// <inheritdoc/>
        public event EventHandler<QuantityRemovedEventArgs> QuantityRemoved;

        private ImmutableDictionary<string, Quantity> quantities = ImmutableDictionary<string, Quantity>.Empty;

        public static QuantityCollection Empty => new();

        private QuantityCollection() { }

        private QuantityCollection([DisallowNull, Pure] QuantityCollection other)
        {
            this.quantities = other.quantities;
        }

        [JsonConstructor]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used for serialization.")]
        private QuantityCollection([DisallowNull, Pure] IEnumerable<Quantity> quantities)
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
        public IQuantifiable RemoveQuantity(string name)
        {
            this.quantities = this.quantities.Remove(name);
            QuantityRemoved?.Invoke(this, new QuantityRemovedEventArgs(name));
            return this;
        }

        /// <inheritdoc/>
        public IQuantifiable Quantify([DisallowNull, Pure] Quantity quantity)
        {
            this.quantities = this.quantities.SetItem(quantity.Name, quantity);
            Quantified?.Invoke(this, new QuantifiedEventArgs(quantity));
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public Quantity Quantity(string name)
        {
            return this.quantities.TryGetValue(name, out var quantity)
                ? quantity
                : null;
        }

        /// <inheritdoc/>
        [Pure]
        public IEnumerator<Quantity> GetEnumerator()
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
