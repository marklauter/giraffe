using Graphs.IO;
using System;

namespace Graphs.Adjacency
{
    public interface IAdjacencyListSource<TId>
        : IComponentSource<IMutableAdjacencyList<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
