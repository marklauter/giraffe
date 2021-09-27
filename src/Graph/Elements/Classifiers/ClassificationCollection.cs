using Collections.Concurrent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graphs.Elements.Classifiers
{
    /// <inheritdoc/>
    internal sealed class ClassificationCollection
        : IClassifier
        , IEnumerable<string>
    {
        /// <inheritdoc/>
        public event EventHandler<ClassifiedEventArgs> Classified;

        /// <inheritdoc/>
        public event EventHandler<DeclassifiedEventArgs> Declassified;

        private readonly ConcurrentHashSet<string> classes;

        public static ClassificationCollection Empty => new();

        private ClassificationCollection()
        {
            this.classes = ConcurrentHashSet<string>.Empty;
        }

        private ClassificationCollection([DisallowNull, Pure] ClassificationCollection other)
        {
            this.classes = new(other.classes);
        }

        internal ClassificationCollection([DisallowNull, Pure] IEnumerable<string> labels)
        {
            this.classes = new(labels);
        }

        /// <inheritdoc/>
        public IClassifier Classify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.classes.Add(label);
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
        public IClassifier Declassify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.classes.Remove(label);
            Declassified?.Invoke(this, new DeclassifiedEventArgs(label));

            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public bool Is(string label)
        {
            return String.IsNullOrWhiteSpace(label)
                ? throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label))
                : this.classes.Contains(label);
        }

        /// <inheritdoc/>
        [Pure]
        public bool Is([DisallowNull] IEnumerable<string> labels)
        {
            return labels is null
                ? throw new ArgumentNullException(nameof(labels))
                : this.classes.IsSupersetOf(labels);
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
