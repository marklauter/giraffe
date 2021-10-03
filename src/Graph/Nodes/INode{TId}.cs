using Graphs.Connections;
using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Nodes
{
    public interface INode<TId>
        : IIdentifiable<TId>
        , IConnectable<TId>
        , ICloneable
        , IEnumerable<KeyValuePair<TId, int>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IEnumerable<TId> Neighbors { get; }

        int Degree { get; }

        bool IsAdjacent(TId nodeId);

        int NeighborReferenceCount(TId nodeId);
    }
}
