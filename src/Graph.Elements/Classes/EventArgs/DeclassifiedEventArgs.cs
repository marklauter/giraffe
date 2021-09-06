using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Elements
{
    public sealed class DeclassifiedEventArgs
        : EventArgs
    {
        public DeclassifiedEventArgs([DisallowNull, Pure] string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.Label = label;
        }

        public string Label { get; }
    }
}
