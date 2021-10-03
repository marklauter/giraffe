using Graphs.Identifiers;
using System;

namespace Graphs.Attributes
{
    public interface IQualified<TId>
        : IIdentifiable<TId>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        bool Equals(string name, object other);

        bool HasAttribute(string name);

        bool TryGetValue(string name, out object value);
    }
}
