using Graphs.Collections;
using System;

namespace Graphs.Edges
{
    public interface IEdgeCollection<TId>
        : IComponentCollection<Edge<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
