using Graphs.Elements;
using System;
using System.Collections.Generic;

namespace Graphs.Traversals
{
    public interface ITraversal<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IAsyncEnumerable<ITraversableElement<TId>> TraverseAsync(ITraversableElement<TId> origin, int depth);

        IAsyncEnumerable<ITraversableElement<TId>> TraverseAsync(ITraversableElement<TId> origin, int depth, Func<ITraversableElement<TId>, bool> predicate);
    }
}
