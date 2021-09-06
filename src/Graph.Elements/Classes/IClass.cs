using System;
using System.Collections.Generic;

namespace Graph.Elements
{
    public interface IClass<T>
        : ICloneable
        where T : struct, IComparable, IComparable<T>, IEquatable<T>, IFormattable
    {
        string Label { get; }
        IClass<T> Classify(T id);
        IClass<T> Classify(IEnumerable<T> id);

        bool Contains(T id);

        IClass<T> Declassify(T id);
        IClass<T> Declassify(IEnumerable<T> id);
    }
}
