using Graph.Qualifiers;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Graph.Elements.Tests
{
    public sealed class SerializableValueTests
    {
        [Fact]
        public void SerializableValue_Equals_With_Null()
        {
            var value = false;
            var quantity = (SerializableValue)value;

            var clone = null as SerializableValue;

            Assert.False(quantity.Equals(clone));
        }

        [Fact]
        public void SerializableValue_Equals_With_Null_X_Y()
        {
            var value = false;
            var quantity = (SerializableValue)value;

            var clone = null as SerializableValue;

            Assert.False(quantity.Equals(quantity, clone));
            Assert.False(quantity.Equals(clone, quantity));
        }

        [Fact]
        public void SerializableValue_Clone()
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
        public void SerializableValue_New_Bool_False()
        {
            var value = false;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (bool)quantity);
        }

        [Fact]
        public void SerializableValue_New_Bool_True()
        {
            var value = true;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (bool)quantity);
        }

        [Fact]
        public void SerializableValue_New_Byte()
        {
            var value = (byte)1;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (byte)quantity);
        }

        [Fact]
        public void SerializableValue_New_SByte()
        {
            var value = (sbyte)1;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (sbyte)quantity);
        }

        [Fact]
        public void SerializableValue_New_Short()
        {
            var value = (short)1;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (short)quantity);
        }

        [Fact]
        public void SerializableValue_New_UShort()
        {
            var value = (ushort)1;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (ushort)quantity);
        }

        [Fact]
        public void SerializableValue_New_Int()
        {
            var value = 1;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (int)quantity);
        }

        [Fact]
        public void SerializableValue_New_UInt()
        {
            var value = 1u;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (uint)quantity);
        }

        [Fact]
        public void SerializableValue_New_Long()
        {
            var value = 1L;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (long)quantity);
        }

        [Fact]
        public void SerializableValue_New_ULong()
        {
            var value = 1ul;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (ulong)quantity);
        }

        [Fact]
        public void SerializableValue_New_Single()
        {
            var value = 1.1f;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (float)quantity);
        }

        [Fact]
        public void SerializableValue_New_Double()
        {
            var value = 1.1d;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (double)quantity);
        }

        [Fact]
        public void SerializableValue_New_Decimal()
        {
            var value = (decimal)1.1;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (decimal)quantity);
        }

        [Fact]
        public void SerializableValue_New_DateTime()
        {
            var value = DateTime.Now;
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (DateTime)quantity);
        }

        [Fact]
        public void SerializableValue_New_String()
        {
            var value = "y";
            var quantity = (SerializableValue)value;
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (string)quantity);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Bug", "S2259:Null pointers should not be dereferenced", Justification = "Compiler is confused.")]
        public void SerializableValue_New_DbNull()
        {
            string value = null;
            var quantity = (SerializableValue)value;
            Assert.NotNull(quantity);
            Assert.Equal(value, quantity.Value);

            var json = JsonConvert.SerializeObject(quantity);
            quantity = JsonConvert.DeserializeObject<SerializableValue>(json);

            Assert.Equal(value, quantity.Value);

            Assert.Equal(value, (string)quantity);
        }

        [Fact]
        public void SerializableValue_HashCodes_Are_Equal()
        {
            var value = 1;
            var q1 = (SerializableValue)value;
            var q2 = (SerializableValue)value;

            Assert.Equal(q1, q2);
            Assert.Equal(q1.GetHashCode(), q2.GetHashCode());
        }

        [Fact]
        public void SerializableValue_HashCodes_Obj_Are_Equal()
        {
            var value = 1;
            var q1 = (SerializableValue)value;
            var q2 = (SerializableValue)"abcdefghijklmnop";

            Assert.NotEqual(q1, q2);
            Assert.NotEqual(q1.GetHashCode(), q2.GetHashCode());
            Assert.Equal(q1.GetHashCode(), q2.GetHashCode(q1));
        }

        [Fact]
        public void SerializableValue_Equals_Obj_True()
        {
            var value = 1;
            var q1 = (SerializableValue)value;
            var q2 = (SerializableValue)value;

            Assert.True(q1.Equals(q2 as object));
        }

        [Fact]
        public void SerializableValue_Equals_Obj_False_1()
        {
            var value = 1;
            var q1 = (SerializableValue)value;
            var q2 = (SerializableValue)(value + 1);

            Assert.False(q1.Equals(q2 as object));
        }

        [Fact]
        public void SerializableValue_Equals_Obj_False_2()
        {
            var value = 1;
            var q1 = (SerializableValue)value;
            var q2 = Node.New;

            Assert.False(q1.Equals(q2));
        }
    }
}
