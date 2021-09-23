using System;
using System.Collections.Generic;

namespace Graphs.Elements
{
    public interface IEdge<TId>
        : IElement<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        bool IsDirected { get; }

        TId SourceId { get; }

        TId TargetId { get; }

        IEnumerable<TId> Nodes { get; }

        IEdge<TId> Disconnect(INode<TId> node1, INode<TId> node2);

        bool IsIncident(TId nodeId);
        bool IsIncident(INode<TId> node);
    }
}
