using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Elements
{
    public sealed class QuantificationChangedEventArgs
        : EventArgs
    {
        public QuantificationChangedEventArgs([DisallowNull, Pure] IQuantity quantity)
        {
            this.Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
        }

        public IQuantity Quantity { get; }
    }
}
