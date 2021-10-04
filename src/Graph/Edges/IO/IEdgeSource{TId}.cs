using Graphs.IO;
using System;

namespace Graphs.Edges
{
    public interface IEdgeSource<TId>
        : IComponentSource<IEdge<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
