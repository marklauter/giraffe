using System;
using System.Diagnostics.CodeAnalysis;

namespace Graph.Classifiers
{
    public sealed class ClassifiedEventArgs
        : EventArgs
    {
        public ClassifiedEventArgs([DisallowNull] string label)
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
