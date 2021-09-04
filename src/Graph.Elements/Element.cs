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
    public class Element
        : IElement
        , IEquatable<Element>
    {
        [JsonProperty("attributes")]
        private readonly ConcurrentDictionary<string, string> attributes = new();

        [JsonProperty("labels")]
        private readonly ConcurrentHashSet<string> labels = new();

        public Element() { }

        protected Element([DisallowNull] Element other)
        {
            this.Id = other.Id;
            this.attributes = new(other.attributes);
            this.labels = other.labels.Clone() as ConcurrentHashSet<string>;
        }

        /// <inheritdoc/>
        [Key]
        [Required]
        [JsonProperty("id")]
        public Guid Id { get; } = Guid.NewGuid();

        public IEnumerable<string> Labels => this.labels;

        /// <inheritdoc/>
        public IElement Classify(string label)
        {
            this.labels.Add(label);
            return this;
        }

        /// <inheritdoc/>
        public IElement Classify([DisallowNull] IEnumerable<string> labels)
        {
            this.labels.UnionWith(labels);
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public virtual object Clone()
        {
            return new Element(this);
        }

        /// <inheritdoc/>
        public IElement Declassify(string label)
        {
            this.labels.Remove(label);
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public bool Equals(Element other)
        {
            return other != null
                && other.Id == this.Id;
        }
        
        /// <inheritdoc/>
        [Pure]
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Element);
        }

        /// <inheritdoc/>
        [Pure]
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
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
            this.attributes.AddOrUpdate(name, value, (key, oldvalue) => value);
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
    }
}
