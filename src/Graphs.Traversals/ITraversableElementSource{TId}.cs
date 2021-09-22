using Graphs.Elements;
using System;

namespace Graphs.Traversals
{
    public interface ITraversableElementSource<TId>
        : INodeSource<TId>
        , IEdgeSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
