using Graphs.Elements;
using System;

namespace Graphs.Traversals
{
    public interface IElementSource<TId>
        : INodeSource<TId>
        , IEdgeSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
