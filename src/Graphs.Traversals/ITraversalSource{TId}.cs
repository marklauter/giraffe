using Graphs.Elements;
using System;

namespace Graphs.Traversals
{
    public interface ITraversalSource<TId>
        : ITraversableElementSource<TId>
        , IQueriableElementSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
