using Documents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graphs.IO
{
    public abstract class ElementState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IElement<TId> element;

        protected ElementState() { }

        protected ElementState(IElement<TId> element)
        {
            this.element = element ?? throw new ArgumentNullException(nameof(element));
        }

        [Key]
        [JsonProperty("id")]
        public TId Id { get; }

        [JsonProperty("classifications")]
        public IEnumerable<string> Classifications => this.element.Classifications;

        [JsonProperty("qualifications")]
        public IEnumerable<KeyValuePair<string, SerializableValue>> Qualifications
        {
            get
            {
                foreach (var kvp in this.element.Qualifications)
                {
                    yield return KeyValuePair.Create(kvp.Key, (SerializableValue)kvp.Value);
                }
            }
        }
    }
}
