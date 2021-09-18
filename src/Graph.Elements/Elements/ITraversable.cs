using System;
using System.Collections.Generic;

namespace Graphs.Elements
{
    public interface ITraversable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        int Degree { get; }

        IEnumerable<TId> Neighbors { get; }

        bool IsAdjacent(TId targetId);
        bool IsIncident(TId edgeId);
    }
}
