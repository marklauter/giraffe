using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Classes
{
    public interface IClassified<TId>
        : IIdentifiable<TId>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        bool Is(string label);

        bool Is(IEnumerable<string> labels);
    }
}
