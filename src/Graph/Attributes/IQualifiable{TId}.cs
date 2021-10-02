using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Attributes
{
    public interface IQualifiable<TId>
        : IIdentifiable<TId>
        , IEnumerable<KeyValuePair<string, object>>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        void Disqualify(string name);

        bool Equals(string name, object other);

        bool HasAttribute(string name);

        void Qualify(string name, object value);

        bool TryGetValue(string name, out object value);
    }
}
