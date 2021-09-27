using Documents;
using Graphs.Elements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graphs.IO
{
    public class ElementEntry<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IElement<TId> element;

        protected ElementEntry() { }

        protected ElementEntry(IElement<TId> element)
        {
            this.element = element ?? throw new ArgumentNullException(nameof(element));
        }

        [Key]
        [JsonProperty("id")]
        public TId Id { get; }

        [JsonProperty("classifications")]
        public IEnumerable<string> Classifications => this.element.Classifications;

        [JsonProperty("qualifications")]
        public IEnumerable<KeyValuePair<string, TypedValue>> Qualifications
        {
            get
            {
                foreach (var kvp in this.element.Qualifications)
                {
                    yield return KeyValuePair.Create(kvp.Key, (TypedValue)kvp.Value);
                }
            }
        }
    }
}
