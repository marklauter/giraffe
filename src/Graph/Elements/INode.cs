using System;
using System.Collections.Generic;

namespace Graph.Elements
{
    // todo: I think the key to removing serialization from the core models is to write interfaces for them and then have the datalayer implement decorator classes based on the interfaces and implemented through a field of the core model class.
    // todo: experiment with above idea in the json experiments project

    public interface INode
        : IElement<Guid>
        , IEquatable<INode>
        , IEqualityComparer<INode>
    {
        public int Degree();
        bool IsAdjacent(Guid nodeId);
        bool IsAdjacent(INode node);
        public IEnumerable<Guid> Neighbors();
        bool TryCouple(INode node);
        bool TryDecouple(INode node);
    }
}
