using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Graphs.Elements
{
    [DebuggerDisplay("{Id}, Deg: {Degree}")]
    public sealed class Node<TId>
        : Element<TId>
        , IEquatable<Node<TId>>
        , IEqualityComparer<Node<TId>>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly AdjacencyIndex<TId> adjacencyIndex = AdjacencyIndex<TId>.Empty;

        internal static Node<TId> NewNode(TId id)
        {
            return new Node<TId>(id);
        }

        public int Degree => this.adjacencyIndex.NodeCount;

        public IEnumerable<TId> Edges => this.adjacencyIndex.Edges;

        public int EdgeCount => this.adjacencyIndex.EdgeCount;

        public IEnumerable<TId> Neighbors => this.adjacencyIndex.Nodes;

        public IEnumerable<KeyValuePair<TId, int>> ReferenceCountedNodes => this.adjacencyIndex.ReferenceCountedNodes;

        private Node(TId id)
            : base(id)
        { }

        private Node(Node<TId> other)
            : base(other)
        {
            this.adjacencyIndex = other.adjacencyIndex.Clone() as AdjacencyIndex<TId>;
        }

        public Node(
            TId id,
            IEnumerable<string> labels,
            IEnumerable<KeyValuePair<string, object>> attributes,
            IEnumerable<TId> edges,
            IEnumerable<KeyValuePair<TId, int>> nodes)
            : base(id, labels, attributes)
        {
            if (edges is null)
            {
                throw new ArgumentNullException(nameof(edges));
            }

            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            this.adjacencyIndex = new AdjacencyIndex<TId>(edges, nodes);
        }

        public override object Clone()
        {
            return new Node<TId>(this);
        }

        internal void Connect(Edge<TId> edge)
        {
            if (!this.IsIncident(edge))
            {
                throw new InvalidOperationException($"{nameof(edge)} with id '{edge.Id}' is not incident to node with id {this.Id}.");
            }

            var otherNodeId = this.Id.Equals(edge.SourceId)
                ? edge.TargetId
                : edge.SourceId;

            this.adjacencyIndex.Add(edge.Id, otherNodeId);
        }

        internal void Disconnect(Edge<TId> edge)
        {
            if (!this.IsIncident(edge))
            {
                throw new InvalidOperationException($"{nameof(edge)} with id '{edge.Id}' is not incident to node with id {this.Id}.");
            }

            var otherNodeId = this.Id.Equals(edge.SourceId)
                ? edge.TargetId
                : edge.SourceId;

            this.adjacencyIndex.Remove(edge.Id, otherNodeId);
        }

        public bool IsAdjacent(TId targetId)
        {
            return this.adjacencyIndex.ContainsNode(targetId);
        }

        public bool IsAdjacent(Node<TId> node)
        {
            return node is null
                ? throw new ArgumentNullException(nameof(node))
                : this.IsAdjacent(node.Id);
        }

        public bool IsIncident(TId edgeId)
        {
            return this.adjacencyIndex.ContainsEdge(edgeId);
        }

        public bool IsIncident(Edge<TId> edge)
        {
            return edge is null
                ? throw new ArgumentNullException(nameof(edge))
                : this.Id.Equals(edge.SourceId) || this.Id.Equals(edge.TargetId) || this.IsIncident(edge.Id);
        }

        public override bool Equals(object obj)
        {
            return obj is Node<TId> node
                && this.Equals(node);
        }

        public bool Equals(Node<TId> other)
        {
            return other != null
                && other.Id.Equals(this.Id);
        }

        public bool Equals(Node<TId> x, Node<TId> y)
        {
            return x != null && x.Equals(y);
        }

        public int GetHashCode(Node<TId> obj)
        {
            return obj is null
                ? throw new ArgumentNullException(nameof(obj))
                : obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }
    }
}
