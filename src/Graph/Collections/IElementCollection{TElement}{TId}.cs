using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Collections
{
    public interface IElementCollection<TElement, TId>
        : IElementSource<TElement, TId>
        , IElementSink<TElement, TId>
        , IEnumerable<TElement>
        where TElement : IIdentifiable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {

    }
}
