using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Quantifiers
{
    public sealed class Quantity
        : ICloneable
        , IEquatable<Quantity>
        , IEqualityComparer<Quantity>
    {
        public static Quantity New(string name, bool value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, sbyte value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, byte value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, short value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, ushort value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, int value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, uint value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, long value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, ulong value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, float value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, double value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, decimal value)
        {
            return new Quantity(name, value);
        }

        public static Quantity New(string name, DateTime value)
        {
            return new Quantity(name, value);
        }

        [JsonConstructor]
        internal Quantity(string name, TypeCode typeCode, object value)
        {
            this.Name = name;
            this.TypeCode = typeCode;
            this.Value = Convert.ChangeType(value, typeCode);
        }

        private Quantity(string name, object value)
        {
            this.Name = name;
            this.Value = value;
            this.TypeCode = Type.GetTypeCode(value.GetType());
        }

        private Quantity(Quantity other)
        {
            this.Name = other.Name;
            this.TypeCode = other.TypeCode;
            this.Value = other.Value;
        }

        public string Name { get; }

        public TypeCode TypeCode { get; }

        public object Value { get; }

        [Pure]
        public object Clone()
        {
            return new Quantity(this);
        }

        public override bool Equals(object obj)
        {
            return obj is Quantity qty && this.Equals(qty);
        }

        public bool Equals(Quantity other)
        {
            return other != null
                && this.Name == other.Name
                && this.TypeCode == other.TypeCode
                && this.Value == other.Value;
        }

        public bool Equals(Quantity x, Quantity y)
        {
            return Quantity.AreEqual(x, y);
        }

        public int GetHashCode([DisallowNull] Quantity obj)
        {
            return obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.TypeCode, this.Value);
        }

        private static bool AreEqual(Quantity q1, Quantity q2)
        {
            return (q1 == null && q2 == null)
                || (q1 != null && q2 != null && q1.Equals(q2));                
        }
    }
}
