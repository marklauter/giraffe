using Graphs.Incidence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graphs.IO
{
    public sealed class IncidenceListState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [Key]
        [Required]
        [JsonProperty]
        public TId Id { get; }

        [Required]
        [JsonProperty]
        public IEnumerable<TId> Edges { get; }

        [JsonConstructor]
        public IncidenceListState(TId id, IEnumerable<TId> edges)
        {
            this.Id = id;
            this.Edges = edges ?? throw new ArgumentNullException(nameof(edges));
        }

        public IncidenceListState(IIncidenceList<TId> component)
            : this(component.Id, component.Edges)
        {
        }
    }
}
