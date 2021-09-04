using System;

namespace Graph.Elements
{
    public enum ClassificationChangeType
    {
        Classifiy,
        Declassify
    }


    /// <summary>
    /// Event is raised when element is classified or declassified.
    /// Indexers and caches can register with this event.
    /// </summary>
    public sealed class ClassificationChangedEventArgs
        : EventArgs
    {
        public ClassificationChangedEventArgs(string label, ClassificationChangeType type)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.Label = label;
            this.Type = type;
        }

        public string Label { get; }
        public ClassificationChangeType Type { get; }
    }
}
