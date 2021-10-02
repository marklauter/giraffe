using System;

namespace Graphs.Connections
{
    public interface IAdjacencyList<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        int Degree { get; }

        bool IsAdjacent(TId nodeId);

        int ReferenceCount(TId nodeId);
    }
}
