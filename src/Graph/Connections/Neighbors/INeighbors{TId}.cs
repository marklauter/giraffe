using System;
using System.Collections.Generic;

namespace Graphs.Connections
{
    public interface INeighbors<TId>
        : IConnectable<TId>
        , IEnumerable<KeyValuePair<TId, int>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IEnumerable<TId> Ids { get; }
    }
}
