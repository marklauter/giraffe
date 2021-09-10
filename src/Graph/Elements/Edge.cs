using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Elements
{
    [DebuggerDisplay("{SourceId} : {TargetId}")]
    [JsonObject("edge")]
    public sealed class Edge
        : Element<Guid>
        , IEdge
    {
        /// <summary>
        /// Creates an edge from two nodes. Defaults to directed edge.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns><see cref="Edge"/></returns>
        public static Edge Couple(INode source, INode target)
        {
            return Edge.Couple(source, target, true);
        }

        /// <summary>
        /// Creates an edge from two nodes. Defaults to directed edge.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="isDirected"></param>
        /// <returns><see cref="Edge"/></returns>
        public static Edge Couple(INode source, INode target, bool isDirected)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (source.TryCouple(target) && !isDirected)
            {
                _ = target.TryCouple(source);
            }

            return new(Guid.NewGuid(), source.Id, target.Id, isDirected);
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

        [JsonConstructor]
        private Edge(Guid id, Guid sourceId, Guid targetId, bool isDirected)
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
        public bool Equals([Pure] IEdge other)
        {
            return other != null
                && this.Id == other.Id
                && this.SourceId == other.SourceId
                && this.TargetId == other.TargetId
                && this.IsDirected == other.IsDirected;
        }

        [Pure]
        public bool Equals([Pure] IEdge x, [Pure] IEdge y)
        {
            return x != null && x.Equals(y);
        }

        [Pure]
        public override bool Equals([Pure] object obj)
        {
            return obj is IEdge edge && this.Equals(edge);
        }

        [Pure]
        public int GetHashCode([DisallowNull] IEdge obj)
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
