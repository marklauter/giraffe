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

        private Edge([DisallowNull] Edge other)
            : base(other)
        {
            this.SourceId = other.SourceId;
            this.TargetId = other.TargetId;
            this.IsDirected = other.IsDirected;
        }

        [JsonConstructor]
        private Edge(Guid sourceId, Guid targetId, bool isDirected)
            : base()
        {
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.IsDirected = isDirected;
        }

        public Edge([DisallowNull] Node source, [DisallowNull] Node target)
            : this(source.Id, target.Id, true)
        {
        }

        public Edge([DisallowNull] Node source, [DisallowNull] Node target, bool isDirected)
            : this(source.Id, target.Id, isDirected)
        {
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
            return this.Equals(obj as Edge);
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
