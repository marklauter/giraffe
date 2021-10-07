using Graphs.Edges;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Graphs.IO
{
    [JsonObject]
    public class EdgeState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [Key]
        [Required]
        [JsonProperty]
        public TId Id { get; }

        [Required]
        [JsonProperty]
        public bool IsDirected { get; }

        [Required]
        [JsonProperty]
        public TId SourceId { get; }

        [Required]
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
            : this(edge.Id, edge.SourceId, edge.TargetId, edge.IsDirected)
        {
        }
    }
}
