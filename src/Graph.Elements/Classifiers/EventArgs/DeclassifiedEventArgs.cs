using System;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Elements.Classifiers
{
    public class DeclassifiedEventArgs
        : EventArgs
    {
        public DeclassifiedEventArgs([DisallowNull] string label)
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
