using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Elements
{
    [JsonObject]
    public sealed class Classifier<T>
        : IClassifier<T>
    {
        [JsonProperty]
        private ImmutableDictionary<string, ImmutableHashSet<T>> classes = ImmutableDictionary<string, ImmutableHashSet<T>>.Empty;

        /// <inheritdoc/>
        public event EventHandler<ItemsClassifiedEventArgs<T>> ItemsClassified;

        /// <inheritdoc/>
        public event EventHandler<ItemsDeclassifiedEventArgs<T>> ItemsDeclassified;

        private Classifier() { }

        private Classifier([DisallowNull, Pure] Classifier<T> other)
        {
            this.classes = other.classes;
        }

        /// <summary>
        /// Returns an empty classifier.
        /// </summary>
        public static IClassifier<T> Empty => new Classifier<T>();

        /// <inheritdoc/>
        public IClassifier<T> Classify(string label, T item)
        {
            var hashset = this.classes
                .GetValueOrDefault(label, ImmutableHashSet<T>.Empty)
                .Add(item);

            this.classes = this.classes
                .SetItem(label, hashset);

            ItemsClassified?.Invoke(this, new ItemsClassifiedEventArgs<T>(label, item));

            return this;
        }

        /// <inheritdoc/>
        public IClassifier<T> Classify(string label, IEnumerable<T> items)
        {
            var hashset = this.classes
                .GetValueOrDefault(label, ImmutableHashSet<T>.Empty)
                .Union(items);

            this.classes = this.classes
                .SetItem(label, hashset);

            ItemsClassified?.Invoke(this, new ItemsClassifiedEventArgs<T>(label, items));

            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public object Clone()
        {
            return new Classifier<T>(this);
        }

        /// <inheritdoc/>
        public IClassifier<T> Declassify(string label, T item)
        {
            var hashset = this.classes
                .GetValueOrDefault(label, ImmutableHashSet<T>.Empty)
                .Remove(item);

            this.classes = this.classes
                .SetItem(label, hashset);

            ItemsDeclassified?.Invoke(this, new ItemsDeclassifiedEventArgs<T>(label, item));

            return this;
        }

        /// <inheritdoc/>
        public IClassifier<T> Declassify(string label, IEnumerable<T> items)
        {
            var hashset = this.classes
                .GetValueOrDefault(label, ImmutableHashSet<T>.Empty)
                .Except(items);

            this.classes = this.classes
                .SetItem(label, hashset);

            ItemsDeclassified?.Invoke(this, new ItemsDeclassifiedEventArgs<T>(label, items));

            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public bool Exists(string label)
        {
            return this.classes.ContainsKey(label);
        }

        /// <inheritdoc/>
        [Pure]
        public ImmutableHashSet<T> Members(string label)
        {
            return this.classes.GetValueOrDefault(label, ImmutableHashSet<T>.Empty);
        }

        /// <inheritdoc/>
        [Pure]
        public bool Is(string label, T item)
        {
            return this.classes
                .GetValueOrDefault(label, ImmutableHashSet<T>.Empty)
                .Contains(item);
        }
    }
}
