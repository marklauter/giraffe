using System;
using System.Collections.Generic;

namespace Graph.Elements
{
    public interface INode
        : IElement<Guid>
        , IEquatable<INode>
        , IEqualityComparer<INode>
    {
        // todo: need to raise events for couple and decouple

        int Degree { get; }
        IEnumerable<Guid> Neighbors { get; }

        bool IsAdjacent(Guid nodeId);
        bool IsAdjacent(INode node);
        bool TryCouple(INode node);
        bool TryDecouple(INode node);
    }
}
