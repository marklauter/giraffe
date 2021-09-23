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
    public sealed class Node<TId>
        : Element<TId>
        , INode<TId>
        , ICoupledEventSource<TId>
        , IEquatable<Node<TId>>
        , IEqualityComparer<Node<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [JsonProperty("nodesAndEdges")]
        private readonly AdjacencyAndIncidenceIndex<TId> nodesAndEdges = AdjacencyAndIncidenceIndex<TId>.Empty;

        public event EventHandler<CoupledEventArgs<TId>> Coupled;
        public event EventHandler<DecoupledEventArgs<TId>> Decoupled;

        internal static Node<TId> New(IIdGenerator<TId> idGenerator)
        {
            return idGenerator is null 
                ? throw new ArgumentNullException(nameof(idGenerator)) 
                : (new(idGenerator.NewId()));
        }

        [Pure]
        [JsonIgnore]
        public int Degree => this.nodesAndEdges.NodeCount;

        [Pure]
        [JsonIgnore]
        public IEnumerable<TId> Neighbors => this.nodesAndEdges.Nodes;

        [Pure]
        [JsonIgnore]
        public IEnumerable<TId> IncidentEdges => this.nodesAndEdges.Edges;

        [JsonConstructor]
        private Node(TId id)
            : base(id)
        { }

        private Node([DisallowNull, Pure] Node<TId> other)
            : base(other)
        {
            this.nodesAndEdges = other.nodesAndEdges.Clone() as AdjacencyAndIncidenceIndex<TId>;
        }

        [Pure]
        public override object Clone()
        {
            return new Node<TId>(this);
        }

        [Pure]
        public bool IsAdjacent(TId targetId)
        {
            return this.nodesAndEdges.ContainsNode(targetId);
        }

        [Pure]
        public bool IsAdjacent([DisallowNull, Pure] Node<TId> node)
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
        public bool IsIncident([DisallowNull, Pure] Edge<TId> edge)
        {
            return edge is null
                ? throw new ArgumentNullException(nameof(edge))
                : this.IsIncident(edge.Id);
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

        internal void Couple([DisallowNull, Pure] Edge<TId> edge)
        {
            if (edge is null)
            {
                throw new ArgumentNullException(nameof(edge));
            }

            if (!this.Id.Equals(edge.SourceId)  && !this.Id.Equals(edge.TargetId))
            {
                throw new InvalidOperationException($"{nameof(edge)} with id '{edge.Id}'does not point to a node with id {this.Id}.");
            }

            var otherNodeId = this.Id.Equals(edge.SourceId)
                ? edge.TargetId
                : edge.SourceId;

            this.nodesAndEdges.Add(edge.Id, otherNodeId);
            this.Coupled?.Invoke(this, new CoupledEventArgs<TId>(this, edge));
        }

        internal void Decouple([DisallowNull, Pure] Edge<TId> edge)
        {
            if (edge is null)
            {
                throw new ArgumentNullException(nameof(edge));
            }

            if (!this.Id.Equals(edge.SourceId) && !this.Id.Equals(edge.TargetId))
            {
                throw new InvalidOperationException($"{nameof(edge)} with id '{edge.Id}' does not point to node with id '{this.Id}'.");
            }

            var otherNodeId = this.Id.Equals(edge.SourceId)
                ? edge.TargetId
                : edge.SourceId;

            this.nodesAndEdges.Remove(edge.Id, otherNodeId);
            this.Decoupled?.Invoke(this, new DecoupledEventArgs<TId>(this, edge));
        }
    }
}
