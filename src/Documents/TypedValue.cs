using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Documents
{
    [DebuggerDisplay("{Value},  {TypeCode}")]
    [JsonObject("typedValue")]
    public sealed class TypedValue
        : ICloneable
        , IEquatable<TypedValue>
        , IEqualityComparer<TypedValue>
    {
        [JsonConstructor]
        internal TypedValue(TypeCode typeCode, object value)
        {
            this.TypeCode = typeCode;
            this.Value = value == null
                ? null
                : Convert.ChangeType(value, typeCode);
        }

        private TypedValue(object value)
        {
            this.Value = value;
            this.TypeCode = GetTypeCode(value);
        }

        private TypedValue(TypedValue other)
        {
            this.TypeCode = other.TypeCode;
            this.Value = other.Value;
        }

        [JsonProperty("code")]
        public TypeCode TypeCode { get; }

        [JsonProperty("value")]
        public object Value { get; }

        [Pure]
        public object Clone()
        {
            return new TypedValue(this);
        }

        public override bool Equals(object obj)
        {
            return obj is TypedValue qty && this.Equals(qty);
        }

        public bool Equals(TypedValue other)
        {
            return other != null
                && this.TypeCode == other.TypeCode
                && this.Value.Equals(other.Value);
        }

        public bool Equals(TypedValue x, TypedValue y)
        {
            return AreEqual(x, y);
        }

        public int GetHashCode([DisallowNull] TypedValue obj)
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

        private static bool AreEqual(TypedValue q1, TypedValue q2)
        {
            return (q1 == null && q2 == null)
                || (q1 != null && q2 != null && q1.Equals(q2));
        }

        public static explicit operator bool(TypedValue quantity)
        {
            return (bool)Convert.ChangeType(quantity.Value, typeof(bool));
        }

        public static explicit operator TypedValue(bool value)
        {
            return new TypedValue(value);
        }

        public static explicit operator byte(TypedValue quantity)
        {
            return (byte)Convert.ChangeType(quantity.Value, typeof(byte));
        }

        public static explicit operator TypedValue(byte value)
        {
            return new TypedValue(value);
        }

        public static explicit operator sbyte(TypedValue quantity)
        {
            return (sbyte)Convert.ChangeType(quantity.Value, typeof(sbyte));
        }

        public static explicit operator TypedValue(sbyte value)
        {
            return new TypedValue(value);
        }

        public static explicit operator short(TypedValue quantity)
        {
            return (short)Convert.ChangeType(quantity.Value, typeof(short));
        }

        public static explicit operator TypedValue(short value)
        {
            return new TypedValue(value);
        }

        public static explicit operator ushort(TypedValue quantity)
        {
            return (ushort)Convert.ChangeType(quantity.Value, typeof(ushort));
        }

        public static explicit operator TypedValue(ushort value)
        {
            return new TypedValue(value);
        }

        public static explicit operator int(TypedValue quantity)
        {
            return (int)Convert.ChangeType(quantity.Value, typeof(int));
        }

        public static explicit operator TypedValue(int value)
        {
            return new TypedValue(value);
        }

        public static explicit operator uint(TypedValue quantity)
        {
            return (uint)Convert.ChangeType(quantity.Value, typeof(uint));
        }

        public static explicit operator TypedValue(uint value)
        {
            return new TypedValue(value);
        }

        public static explicit operator long(TypedValue quantity)
        {
            return (long)Convert.ChangeType(quantity.Value, typeof(long));
        }

        public static explicit operator TypedValue(long value)
        {
            return new TypedValue(value);
        }

        public static explicit operator ulong(TypedValue quantity)
        {
            return (ulong)Convert.ChangeType(quantity.Value, typeof(ulong));
        }

        public static explicit operator TypedValue(ulong value)
        {
            return new TypedValue(value);
        }

        public static explicit operator float(TypedValue quantity)
        {
            return (float)Convert.ChangeType(quantity.Value, typeof(float));
        }

        public static explicit operator TypedValue(float value)
        {
            return new TypedValue(value);
        }

        public static explicit operator double(TypedValue quantity)
        {
            return (double)Convert.ChangeType(quantity.Value, typeof(double));
        }

        public static explicit operator TypedValue(double value)
        {
            return new TypedValue(value);
        }

        public static explicit operator decimal(TypedValue quantity)
        {
            return (decimal)Convert.ChangeType(quantity.Value, typeof(decimal));
        }

        public static explicit operator TypedValue(decimal value)
        {
            return new TypedValue(value);
        }

        public static explicit operator DateTime(TypedValue quantity)
        {
            return (DateTime)Convert.ChangeType(quantity.Value, typeof(DateTime));
        }

        public static explicit operator TypedValue(DateTime value)
        {
            return new TypedValue(value);
        }

        public static explicit operator string(TypedValue quantity)
        {
            return (string)Convert.ChangeType(quantity.Value, typeof(string));
        }

        public static explicit operator TypedValue(string value)
        {
            return new TypedValue(value);
        }
    }
}
