using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Quantifiers
{
    public sealed class QuantifiedEventArgs
        : EventArgs
    {
        public QuantifiedEventArgs([DisallowNull, Pure] Quantity quantity)
        {
            this.Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
        }

        public Quantity Quantity { get; }
    }
}
