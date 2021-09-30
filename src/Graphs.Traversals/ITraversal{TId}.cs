using System;
using System.Collections.Generic;

namespace Graphs.Traversals
{
    public interface ITraversal<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IAsyncEnumerable<Node<TId>> TraverseAsync(Node<TId> origin, int depth);

        IAsyncEnumerable<Node<TId>> TraverseAsync(Node<TId> origin, int depth, Func<Node<TId>, bool> predicate);
    }
}
