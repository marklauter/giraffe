using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Graph.Elements
{
    [DebuggerDisplay("{Id}")]
    [JsonObject("node")]
    public sealed class Node
        : Element<Guid>
        , IElement<Guid>
        , IEquatable<Node>
        , IEqualityComparer<Node>
    {
        // todo: to index by label replace neighbors with a dictionary of IClass mapped by label. when coupling the target node gets added to the appropriate set of classes.

        [JsonProperty("adjacentNodes")]
        private ImmutableHashSet<Guid> adjacentNodes = ImmutableHashSet<Guid>.Empty;

        [JsonProperty("incidentEdges")]
        private ImmutableHashSet<Guid> incidentEdges = ImmutableHashSet<Guid>.Empty;

        public static Node New => new(Guid.NewGuid());

        [JsonConstructor]
        private Node(Guid id)
            : base(id)
        { }

        private Node([DisallowNull, Pure] Node other)
            : base(other)
        {
            this.adjacentNodes = other.adjacentNodes;
            this.incidentEdges = other.incidentEdges;
        }

        [Pure]
        [JsonIgnore]
        public int Degree => this.adjacentNodes.Count;

        [Pure]
        [JsonIgnore]
        public IEnumerable<Guid> Neighbors => this.adjacentNodes;

        [Pure]
        public bool IsAdjacent(Guid nodeId)
        {
            return this.adjacentNodes.Contains(nodeId);
        }

        [Pure]
        public bool IsAdjacent([DisallowNull, Pure] Node node)
        {
            return node is null
                ? throw new ArgumentNullException(nameof(node))
                : this.IsAdjacent(node.Id);
        }

        [Pure]
        public override object Clone()
        {
            return new Node(this);
        }

        [Pure]
        public bool Equals([Pure] Node other)
        {
            return other != null
                && other.Id == this.Id;
        }

        [Pure]
        public bool Equals([Pure] Node x, [Pure] Node y)
        {
            return x != null && x.Equals(y);
        }

        [Pure]
        public override bool Equals([Pure] object obj)
        {
            return obj is Node node
                && this.Equals(node);
        }

        [Pure]
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }

        [Pure]
        public int GetHashCode([DisallowNull, Pure] Node obj)
        {
            return obj is null
                ? throw new ArgumentNullException(nameof(obj))
                : obj.GetHashCode();
        }

        /// <summary>
        /// Couples the nodes.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns><see cref="Edge"/></returns>
        public static Edge operator +(Node source, Node target)
        {
            return Edge.Couple(source, target);
        }

        internal void Couple([DisallowNull, Pure] Edge edge)
        {
            if (edge is null)
            {
                throw new ArgumentNullException(nameof(edge));
            }

            var targetId = edge.Nodes
                .Single(id => id != this.Id);

            this.adjacentNodes = this.adjacentNodes
                .Add(targetId);

            this.incidentEdges = this.incidentEdges
                .Add(edge.Id);
        }

        internal void Decouple([DisallowNull, Pure] Edge edge)
        {
            if (edge is null)
            {
                throw new ArgumentNullException(nameof(edge));
            }

            var targetId = edge.Nodes
                .Single(id => id != this.Id);

            this.adjacentNodes = this.adjacentNodes
                .Remove(targetId);

            this.incidentEdges = this.incidentEdges
                .Remove(edge.Id);
        }
    }
}
