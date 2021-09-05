using Graph.ConcurrentCollections;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        private ImmutableDictionary<string, string> attributes = ImmutableDictionary<string, string>.Empty;

        protected Element() { }

        protected Element(Guid id)
        {
            this.Id = id;
        }

        protected Element([DisallowNull] Element other)
        {
            this.Id = other.Id;
            this.attributes = other.attributes;
        }

        /// <inheritdoc/>
        [Key]
        [Required]
        [JsonProperty("id")]
        public Guid Id { get; } 

        /// <inheritdoc/>
        [Pure]
        public abstract object Clone();

        /// <inheritdoc/>
        [Pure]
        public bool HasAttribute(string name)
        {
            return this.attributes.ContainsKey(name);
        }

        /// <inheritdoc/>
        public IElement Qualify(string name, string value)
        {

            this.attributes = this.attributes
                .SetItem(name, value);
            return this;
        }

        /// <inheritdoc/>
        public IElement Qualify([DisallowNull] IEnumerable<KeyValuePair<string, string>> attributes)
        {
            this.attributes = this.attributes
                .AddRange(attributes);

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
