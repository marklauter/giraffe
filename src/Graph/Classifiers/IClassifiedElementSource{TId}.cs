using Graphs.Sources;
using System;

namespace Graphs.Classifiers
{
    public interface IClassifiedElementSource<TId>
        : IElementSource<ClassifiedElement<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
