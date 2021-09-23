using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Graphs.Elements
{
    [DebuggerDisplay("{SourceId} : {TargetId}")]
    [JsonObject("edge")]
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
        /// <param name="source"><see cref="Node"/></param>
        /// <param name="target"><see cref="Node"/></param>
        /// <returns><see cref="Edge"/></returns>
        internal static Edge<TId> Couple(IIdGenerator<TId> idGenerator, Node<TId> source, Node<TId> target)
        {
            return Couple(idGenerator, source, target, true);
        }

        /// <summary>
        /// Creates an edge from two nodes.
        /// </summary>
        /// <param name="source"><see cref="Node"/></param>
        /// <param name="target"><see cref="Node"/></param>
        /// <param name="isDirected"><see cref="Boolean"/></param>
        /// <returns><see cref="Edge"/></returns>
        internal static Edge<TId> Couple(IIdGenerator<TId> idGenerator, Node<TId> source, Node<TId> target, bool isDirected)
        {
            if (idGenerator is null)
            {
                throw new ArgumentNullException(nameof(idGenerator));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            var edge = new Edge<TId>(idGenerator.NewId(), source.Id, target.Id, isDirected);
            source.Couple(edge);
            target.Couple(edge);

            return edge;
        }

        internal void Decouple(Node<TId> node1, Node<TId> node2)
        {
            if (node1 is null)
            {
                throw new ArgumentNullException(nameof(node1));
            }

            if (node2 is null)
            {
                throw new ArgumentNullException(nameof(node2));
            }

            if (!this.Nodes.Contains(node1.Id))
            {
                throw new InvalidOperationException($"{nameof(Node<TId>)} with id '{node1.Id}' is not incident to edge with id '{this.Id}'.");
            }

            if (!this.Nodes.Contains(node2.Id))
            {
                throw new InvalidOperationException($"{nameof(Node<TId>)} with id '{node2.Id}' is not incident to edge with id '{this.Id}'.");
            }

            node1.Decouple(this);
            node2.Decouple(this);
        }

        [Required]
        [JsonProperty("directed")]
        public bool IsDirected { get; }

        [Pure]
        [JsonIgnore]
        public IEnumerable<TId> Nodes
        {
            get
            {
                yield return this.SourceId;
                yield return this.TargetId;
            }
        }

        [Required]
        [JsonProperty("source")]
        public TId SourceId { get; }

        [Required]
        [JsonProperty("target")]
        public TId TargetId { get; }

        // internal for testing
        [JsonConstructor]
        internal Edge(TId id, TId sourceId, TId targetId, bool isDirected)
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

        [Pure]
        public bool IsIncident(TId nodeId)
        {
            return this.SourceId.Equals(nodeId) || this.TargetId.Equals(nodeId);
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
