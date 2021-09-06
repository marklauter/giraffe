using System;
using System.Collections.Generic;

namespace Graph.Classifiers
{
    /// <summary>
    /// Event is raised when element is classified.
    /// Indexers and caches can register with this event.
    /// </summary>
    public sealed class ItemsClassifiedEventArgs<T>
        : EventArgs
    {
        public ItemsClassifiedEventArgs(string label, T item)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.Label = label;
            this.Items = new T[] { item };
        }

        public ItemsClassifiedEventArgs(string label, IEnumerable<T> items)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.Label = label;
            this.Items = items;
        }

        public string Label { get; }
        public IEnumerable<T> Items { get; }
    }
}
