using Graphs.Elements.Traversables;
using System;
using System.Collections.Generic;

namespace Graphs.Elements
{
    public interface INode<TId>
        : IMutable<TId>
        , ICoupledEventSource<TId>
        , ITraversable<TId>
        , IEquatable<INode<TId>>
        , IEqualityComparer<INode<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
