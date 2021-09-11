using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Classifiers
{
    /// <inheritdoc/>
    [JsonArray]
    internal sealed class ClassificationCollection
        : IClassifiable
        , IEnumerable<string>
    {
        /// <inheritdoc/>
        public event EventHandler<ClassifiedEventArgs> Classified;

        /// <inheritdoc/>
        public event EventHandler<DeclassifiedEventArgs> Declassified;

        private ImmutableHashSet<string> classes = ImmutableHashSet<string>.Empty;

        public static ClassificationCollection Empty => new();

        private ClassificationCollection() { }

        private ClassificationCollection([DisallowNull, Pure] ClassificationCollection other)
        {
            this.classes = other.classes;
        }

        [JsonConstructor]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used for serialization.")]
        private ClassificationCollection([DisallowNull, Pure] IEnumerable<string> labels)
        {
            this.classes = this.classes.Union(labels);
        }

        /// <inheritdoc/>
        public IClassifiable Classify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.classes = this.classes.Add(label);
            Classified?.Invoke(this, new ClassifiedEventArgs(label));

            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public object Clone()
        {
            return new ClassificationCollection(this);
        }

        /// <inheritdoc/>
        public IClassifiable Declassify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.classes = this.classes.Remove(label);
            Declassified?.Invoke(this, new DeclassifiedEventArgs(label));

            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public bool Is(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            return this.classes.Contains(label);
        }

        /// <inheritdoc/>
        [Pure]
        public bool Is([DisallowNull] IEnumerable<string> labels)
        {
            if (labels is null)
            {
                throw new ArgumentNullException(nameof(labels));
            }

            return this.classes.IsSupersetOf(labels);
        }

        /// <inheritdoc/>
        public IEnumerator<string> GetEnumerator()
        {
            foreach (var item in this.classes)
            {
                yield return item;
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
