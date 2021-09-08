using Graph.Qualifiers;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Graph.Tests
{
    public sealed class QuantityTests
    {
        [Fact]
        public void Quantity_Equals_With_Null()
        {
            var value = false;
            var quantity = (SerializableValue)value;

            var clone = null as SerializableValue;

            Assert.False(quantity.Equals(clone));
        }

        [Fact]
        public void Quantity_Equals_With_Null_X_Y()
        {
            var value = false;
            var quantity = (SerializableValue)value;

            var clone = null as SerializableValue;

            Assert.False(quantity.Equals(quantity, clone));
            Assert.False(quantity.Equals(clone, quantity));
        }

        [Fact]
        public void Quantity_Clone()
        {
            var value = false;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var clone = quantity.Clone() as SerializableValue;

            Assert.Equal(value, clone.Value);
            Assert.Equal(quantity.TypeCode, clone.TypeCode);

            Assert.Equal(quantity, clone);
        }

        [Fact]
        public void Quantity_New_Bool_False()
        {
            var value = false;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Bool_True()
        {
            var value = true;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Byte()
        {
            var value = (byte)1;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_SByte()
        {
            var value = (sbyte)1;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Int()
        {
            var value = 1;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_UInt()
        {
            var value = 1u;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Long()
        {
            var value = 1L;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_ULong()
        {
            var value = 1ul;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Single()
        {
            var value = 1.1f;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Double()
        {
            var value = 1.1d;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Decimal()
        {
            var value = (decimal)1.1;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_DateTime()
        {
            var value = DateTime.Now;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_String()
        {
            var value = "y";
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);
        }
    }
}
