using Graphs.Elements.Edges;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graphs.IO
{
    [JsonObject("edge")]
    public sealed class EdgeEntry<TId>
        : ElementEntry<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly Edge<TId> edge;

        [JsonProperty("directed")]
        public bool IsDirected => this.edge.IsDirected;

        [JsonProperty("source")]
        public TId SourceId => this.edge.SourceId;

        [JsonProperty("target")]
        public TId TargetId => this.edge.TargetId;

        private EdgeEntry(Edge<TId> edge)
            : base(edge)
        {
            this.edge = edge ?? throw new ArgumentNullException(nameof(edge));
        }

        internal EdgeEntry(
            TId id,
            [DisallowNull, Pure] IEnumerable<string> classifications,
            [DisallowNull, Pure] IEnumerable<KeyValuePair<string, object>> qualifications,
            TId sourceId,
            TId targetId,
            bool isDirected)
        {
            this.edge = new Edge<TId>(
                id, 
                classifications,
                qualifications,
                sourceId, 
                targetId,
                isDirected);
        }

        public static explicit operator Edge<TId>(EdgeEntry<TId> edgeEntry)
        {
            return edgeEntry.edge;
        }

        public static explicit operator EdgeEntry<TId>([DisallowNull, Pure] Edge<TId> edge)
        {
            return new EdgeEntry<TId>(edge);
        }
    }
}
