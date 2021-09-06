using System;

namespace Graph.Elements
{
    public interface IQuantity
    {
        string Name { get; }
        string Value { get; }
        TypeCode TypeCode { get; }
        T ParseValue<T>() where T : struct, IComparable, IComparable<T>, IEquatable<T>, IFormattable;
    }
}
