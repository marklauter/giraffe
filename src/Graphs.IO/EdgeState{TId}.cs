using Graphs.Elements.Edges;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Graphs.IO
{
    [JsonObject("edge")]
    public sealed class EdgeState<TId>
        : ElementState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly Edge<TId> edge;

        [JsonProperty("isDirected")]
        public bool IsDirected => this.edge.IsDirected;

        [JsonProperty("sourceId")]
        public TId SourceId => this.edge.SourceId;

        [JsonProperty("targetId")]
        public TId TargetId => this.edge.TargetId;

        private EdgeState(Edge<TId> edge)
            : base(edge)
        {
            this.edge = edge ?? throw new ArgumentNullException(nameof(edge));
        }

        [JsonConstructor]
        internal EdgeState(
            TId id,
            IEnumerable<string> classifications,
            IEnumerable<KeyValuePair<string, object>> qualifications,
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

        public static explicit operator EdgeState<TId>(Edge<TId> edge)
        {
            return new EdgeState<TId>(edge);
        }

        public static explicit operator Edge<TId>(EdgeState<TId> edgeEntry)
        {
            return edgeEntry.edge;
        }
    }
}
