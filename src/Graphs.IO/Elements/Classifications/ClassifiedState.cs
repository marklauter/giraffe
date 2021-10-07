using Graphs.Classifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graphs.IO
{
    [JsonObject]
    public sealed class ClassifiedState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [Key]
        [Required]
        [JsonProperty]
        public TId Id { get; }

        [Required]
        [JsonProperty]
        public IEnumerable<string> Labels { get; }

        [JsonConstructor]
        public ClassifiedState(TId id, IEnumerable<string> labels)
        {
            this.Id = id;
            this.Labels = labels ?? throw new ArgumentNullException(nameof(labels));
        }

        public ClassifiedState(IClassified<TId> component)
            : this(component.Id, component.Labels)
        {
        }
    }
}
