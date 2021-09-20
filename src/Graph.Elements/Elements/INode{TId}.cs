using System;
using System.Collections.Generic;

namespace Graphs.Elements
{
    public interface INode<TId>
        : IMutableElement<TId>
        , ICoupledEventSource<TId>
        , ITraversableElement<TId>
        , IEquatable<INode<TId>>
        , IEqualityComparer<INode<TId>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
