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

        internal sealed class Quantity
        {
            [JsonConstructor]
            internal Quantity(string name, object value, TypeCode typeCode) 
            {
                this.Name = name;
                this.TypeCode = typeCode;
                this.Value = Convert.ChangeType(value, typeCode);
            }

            public Quantity(string name, int value)
            {
                this.Name = name;
                this.Value = value;
                this.TypeCode = Type.GetTypeCode(value.GetType());
            }

            public Quantity(string name, double value)
            {
                this.Name = name;
                this.Value = value;
                this.TypeCode = Type.GetTypeCode(value.GetType());
            }

            public string Name { get; set; }

            public object Value { get; set; }

            public TypeCode TypeCode { get; set; }
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

        [Fact]
        public void Quantity_WritesTypeCode()
        {
            var one = 1;
            var two = 2.2;
            var qty1 = new Quantity("q1", one);
            var qty2 = new Quantity("q2", two);

            var json1 = JsonConvert.SerializeObject(qty1);
            Assert.NotEmpty(json1);
            var qty3 = JsonConvert.DeserializeObject<Quantity>(json1);
            Assert.Equal(one, qty3.Value);

            var json2 = JsonConvert.SerializeObject(qty2);
            Assert.NotEmpty(json2);
            var qty4 = JsonConvert.DeserializeObject<Quantity>(json2);
            Assert.Equal(two, qty4.Value);
        }
    }
}
