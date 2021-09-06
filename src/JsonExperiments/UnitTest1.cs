using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace JsonExperiments
{
    public class JsonExperiment
    {
        [JsonArray(Title = "ARR")]
        internal sealed class ArrayClass
            : IEnumerable<int>
        {
            private readonly int[] items;

            [JsonConstructor]
            [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used for serialization.")]
            private ArrayClass(IEnumerable<int> ints)
            {
                this.items = ints.ToArray();
            }

            public ArrayClass(int[] ints)
            {
                this.items = ints.ToArray();
            }

            public IEnumerator<int> GetEnumerator()
            {
                foreach (var i in this.items)
                {
                    yield return i;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        internal sealed class AggregateClass
        {
            private readonly int[] items;

            [JsonProperty("items")]
            public IEnumerable<int> Items => this.items;

            [JsonProperty("id")]
            public Guid Id { get; }

            [JsonConstructor]
            [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used for serialization.")]
            private AggregateClass(Guid id, IEnumerable<int> items)
            {
                this.Id = id;
                this.items = items.ToArray();
            }

            public AggregateClass(Guid id, int[] items)
            {
                this.Id = id;
                this.items = items.ToArray();
            }
        }

        [Fact]
        public void ArrayAttribute_Test()
        {
            var ac = new ArrayClass(new int[] { 1, 2, 3, 4 });
            var json = JsonConvert.SerializeObject(ac);
            Assert.False(String.IsNullOrWhiteSpace(json));
            var ca = JsonConvert.DeserializeObject<ArrayClass>(json);
            Assert.Contains(1, ca);
        }

        [Fact]
        public void AggregateClass_Test()
        {
            var ac = new AggregateClass(Guid.NewGuid(), new int[] { 1, 2, 3, 4 });
            var json = JsonConvert.SerializeObject(ac);
            Assert.False(String.IsNullOrWhiteSpace(json));
            var ca = JsonConvert.DeserializeObject<AggregateClass>(json);
            Assert.Contains(1, ca.Items);
        }
    }
}
