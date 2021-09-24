using Graphs.Elements.Nodes;
using System;
using System.Collections.Generic;

namespace Graphs.Traversals
{
    public interface ITraversal<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IAsyncEnumerable<INode<TId>> TraverseAsync(INode<TId> origin, int depth);

        IAsyncEnumerable<INode<TId>> TraverseAsync(INode<TId> origin, int depth, Func<INode<TId>, bool> predicate);
    }
}
