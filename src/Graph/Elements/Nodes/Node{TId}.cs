using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Graphs.Elements
{
    [DebuggerDisplay("{Id}")]
    public sealed class Node<TId>
        : Element<TId>
        , IEquatable<Node<TId>>
        , IEqualityComparer<Node<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly AdjacencyAndIncidenceIndex<TId> nodesAndEdges = AdjacencyAndIncidenceIndex<TId>.Empty;

        internal static Node<TId> New(TId id)
        {
            return new Node<TId>(id);
        }


        public int Degree => this.nodesAndEdges.NodeCount;


        public IEnumerable<TId> Neighbors => this.nodesAndEdges.Nodes;

        public IEnumerable<KeyValuePair<TId, int>> ReferenceCountedNodes => this.nodesAndEdges.ReferenceCountedNodes;


        public IEnumerable<TId> Edges => this.nodesAndEdges.Edges;

        private Node(TId id)
            : base(id)
        { }

        private Node(Node<TId> other)
            : base(other)
        {
            this.nodesAndEdges = other.nodesAndEdges.Clone() as AdjacencyAndIncidenceIndex<TId>;
        }

        public Node(
            TId id,
            IEnumerable<string> classifications,
            IEnumerable<KeyValuePair<string, object>> qualifications,
            IEnumerable<KeyValuePair<TId, int>> nodes,
            IEnumerable<TId> edges)
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


        public override object Clone()
        {
            return new Node<TId>(this);
        }

        public INode<TId> Connect(IEdge<TId> edge)
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

        public INode<TId> Disconnect(IEdge<TId> edge)
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


        public bool IsAdjacent(TId targetId)
        {
            return this.nodesAndEdges.ContainsNode(targetId);
        }


        public bool IsAdjacent(INode<TId> node)
        {
            return node is null
                ? throw new ArgumentNullException(nameof(node))
                : this.IsAdjacent(node.Id);
        }


        public bool IsIncident(TId edgeId)
        {
            return this.nodesAndEdges.ContainsEdge(edgeId);
        }


        public bool IsIncident(IEdge<TId> edge)
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


        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }


        public int GetHashCode(Node<TId> obj)
        {
            return obj is null
                ? throw new ArgumentNullException(nameof(obj))
                : obj.GetHashCode();
        }
    }
}
