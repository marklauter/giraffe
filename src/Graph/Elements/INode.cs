using System;
using System.Collections.Generic;

namespace Graph.Elements
{
    // todo: I think the key to removing serialization from the core models is to write interfaces for them and then have the datalayer implement decorator classes based on the interfaces and implemented through a field of the core model class.
    // todo: experiment with above idea in the json experiments project
    // todo: 1: move all the json annotations to a MetadataType class in the data layer so the business layer is free of it
    // todo: 2: See the json experiements unit test project

    public interface INode
        : IElement<Guid>
        , IEquatable<INode>
        , IEqualityComparer<INode>
    {
        // todo: need to raise events for couple and decouple

        public int Degree { get; }
        bool IsAdjacent(Guid nodeId);
        bool IsAdjacent(INode node);
        public IEnumerable<Guid> Neighbors { get; }
        bool TryCouple(INode node);
        bool TryDecouple(INode node);
    }
}
