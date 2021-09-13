using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Qualifiers
{
    [DebuggerDisplay("{Value},  {TypeCode}")]
    public sealed class SerializableValue
        : ICloneable
        , IEquatable<SerializableValue>
        , IEqualityComparer<SerializableValue>
    {
        [JsonConstructor]
        internal SerializableValue(TypeCode typeCode, object value)
        {
            this.TypeCode = typeCode;
            this.Value = value == null
                ? null
                : Convert.ChangeType(value, typeCode);
        }

        private SerializableValue(object value)
        {
            this.Value = value;
            this.TypeCode = GetTypeCode(value);
        }

        private SerializableValue(SerializableValue other)
        {
            this.TypeCode = other.TypeCode;
            this.Value = other.Value;
        }

        public TypeCode TypeCode { get; }

        public object Value { get; }

        [Pure]
        public object Clone()
        {
            return new SerializableValue(this);
        }

        public override bool Equals(object obj)
        {
            return obj is SerializableValue qty && this.Equals(qty);
        }

        public bool Equals(SerializableValue other)
        {
            return other != null
                && this.TypeCode == other.TypeCode
                && this.Value.Equals(other.Value);
        }

        public bool Equals(SerializableValue x, SerializableValue y)
        {
            return SerializableValue.AreEqual(x, y);
        }

        public int GetHashCode([DisallowNull] SerializableValue obj)
        {
            return obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.TypeCode, this.Value, 7);
        }

        private static TypeCode GetTypeCode(object value)
        {
            return value is null
                ? TypeCode.DBNull
                : Type.GetTypeCode(value.GetType());
        }

        private static bool AreEqual(SerializableValue q1, SerializableValue q2)
        {
            return (q1 == null && q2 == null)
                || (q1 != null && q2 != null && q1.Equals(q2));
        }

        public static explicit operator bool(SerializableValue quantity)
        {
            return (bool)Convert.ChangeType(quantity.Value, typeof(bool));
        }

        public static explicit operator SerializableValue(bool value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator byte(SerializableValue quantity)
        {
            return (byte)Convert.ChangeType(quantity.Value, typeof(byte));
        }

        public static explicit operator SerializableValue(byte value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator sbyte(SerializableValue quantity)
        {
            return (sbyte)Convert.ChangeType(quantity.Value, typeof(sbyte));
        }

        public static explicit operator SerializableValue(sbyte value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator short(SerializableValue quantity)
        {
            return (short)Convert.ChangeType(quantity.Value, typeof(short));
        }

        public static explicit operator SerializableValue(short value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator ushort(SerializableValue quantity)
        {
            return (ushort)Convert.ChangeType(quantity.Value, typeof(ushort));
        }

        public static explicit operator SerializableValue(ushort value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator int(SerializableValue quantity)
        {
            return (int)Convert.ChangeType(quantity.Value, typeof(int));
        }

        public static explicit operator SerializableValue(int value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator uint(SerializableValue quantity)
        {
            return (uint)Convert.ChangeType(quantity.Value, typeof(uint));
        }

        public static explicit operator SerializableValue(uint value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator long(SerializableValue quantity)
        {
            return (long)Convert.ChangeType(quantity.Value, typeof(long));
        }

        public static explicit operator SerializableValue(long value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator ulong(SerializableValue quantity)
        {
            return (ulong)Convert.ChangeType(quantity.Value, typeof(ulong));
        }

        public static explicit operator SerializableValue(ulong value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator float(SerializableValue quantity)
        {
            return (float)Convert.ChangeType(quantity.Value, typeof(float));
        }

        public static explicit operator SerializableValue(float value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator double(SerializableValue quantity)
        {
            return (double)Convert.ChangeType(quantity.Value, typeof(double));
        }

        public static explicit operator SerializableValue(double value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator decimal(SerializableValue quantity)
        {
            return (decimal)Convert.ChangeType(quantity.Value, typeof(decimal));
        }

        public static explicit operator SerializableValue(decimal value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator DateTime(SerializableValue quantity)
        {
            return (DateTime)Convert.ChangeType(quantity.Value, typeof(DateTime));
        }

        public static explicit operator SerializableValue(DateTime value)
        {
            return new SerializableValue(value);
        }

        public static explicit operator string(SerializableValue quantity)
        {
            return (string)Convert.ChangeType(quantity.Value, typeof(string));
        }

        public static explicit operator SerializableValue(string value)
        {
            return new SerializableValue(value);
        }
    }
}
