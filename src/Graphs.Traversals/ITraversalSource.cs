using Graphs.Elements.Queriables;
using Graphs.Elements.Traversables;
using System;

namespace Graphs.Traversals
{
    public interface ITraversalSource<TId>
        : ITraversableSource<TId>
        , IQueriableSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
