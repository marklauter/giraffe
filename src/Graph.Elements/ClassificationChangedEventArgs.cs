using System;

namespace Graph.Elements
{
    /// <summary>
    /// Event is raised when element is classified or declassified.
    /// Indexers and caches can register with this event.
    /// </summary>
    public sealed class ClassificationChangedEventArgs
        :EventArgs
    {
        public ClassificationChangedEventArgs(string label)
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
