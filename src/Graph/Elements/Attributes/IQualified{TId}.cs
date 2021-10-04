using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Attributes
{
    public interface IQualified<TId>
        : IIdentifiable<TId>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IEnumerable<KeyValuePair<string, object>> Attributes { get; }

        bool Equals(string name, object other);

        bool HasAttribute(string name);

        bool TryGetValue(string name, out object value);
    }
}
