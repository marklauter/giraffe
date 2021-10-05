using Graphs.Classifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Graphs.IO
{
    [JsonObject]
    public sealed class ClassifiedState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [JsonProperty]
        public TId Id { get; }

        [JsonProperty]
        public IEnumerable<string> Labels { get; }

        [JsonConstructor]
        public ClassifiedState(IEnumerable<string> labels)
        {
            this.Labels = labels;
        }

        public ClassifiedState(IClassified<TId> component)
        {
            this.Labels = component.Labels;
        }
    }
}
