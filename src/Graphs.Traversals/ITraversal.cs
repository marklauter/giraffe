using Graphs.Elements.Traversables;
using System;
using System.Collections.Generic;

namespace Graphs.Traversals
{
    public interface ITraversal<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IAsyncEnumerable<ITraversable<TId>> TraverseAsync(ITraversable<TId> origin, int depth);

        IAsyncEnumerable<ITraversable<TId>> TraverseAsync(ITraversable<TId> origin, int depth, Func<ITraversable<TId>, bool> predicate);
    }
}
