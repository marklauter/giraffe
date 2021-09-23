using System;
using System.Collections.Generic;

namespace Graphs.Elements
{
    public interface INode<TId>
        : IElement<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        int Degree { get; }

        IEnumerable<TId> Neighbors { get; }

        IEnumerable<TId> IncidentEdges { get; }

        bool IsAdjacent(TId targetId);

        bool IsIncident(TId edgeId);
    }
}
