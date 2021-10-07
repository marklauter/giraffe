using Graphs.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graphs.IO
{
    [JsonObject]
    public sealed class QualifiedState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [Key]
        [Required]
        [JsonProperty]
        public TId Id { get; }

        [Required]
        [JsonProperty]
        public IEnumerable<KeyValuePair<string, object>> Attributes { get; }

        [JsonConstructor]
        public QualifiedState(TId id, IEnumerable<KeyValuePair<string, object>> attributes)
        {
            this.Id = id;
            this.Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
        }

        public QualifiedState(IQualified<TId> component)
            : this(component.Id, component.Attributes)
        {
        }
    }
}
