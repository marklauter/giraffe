using Graphs.Sources;
using System;

namespace Graphs.Attributes
{
    public interface IQualifiedElementSource<TId>
        : IElementSource<QualifiedElement<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
