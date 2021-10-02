using System;
using System.Collections.Generic;

namespace Graphs.Classes
{
    public interface IClass<TId>
        : IEnumerable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        string Label { get; }

        IClass<TId> Classify(TId id);

        bool Contains(TId id);

        IClass<TId> Declassify(TId id);
    }
}
