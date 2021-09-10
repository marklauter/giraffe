using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Elements
{
    /// <inheritdoc/>
    [DebuggerDisplay("{Id}")]
    [JsonObject("node")]
    public sealed class Node
        : Element<Guid>
        , INode
    {
        // todo: to index by label replace neighbors with a hash of IClass. when coupling the target node gets added to the appropriate set of classes.

        [JsonProperty("neighbors")]
        private ImmutableHashSet<Guid> neighbors = ImmutableHashSet<Guid>.Empty;

        public static Node New => new(Guid.NewGuid());

        [JsonConstructor]
        private Node(Guid id)
            : base(id)
        { }

        private Node([DisallowNull, Pure] Node other)
            : base(other)
        {
            this.neighbors = other.neighbors;
        }


        /// <inheritdoc/>
        [Pure]
        [JsonIgnore]
        public int Degree => this.neighbors.Count;

        /// <inheritdoc/>
        [Pure]
        [JsonIgnore]
        public IEnumerable<Guid> Neighbors => this.neighbors;

        /// <inheritdoc/>
        [Pure]
        public bool IsAdjacent(Guid nodeId)
        {
            return this.neighbors.Contains(nodeId);
        }

        /// <inheritdoc/>
        [Pure]
        public bool IsAdjacent([DisallowNull, Pure] INode node)
        {
            return node is null
                ? throw new ArgumentNullException(nameof(node))
                : this.IsAdjacent(node.Id);
        }

        /// <inheritdoc/>
        [Pure]
        public override object Clone()
        {
            return new Node(this);
        }

        /// <inheritdoc/>
        public bool TryCouple([DisallowNull, Pure] INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (!this.neighbors.Contains(node.Id))
            {
                this.neighbors = this.neighbors
                    .Add(node.Id);

                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool TryDecouple([DisallowNull, Pure] INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (this.neighbors.Contains(node.Id))
            {
                this.neighbors = this.neighbors
                    .Remove(node.Id);

                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        [Pure]
        public bool Equals([Pure] INode other)
        {
            return other != null
                && other.Id == this.Id;
        }

        /// <inheritdoc/>
        [Pure]
        public bool Equals([Pure] INode x, [Pure] INode y)
        {
            return x != null && x.Equals(y);
        }

        /// <inheritdoc/>
        [Pure]
        public override bool Equals([Pure] object obj)
        {
            return obj is INode node && this.Equals(node);
        }

        /// <inheritdoc/>
        [Pure]
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }

        /// <inheritdoc/>
        [Pure]
        public int GetHashCode([DisallowNull, Pure] INode obj)
        {
            return obj is null
                ? throw new ArgumentNullException(nameof(obj))
                : obj.GetHashCode();
        }
    }
}
