using Graphs.Adjacency;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graphs.IO
{
    [JsonObject]
    public sealed class AdjacencyListState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [Key]
        [Required]
        [JsonProperty]
        public TId Id { get; }

        [Required]
        [JsonProperty]
        public IEnumerable<KeyValuePair<TId, int>> ReferenceCountedNeighbors { get; }

        [JsonConstructor]
        public AdjacencyListState(TId id, IEnumerable<KeyValuePair<TId, int>> referenceCountedNeighbors)
        {
            this.Id = id;
            this.ReferenceCountedNeighbors = referenceCountedNeighbors ?? throw new ArgumentNullException(nameof(referenceCountedNeighbors));
        }

        public AdjacencyListState(IAdjacencyList<TId> component)
            : this(component.Id, component.ReferenceCountedNeighbors)
        {
        }
    }
}
