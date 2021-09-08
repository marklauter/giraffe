using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Quantifiers
{
    public sealed class QuantityRemovedEventArgs
        : EventArgs
    {
        public QuantityRemovedEventArgs([DisallowNull, Pure] string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.Label = name;
        }

        public string Label { get; }
    }
}
