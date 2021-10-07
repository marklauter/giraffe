using Graphs.Connections;
using System;

namespace Graphs.Adjacency
{
    public interface IMutableAdjancencyList<TId>
        : IAdjancencyList<TId>
        , IConnectable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
