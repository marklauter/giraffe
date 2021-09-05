using Newtonsoft.Json;
using System;
using System.Collections;
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
        : Element
        , IEquatable<Edge>
        , IEqualityComparer<Edge>
        , IEnumerable<Guid>
    {
        public static Edge New(Guid sourceId, Guid targetId)
        {
            return new(Guid.NewGuid(), sourceId, targetId);
        }

        public static Edge New(Guid sourceId, Guid targetId, bool isDirected)
        {
            return new(Guid.NewGuid(), sourceId, targetId, isDirected);
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

        private Edge() : base() { }

        private Edge(Guid id, Guid sourceId, Guid targetId)
            : this(id, sourceId, targetId, false)
        {
        }

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
        public bool Equals(Edge other)
        {
            return other != null
                && this.Id == other.Id
                && this.SourceId == other.SourceId
                && this.TargetId == other.TargetId
                && this.IsDirected == other.IsDirected;
        }

        [Pure]
        public bool Equals(Edge x, Edge y)
        {
            return x != null && x.Equals(y) || y == null;
        }

        [Pure]
        public override bool Equals(object obj)
        {
            return obj is Edge edge && this.Equals(edge);
        }

        [Pure]
        public int GetHashCode([DisallowNull] Edge obj)
        {
            return obj.GetHashCode();
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
        public IEnumerator<Guid> GetEnumerator()
        {
            yield return this.SourceId;
            yield return this.TargetId;
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
