using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Classifications
{
    public interface IClassified<TId>
        : IIdentifiable<TId>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IEnumerable<string> Labels { get; }

        bool Is(string label);

        bool Is(IEnumerable<string> labels);
    }
}
