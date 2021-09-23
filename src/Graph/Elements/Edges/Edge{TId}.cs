using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graphs.Elements
{
    [DebuggerDisplay("{SourceId} : {TargetId}")]
    public sealed class Edge<TId>
        : Element<TId>
        , IEdge<TId>
        , IEquatable<Edge<TId>>
        , IEqualityComparer<Edge<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        /// <summary>
        /// Creates an edge from two nodes. Defaults to directed edge.
        /// </summary>
        /// <param name="source"><see cref="INode"/></param>
        /// <param name="target"><see cref="INode"/></param>
        /// <returns><see cref="Edge"/></returns>
        internal static IEdge<TId> Connect(TId id, INode<TId> source, INode<TId> target)
        {
            return Connect(id, source, target, true);
        }

        /// <summary>
        /// Creates an edge from two nodes.
        /// </summary>
        /// <param name="source"><see cref="INode"/></param>
        /// <param name="target"><see cref="INode"/></param>
        /// <param name="isDirected"><see cref="Boolean"/></param>
        /// <returns><see cref="Edge"/></returns>
        internal static IEdge<TId> Connect(TId id, INode<TId> source, INode<TId> target, bool isDirected)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            var edge = new Edge<TId>(id, source.Id, target.Id, isDirected);
            source.Connect(edge);
            target.Connect(edge);

            return edge;
        }

        public bool IsDirected { get; }

        [Pure]
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

        private Edge([DisallowNull] Edge<TId> other)
            : base(other)
        {
            this.SourceId = other.SourceId;
            this.TargetId = other.TargetId;
            this.IsDirected = other.IsDirected;
        }

        [Pure]
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

        [Pure]
        public bool IsIncident(TId nodeId)
        {
            return this.SourceId.Equals(nodeId) || this.TargetId.Equals(nodeId);
        }

        [Pure]
        public bool IsIncident(INode<TId> node)
        {
            return node is null 
                ? throw new ArgumentNullException(nameof(node)) 
                : this.IsIncident(node.Id);
        }

        [Pure]
        public bool Equals([Pure] Edge<TId> other)
        {
            return other != null
                && this.Id.Equals(other.Id)
                && this.SourceId.Equals(other.SourceId)
                && this.TargetId.Equals(other.TargetId)
                && this.IsDirected == other.IsDirected;
        }

        [Pure]
        public bool Equals([Pure] Edge<TId> x, [Pure] Edge<TId> y)
        {
            return x != null && x.Equals(y);
        }

        [Pure]
        public override bool Equals([Pure] object obj)
        {
            return obj is Edge<TId> edge && this.Equals(edge);
        }

        [Pure]
        public int GetHashCode([DisallowNull, Pure] Edge<TId> obj)
        {
            return obj is null
                ? throw new ArgumentNullException(nameof(obj))
                : obj.GetHashCode();
        }

        [Pure]
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
