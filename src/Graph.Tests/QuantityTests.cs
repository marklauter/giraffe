using Graph.Quantifiers;
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
            var name = "x";
            var value = false;
            var quantity = Quantity.New(name, value);

            var clone = null as Quantity;

            Assert.False(quantity.Equals(clone));
        }

        [Fact]
        public void Quantity_Equals_With_Null_X_Y()
        {
            var name = "x";
            var value = false;
            var quantity = Quantity.New(name, value);

            var clone = null as Quantity;

            Assert.False(quantity.Equals(quantity, clone));
            Assert.False(quantity.Equals(clone, quantity));
        }

        [Fact]
        public void Quantity_Clone()
        {
            var name = "x";
            var value = false;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var clone = quantity.Clone() as Quantity;

            Assert.Equal(name, clone.Name);
            Assert.Equal(value, clone.Value);
            Assert.Equal(quantity.TypeCode, clone.TypeCode);

            Assert.Equal(quantity, clone);
        }

        [Fact]
        public void Quantity_New_Bool_False()
        {
            var name = "x";
            var value = false;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Bool_True()
        {
            var name = "x";
            var value = true;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Byte()
        {
            var name = "x";
            var value = (byte)1;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_SByte()
        {
            var name = "x";
            var value = (sbyte)1;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Int()
        {
            var name = "x";
            var value = 1;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_UInt()
        {
            var name = "x";
            var value = 1u;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Long()
        {
            var name = "x";
            var value = 1L;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_ULong()
        {
            var name = "x";
            var value = 1ul;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Single()
        {
            var name = "x";
            var value = 1.1f;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Double()
        {
            var name = "x";
            var value = 1.1d;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_Decimal()
        {
            var name = "x";
            var value = (decimal)1.1;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void Quantity_New_DateTime()
        {
            var name = "x";
            var value = DateTime.Now;
            var quantity = Quantity.New(name, value);
            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<Quantity>(json);

            Assert.Equal(name, quantity.Name);
            Assert.Equal(value, quantity.Value);
        }
    }
}
