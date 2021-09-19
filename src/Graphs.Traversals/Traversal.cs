using Graphs.Elements.Queriables;
using Graphs.Elements.Traversables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graphs.Traversals
{
    public class Traversal<TId>
        : ITraversal<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly ITraversableSource<TId> traversableSource;
        private readonly IQueriableSource<TId> queriableSource;

        public Traversal(ITraversableSource<TId> traversableSource, IQueriableSource<TId> queriableSource)
        {
            this.traversableSource = traversableSource ?? throw new ArgumentNullException(nameof(traversableSource));
            this.queriableSource = queriableSource ?? throw new ArgumentNullException(nameof(queriableSource));
        }

        public async IAsyncEnumerable<ITraversable<TId>> TraverseAsync(ITraversable<TId> origin, int depth)
        {
            var visited = new HashSet<TId>(new[] { origin.Id });
            var frontier = await this.ReadFrontierAsync(origin.Neighbors);

            var rank = 1;
            while (frontier.Any() && rank <= depth)
            {
                var tasks = new List<Task<IEnumerable<ITraversable<TId>>>>();
                foreach (var traversable in frontier)
                {
                    yield return traversable;

                    _ = visited.Add(traversable.Id);
                    var ids = traversable.Neighbors.Where(n => !visited.Contains(n));
                    tasks.Add(this.ReadFrontierAsync(ids));
                }

                ++rank;
                frontier = (await Task.WhenAll(tasks.ToArray())).SelectMany(f => f);
            }
        }

        public async IAsyncEnumerable<ITraversable<TId>> TraverseAsync(
            ITraversable<TId> origin, int depth, Func<ITraversable<TId>, bool> predicate)
        {
            var visited = new HashSet<TId>(new[] { origin.Id });
            var frontier = await this.ReadFrontierAsync(origin.Neighbors);
            frontier = Filter(frontier, predicate);

            var rank = 1;
            while (frontier.Any() && rank <= depth)
            {
                var tasks = new List<Task<IEnumerable<ITraversable<TId>>>>();
                foreach (var traversable in frontier)
                {
                    yield return traversable;

                    _ = visited.Add(traversable.Id);
                    var ids = traversable.Neighbors.Where(n => !visited.Contains(n));
                    tasks.Add(this.ReadFrontierAsync(ids));
                }

                ++rank;
                frontier = (await Task.WhenAll(tasks.ToArray())).SelectMany(f => f);
                frontier = Filter(frontier, predicate);
            }
        }

        private async Task<IEnumerable<ITraversable<TId>>> ReadFrontierAsync(IEnumerable<TId> ids)
        {
            var tasks = ids.Select(id => this.traversableSource.GetTraversableAsync<ITraversable<TId>>(id)).ToArray();
            return await Task.WhenAll(tasks);
        }

        private static IEnumerable<ITraversable<TId>> Filter(
            IEnumerable<ITraversable<TId>> frontier,
            Func<ITraversable<TId>, bool> predicate)
        {
            return frontier.Where(predicate);
        }
    }
}
