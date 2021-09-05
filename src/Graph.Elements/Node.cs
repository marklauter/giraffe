using Graph.ConcurrentCollections;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;

namespace Graph.Elements
{
    [DebuggerDisplay("{Key}")]
    [JsonObject("node")]
    public sealed class Node
        : Element
        , IEquatable<Node>
        , IEqualityComparer<Node>
    {
        [JsonProperty]
        private readonly ConcurrentHashSet<Guid> neighbors = new();

        [JsonProperty]
        private readonly ElementIndex nodeIndex = ElementIndex.Empty;

        // indexed by target node
        [JsonProperty]
        private readonly ConcurrentDictionary<Guid, Guid> edges = new();

        private Node() : base() { }

        private Node([DisallowNull] Node other)
            : base(other)
        {
            this.neighbors = other.neighbors.Clone() as ConcurrentHashSet<Guid>;
            this.edges = new ConcurrentDictionary<Guid, Guid>(other.edges);
            this.nodeIndex = other.nodeIndex.Clone() as ElementIndex;
        }

        [Pure]
        public bool IsAdjacent(Guid targetId)
        {
            return this.neighbors.Contains(targetId);
        }

        [Pure]
        public bool IsAdjacent(Node target)
        {
            return this.IsAdjacent(target.Id);
        }

        [Pure]
        public override object Clone()
        {
            return new Node(this);
        }

        public bool TryCouple([DisallowNull] Node target, bool isDirected, out Edge edge)
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            edge = null;
            if (this.neighbors.Add(target.Id))
            {
                edge = new Edge(this, target);
                _ = this.edges.TryAdd(target.Id, edge.Id);

                if (!isDirected && target.neighbors.Add(target.Id))
                {
                    _ = target.edges.TryAdd(this.Id, edge.Id);
                }

                return true;
            }

            return false;
        }

        public bool TryDecouple([DisallowNull] Node target)
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            return this.neighbors.Remove(target.Id);
        }

        [Pure]
        public int Degree()
        {
            return this.neighbors.Count;
        }

        [Pure]
        public bool Equals(Node other)
        {
            return other != null
                && other.Id == this.Id;
        }

        [Pure]
        public bool Equals(Node x, Node y)
        {
            return x != null && x.Equals(y) || y == null;
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
        public int GetHashCode([DisallowNull] Node obj)
        {
            return obj.GetHashCode();
        }

        [Pure]
        public IEnumerable<Guid> Neighbors()
        {
            foreach (var neighbor in this.neighbors)
            {
                yield return neighbor;
            }
        }
    }
}
