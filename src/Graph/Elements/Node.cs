using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Elements
{
    [DebuggerDisplay("{Key}")]
    [JsonObject("node")]
    public sealed class Node
        : Element<Guid>
        , IEquatable<Node>
        , IEqualityComparer<Node>
    {
        [JsonProperty("neigbors")]
        private ImmutableHashSet<Guid> neighbors = ImmutableHashSet<Guid>.Empty;

        public static Node New => new(Guid.NewGuid());

        private Node() : base() { }

        private Node(Guid id) : base(id) { }

        private Node([DisallowNull, Pure] Node other)
            : base(other)
        {
            this.neighbors = other.neighbors;
        }

        [JsonConstructor]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used for serialization.")]
        private Node(Guid id, IEnumerable<Guid> neighbors)
            : base(id)
        {
            this.neighbors = this.neighbors.Union(neighbors);
        }

        [Pure]
        public bool IsAdjacent(Guid nodeId)
        {
            return this.neighbors.Contains(nodeId);
        }

        [Pure]
        public bool IsAdjacent([DisallowNull, Pure] Node node)
        {
            return this.IsAdjacent(node.Id);
        }

        [Pure]
        public override object Clone()
        {
            return new Node(this);
        }

        public bool TryCouple([DisallowNull, Pure] Node node)
        {
            if (!this.neighbors.Contains(node.Id))
            {
                this.neighbors = this.neighbors
                    .Add(node.Id);

                return true;
            }

            return false;
        }

        public bool TryDecouple([DisallowNull, Pure] Node node)
        {
            if (this.neighbors.Contains(node.Id))
            {
                this.neighbors = this.neighbors
                    .Remove(node.Id);

                return true;
            }

            return false;
        }

        [Pure]
        public int Degree()
        {
            return this.neighbors.Count;
        }

        [Pure]
        public bool Equals([Pure] Node other)
        {
            return other != null
                && other.Id == this.Id;
        }

        [Pure]
        public bool Equals(Node x, Node y)
        {
            return x != null && x.Equals(y);
        }

        [Pure]
        public override bool Equals(object obj)
        {
            return obj is Node node && this.Equals(node);
        }

        [Pure]
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }

        [Pure]
        public int GetHashCode([DisallowNull, Pure] Node obj)
        {
            return obj.GetHashCode();
        }

        [Pure]
        public IEnumerable<Guid> Neighbors()
        {
            return this.neighbors;
        }
    }
}
