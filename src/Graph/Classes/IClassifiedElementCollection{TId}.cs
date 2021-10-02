using Graphs.Collections;
using System;

namespace Graphs.Classes
{
    public interface IClassifiedElementCollection<TId>
        : IElementCollection<ClassifiedElement<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
