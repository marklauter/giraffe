using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JsonExperiments
{
    public class JsonExperiment
    {
        [JsonArray(Title = "ARR")]
        public sealed class ArrayClass
            : IEnumerable<int>
        {
            private readonly int[] items;

            [JsonConstructor]
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

        [Fact]
        public void ArrayAttribute_Test()
        {
            var ac = new ArrayClass(new int[] { 1, 2, 3, 4 });
            var json = JsonConvert.SerializeObject(ac);
            Assert.False(String.IsNullOrWhiteSpace(json));
            var ca = JsonConvert.DeserializeObject<ArrayClass>(json);
            Assert.Contains(1, ca);
        }
    }
}
