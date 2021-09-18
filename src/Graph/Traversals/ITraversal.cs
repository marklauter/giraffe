using Graphs.Elements;
using Graphs.Elements.Traversals;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Graphs.Traversals
{
    public interface ITraversal<TId, TTraversable>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
        where TTraversable : ITraversable<TId>
    {
        IAsyncEnumerable<TTraversable> TraverseAsync(TTraversable origin, int depth);

        IAsyncEnumerable<TTraversable> TraverseAsync(
            TTraversable origin, 
            int depth,
            Func<TTraversable, bool> predicate);

        //Task<IEnumerable<TTraversable>> TraverseAsync(TTraversable origin, int depth, Func<Edge, bool> predicate);

        //Task<IEnumerable<TTraversable>> TraverseAsync(
        //    TTraversable origin,
        //    int depth,
        //    Func<Node, bool> nodePredicate,
        //    Func<Edge, bool> edgePredicate);
    }
}
