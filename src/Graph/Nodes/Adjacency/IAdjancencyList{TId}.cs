using Graphs.Connections;
using System;
using System.Collections.Generic;

namespace Graphs.Adjacency
{
    public interface IAdjancencyList<TId>
        : IConnected<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IEnumerable<TId> Neighbors { get; }

        IEnumerable<KeyValuePair<TId, int>> ReferenceCountedNeighbors { get; }

        int Degree { get; }

        bool IsAdjacent(TId nodeId);

        int NeighborReferenceCount(TId nodeId);
    }
}
