using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graphs.Elements
{
    [DebuggerDisplay("{Id}")]
    [JsonObject("node")]
    public sealed class Node
        : Element<Guid>
        , IElement<Guid>
        , IEquatable<Node>
        , IEqualityComparer<Node>
    {
        [JsonProperty("nodesAndEdges")]
        private readonly AdjacencyAndIncidenceIndex nodesAndEdges = AdjacencyAndIncidenceIndex.Empty;

        // todo: raise events on coupled and decoupled 

        public static Node New => new(Guid.NewGuid());

        [Pure]
        [JsonIgnore]
        public int Degree => this.nodesAndEdges.NodeCount;

        [Pure]
        [JsonIgnore]
        public IEnumerable<Guid> Neighbors => this.nodesAndEdges.Nodes;

        [Pure]
        [JsonIgnore]
        public IEnumerable<Guid> IncidentEdges => this.nodesAndEdges.Edges;

        [JsonConstructor]
        private Node(Guid id)
            : base(id)
        { }

        private Node([DisallowNull, Pure] Node other)
            : base(other)
        {
            this.nodesAndEdges = other.nodesAndEdges.Clone() as AdjacencyAndIncidenceIndex;
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

        [Pure]
        public bool IsAdjacent(Guid nodeId)
        {
            return this.nodesAndEdges.ContainsNode(nodeId);
        }

        [Pure]
        public bool IsAdjacent([DisallowNull, Pure] Node node)
        {
            return node is null
                ? throw new ArgumentNullException(nameof(node))
                : this.IsAdjacent(node.Id);
        }

        [Pure]
        public bool IsIncident(Guid edgeId)
        {
            return this.nodesAndEdges.ContainsEdge(edgeId);
        }

        [Pure]
        public bool IsIncident([DisallowNull, Pure] Edge edge)
        {
            return edge is null
                ? throw new ArgumentNullException(nameof(edge))
                : this.IsIncident(edge.Id);
        }

        /// <summary>
        /// Couples the nodes and returns the resulting edge.
        /// </summary>
        /// <param name="source"><see cref="Node"/></param>
        /// <param name="target"><see cref="Node"/></param>
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

            if (edge.SourceId != this.Id && edge.TargetId != this.Id)
            {
                throw new InvalidOperationException($"{nameof(edge)} with id '{edge.Id}'does not point to a node with id {this.Id}.");
            }

            var otherNodeId = edge.SourceId == this.Id
                ? edge.TargetId
                : edge.SourceId;

            this.nodesAndEdges.Add(edge.Id, otherNodeId);
        }

        internal void Decouple([DisallowNull, Pure] Edge edge)
        {
            if (edge is null)
            {
                throw new ArgumentNullException(nameof(edge));
            }

            if (edge.SourceId != this.Id && edge.TargetId != this.Id)
            {
                throw new InvalidOperationException($"{nameof(edge)} with id '{edge.Id}' does not point to node with id '{this.Id}'.");
            }

            var otherNodeId = edge.SourceId == this.Id
                ? edge.TargetId
                : edge.SourceId;

            this.nodesAndEdges.Remove(edge.Id, otherNodeId);
        }
    }
}
