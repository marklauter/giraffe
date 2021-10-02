using Graphs.Collections;
using System;

namespace Graphs.Attributes
{
    public interface IQualifiedElementCollection<TId>
        : IElementCollection<QualifiedElement<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
