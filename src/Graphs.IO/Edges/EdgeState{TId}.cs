using Graphs.Edges;
using Newtonsoft.Json;
using System;

namespace Graphs.IO
{
    [JsonObject]
    public class EdgeState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [JsonProperty]
        public TId Id { get; }

        [JsonProperty]
        public bool IsDirected { get; }

        [JsonProperty]
        public TId SourceId { get; }

        [JsonProperty]
        public TId TargetId { get; }

        [JsonConstructor]
        public EdgeState(TId id, TId sourceId, TId targetId, bool isDirected)
        {
            this.Id = id;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.IsDirected = isDirected;
        }

        public EdgeState(IEdge<TId> edge)
        {
            this.Id = edge.Id;
            this.SourceId = edge.SourceId;
            this.TargetId = edge.TargetId;
            this.IsDirected = edge.IsDirected;
        }
    }
}
