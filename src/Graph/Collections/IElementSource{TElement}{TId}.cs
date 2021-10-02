using Graphs.Identifiers;
using System;

namespace Graphs.Collections
{
    public interface IElementSource<TElement, TId>
        where TElement : IIdentifiable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        TElement this[TId id] { get; }
    }
}
