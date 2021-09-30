using Graphs.Elements.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Graphs.Elements
{
    [DebuggerDisplay("{SourceId} : {TargetId}")]
    public sealed class Edge<TId>
        : Element<TId>
        , IEquatable<Edge<TId>>
        , IEqualityComparer<Edge<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {

        public bool IsDirected { get; }


        public IEnumerable<TId> Nodes
        {
            get
            {
                yield return this.SourceId;
                yield return this.TargetId;
            }
        }

        public TId SourceId { get; }

        public TId TargetId { get; }

        private Edge(TId id, TId sourceId, TId targetId, bool isDirected)
            : base(id)
        {
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.IsDirected = isDirected;
        }

        private Edge(Edge<TId> other)
            : base(other)
        {
            this.SourceId = other.SourceId;
            this.TargetId = other.TargetId;
            this.IsDirected = other.IsDirected;
        }

        public Edge(
            TId id,
            IEnumerable<string> classifications,
            IEnumerable<KeyValuePair<string, object>> qualifications,
            TId sourceId,
            TId targetId,
            bool isDirected)
            : base(id, classifications, qualifications)
        {
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.IsDirected = isDirected;
        }


        public override object Clone()
        {
            return new Edge<TId>(this);
        }

        public IEdge<TId> Disconnect(INode<TId> node1, INode<TId> node2)
        {
            if (!this.IsIncident(node1))
            {
                throw new InvalidOperationException($"{nameof(Node<TId>)} with id '{node1.Id}' is not incident to edge with id '{this.Id}'.");
            }

            if (!this.IsIncident(node2))
            {
                throw new InvalidOperationException($"{nameof(Node<TId>)} with id '{node2.Id}' is not incident to edge with id '{this.Id}'.");
            }

            node1.Disconnect(this);
            node2.Disconnect(this);

            return this;
        }


        public bool IsIncident(TId nodeId)
        {
            return this.SourceId.Equals(nodeId) || this.TargetId.Equals(nodeId);
        }


        public bool IsIncident(INode<TId> node)
        {
            return node is null
                ? throw new ArgumentNullException(nameof(node))
                : this.IsIncident(node.Id);
        }


        public bool Equals(Edge<TId> other)
        {
            return other != null
                && this.Id.Equals(other.Id)
                && this.SourceId.Equals(other.SourceId)
                && this.TargetId.Equals(other.TargetId)
                && this.IsDirected == other.IsDirected;
        }


        public bool Equals(Edge<TId> x, Edge<TId> y)
        {
            return x != null && x.Equals(y);
        }


        public override bool Equals(object obj)
        {
            return obj is Edge<TId> edge && this.Equals(edge);
        }


        public int GetHashCode(Edge<TId> obj)
        {
            return obj is null
                ? throw new ArgumentNullException(nameof(obj))
                : obj.GetHashCode();
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(
                this.Id,
                this.SourceId,
                this.TargetId,
                this.IsDirected);
        }
    }
}
