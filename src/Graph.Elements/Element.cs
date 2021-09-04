using Graph.ConcurrentCollections;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Elements
{
    [DebuggerDisplay("{Id}")]
    public abstract class Element
        : IElement
    {
        [JsonProperty("attributes")]
        private readonly ConcurrentDictionary<string, string> attributes = new();

        [JsonProperty("labels")]
        private readonly ConcurrentHashSet<string> labels = new();

        protected Element() { }

        protected Element([DisallowNull] Element other)
        {
            this.Id = other.Id;
            this.attributes = new(other.attributes);
            this.labels = other.labels.Clone() as ConcurrentHashSet<string>;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        /// <inheritdoc/>
        [Key]
        [Required]
        [JsonProperty("id")]
        public Guid Id { get; } = Guid.NewGuid();

        [JsonIgnore]
        public IEnumerable<string> Labels => this.labels;

        /// <inheritdoc/>
        public IElement Classify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            if (this.labels.Add(label))
            {
                this.OnClassificationChanged(label);
            }

            return this;
        }

        /// <inheritdoc/>
        public IElement Classify([DisallowNull] IEnumerable<string> labels)
        {
            foreach (var label in labels)
            {
                _ = this.Classify(label);
            }

            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public abstract object Clone();

        /// <inheritdoc/>
        public IElement Declassify(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            if (this.labels.Remove(label))
            {
                this.OnClassificationChanged(label);
            }

            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public bool HasAttribute(string name)
        {
            return this.attributes.ContainsKey(name);
        }

        /// <inheritdoc/>
        [Pure]
        public bool Is(string label)
        {
            return this.labels.Contains(label);
        }

        /// <inheritdoc/>
        public IElement Qualify(string name, string value)
        {
            _ = this.attributes.AddOrUpdate(name, value, (key, oldvalue) => value);
            return this;
        }

        /// <inheritdoc/>
        public IElement Qualify([DisallowNull] IEnumerable<KeyValuePair<string, string>> attributes)
        {
            foreach (var kvp in attributes)
            {
                this.Qualify(kvp.Key, kvp.Value);
            }

            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public bool TryGetAttribute(string name, out string value)
        {
            return this.attributes.TryGetValue(name, out value);
        }

        private void OnClassificationChanged(string label)
        {
            ClassificationChanged?.Invoke(this, new ClassificationChangedEventArgs(label));
        }
    }
}
