using Graphs.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Graphs.IO
{
    [JsonObject]
    public sealed class QualifiedState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [JsonProperty]
        public TId Id { get; }

        [JsonProperty]
        public IEnumerable<KeyValuePair<string, object>> Attributes { get; }

        [JsonConstructor]
        public QualifiedState(IEnumerable<KeyValuePair<string, object>> attributes)
        {
            this.Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
        }

        public QualifiedState(IQualified<TId> component)
        {
            this.Attributes = component.Attributes;
        }
    }
}
