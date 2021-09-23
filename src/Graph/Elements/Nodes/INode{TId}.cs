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

        INode<TId> Connect(IEdge<TId> edge);
        INode<TId> Disconnect(IEdge<TId> edge);

        bool IsAdjacent(TId targetId);
        bool IsAdjacent(INode<TId> node);

        bool IsIncident(TId edgeId);
        bool IsIncident(IEdge<TId> edge);
    }
}
