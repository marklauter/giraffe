using Graphs.Elements.Edges;
using Graphs.Elements.Nodes;
using System;

namespace Graphs.Elements
{
    public interface IElementSource<TId>
        : INodeSource<TId>
        , IEdgeSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
