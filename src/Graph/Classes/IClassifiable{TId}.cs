using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Classes
{
    public interface IClassifiable<TId>
        : IIdentifiable<TId>
        , IEnumerable<string>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        void Classify(string label);

        void Declassify(string label);

        bool Is(string label);

        bool Is(IEnumerable<string> labels);
    }
}
