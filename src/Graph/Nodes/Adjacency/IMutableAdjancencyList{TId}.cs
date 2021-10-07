using Graphs.Connections;
using System;

namespace Graphs.Adjacency
{
    public interface IMutableAdjacencyList<TId>
        : IAdjacencyList<TId>
        , IConnectable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
