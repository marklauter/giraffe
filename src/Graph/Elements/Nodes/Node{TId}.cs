using Graphs.Elements.Edges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graphs.Elements.Nodes
{
    [DebuggerDisplay("{Id}")]
    public sealed class Node<TId>
        : Element<TId>
        , INode<TId>
        , IEquatable<Node<TId>>
        , IEqualityComparer<Node<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly AdjacencyAndIncidenceIndex<TId> nodesAndEdges = AdjacencyAndIncidenceIndex<TId>.Empty;

        internal static INode<TId> New(TId id)
        {
            return new Node<TId>(id);
        }

        [Pure]
        public int Degree => this.nodesAndEdges.NodeCount;

        [Pure]
        public IEnumerable<TId> Neighbors => this.nodesAndEdges.Nodes;

        public IEnumerable<KeyValuePair<TId, int>> ReferenceCountedNodes => this.nodesAndEdges.ReferenceCountedNodes;

        [Pure]
        public IEnumerable<TId> Edges => this.nodesAndEdges.Edges;

        private Node(TId id)
            : base(id)
        { }

        private Node([DisallowNull, Pure] Node<TId> other)
            : base(other)
        {
            this.nodesAndEdges = other.nodesAndEdges.Clone() as AdjacencyAndIncidenceIndex<TId>;
        }

        public Node(
            TId id,
            [DisallowNull, Pure] IEnumerable<string> classifications,
            [DisallowNull, Pure] IEnumerable<KeyValuePair<string, object>> qualifications,
            [DisallowNull, Pure] IEnumerable<KeyValuePair<TId, int>> nodes,
            [DisallowNull, Pure] IEnumerable<TId> edges)
            : base(id, classifications, qualifications)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            if (edges is null)
            {
                throw new ArgumentNullException(nameof(edges));
            }

            this.nodesAndEdges = new AdjacencyAndIncidenceIndex<TId>(nodes, edges);
        }

        [Pure]
        public override object Clone()
        {
            return new Node<TId>(this);
        }

        public INode<TId> Connect([DisallowNull, Pure] IEdge<TId> edge)
        {
            if (!this.IsIncident(edge))
            {
                throw new InvalidOperationException($"{nameof(edge)} with id '{edge.Id}' is not incident to node with id {this.Id}.");
            }

            var otherNodeId = this.Id.Equals(edge.SourceId)
                ? edge.TargetId
                : edge.SourceId;

            this.nodesAndEdges.Add(edge.Id, otherNodeId);
            return this;
        }

        public INode<TId> Disconnect([DisallowNull, Pure] IEdge<TId> edge)
        {
            if (!this.IsIncident(edge))
            {
                throw new InvalidOperationException($"{nameof(edge)} with id '{edge.Id}' is not incident to node with id {this.Id}.");
            }

            var otherNodeId = this.Id.Equals(edge.SourceId)
                ? edge.TargetId
                : edge.SourceId;

            this.nodesAndEdges.Remove(edge.Id, otherNodeId);
            return this;
        }

        [Pure]
        public bool IsAdjacent(TId targetId)
        {
            return this.nodesAndEdges.ContainsNode(targetId);
        }

        [Pure]
        public bool IsAdjacent([DisallowNull, Pure] INode<TId> node)
        {
            return node is null
                ? throw new ArgumentNullException(nameof(node))
                : this.IsAdjacent(node.Id);
        }

        [Pure]
        public bool IsIncident(TId edgeId)
        {
            return this.nodesAndEdges.ContainsEdge(edgeId);
        }

        [Pure]
        public bool IsIncident([DisallowNull, Pure] IEdge<TId> edge)
        {
            return edge is null
                ? throw new ArgumentNullException(nameof(edge))
                : this.Id.Equals(edge.SourceId) || this.Id.Equals(edge.TargetId) || this.IsIncident(edge.Id);
        }

        [Pure]
        public override bool Equals([Pure] object obj)
        {
            return obj is Node<TId> node
                && this.Equals(node);
        }

        [Pure]
        public bool Equals([Pure] Node<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        [Pure]
        public bool Equals([Pure] Node<TId> x, [Pure] Node<TId> y)
        {
            return x != null && x.Equals(y);
        }

        [Pure]
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }

        [Pure]
        public int GetHashCode([DisallowNull, Pure] Node<TId> obj)
        {
            return obj is null
                ? throw new ArgumentNullException(nameof(obj))
                : obj.GetHashCode();
        }
    }
}
