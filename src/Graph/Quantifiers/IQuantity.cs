using System;

namespace Graph.Quantifiers
{
    public interface IQuantity
        : ICloneable
    {
        string Name { get; }
        string Value { get; }
        TypeCode TypeCode { get; }
        T ParseValue<T>() where T : struct, IComparable, IComparable<T>, IEquatable<T>, IFormattable;
    }
}
