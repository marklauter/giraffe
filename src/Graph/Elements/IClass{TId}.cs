using System;
using System.Collections.Generic;

namespace Graphs.Elements
{
    /// <summary>
    /// A class is a labeled collection of element references 
    /// </summary>
    /// <typeparam name="TId">type of the ID</typeparam>
    public interface IClass<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        string Label { get; }

        IClass<TId> Classify(TId id);
        IClass<TId> Classify(IEnumerable<TId> id);

        bool Contains(TId id);

        IClass<TId> Declassify(TId id);
        IClass<TId> Declassify(IEnumerable<TId> id);
    }
}
