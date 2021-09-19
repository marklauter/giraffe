using Graphs.Elements.Mutables;
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
    public sealed class Edge
        : Mutable<Guid>
        , IEquatable<Edge>
        , IEqualityComparer<Edge>
    {
        /// <summary>
        /// Creates an edge from two nodes. Defaults to directed edge.
        /// </summary>
        /// <param name="source"><see cref="Node"/></param>
        /// <param name="target"><see cref="Node"/></param>
        /// <returns><see cref="Edge"/></returns>
        public static Edge Couple(Node source, Node target)
        {
            return Couple(source, target, true);
        }

        /// <summary>
        /// Creates an edge from two nodes.
        /// </summary>
        /// <param name="source"><see cref="Node"/></param>
        /// <param name="target"><see cref="Node"/></param>
        /// <param name="isDirected"><see cref="Boolean"/></param>
        /// <returns><see cref="Edge"/></returns>
        public static Edge Couple(Node source, Node target, bool isDirected)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            var edge = new Edge(Guid.NewGuid(), source.Id, target.Id, isDirected);
            source.Couple(edge);
            target.Couple(edge);

            return edge;
        }

        [Required]
        [JsonProperty("directed")]
        public bool IsDirected { get; }

        [Required]
        [JsonProperty("source")]
        public Guid SourceId { get; }

        [Required]
        [JsonProperty("target")]
        public Guid TargetId { get; }

        // internal for testing
        [JsonConstructor]
        internal Edge(Guid id, Guid sourceId, Guid targetId, bool isDirected)
            : base(id)
        {
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.IsDirected = isDirected;
        }

        private Edge([DisallowNull] Edge other)
            : base(other)
        {
            this.SourceId = other.SourceId;
            this.TargetId = other.TargetId;
            this.IsDirected = other.IsDirected;
        }

        [Pure]
        public override object Clone()
        {
            return new Edge(this);
        }

        [Pure]
        public bool IsIncident(Guid nodeId)
        {
            return nodeId == this.SourceId || nodeId == this.TargetId;
        }

        public void Decouple(Node node1, Node node2)
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
                throw new InvalidOperationException($"{nameof(Node)} with id '{node1.Id}' is not incident to edge with id '{this.Id}'.");
            }

            if (!this.Nodes.Contains(node2.Id))
            {
                throw new InvalidOperationException($"{nameof(Node)} with id '{node2.Id}' is not incident to edge with id '{this.Id}'.");
            }

            node1.Decouple(this);
            node2.Decouple(this);
        }

        [Pure]
        public bool Equals([Pure] Edge other)
        {
            return other != null
                && this.Id == other.Id
                && this.SourceId == other.SourceId
                && this.TargetId == other.TargetId
                && this.IsDirected == other.IsDirected;
        }

        [Pure]
        public bool Equals([Pure] Edge x, [Pure] Edge y)
        {
            return x != null && x.Equals(y);
        }

        [Pure]
        public override bool Equals([Pure] object obj)
        {
            return obj is Edge edge && this.Equals(edge);
        }

        [Pure]
        public int GetHashCode([DisallowNull] Edge obj)
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

        [Pure]
        [JsonIgnore]
        public IEnumerable<Guid> Nodes => new Guid[] { this.SourceId, this.TargetId };
    }
}
