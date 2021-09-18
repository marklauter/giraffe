using Graphs.Elements.Traversals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graphs.Traversals
{
    public class Traversal<TId, TTraversable>
        : ITraversal<TId, TTraversable>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
        where TTraversable : ITraversable<TId>
    {
        private readonly ITraversableSource<TId> source;

        public Traversal(ITraversableSource<TId> source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public async IAsyncEnumerable<TTraversable> TraverseAsync(TTraversable origin, int depth)
        {
            var visited = new HashSet<TId>(new[] { origin.Id });
            var frontier = await this.GetFrontierAsync(origin.Neighbors);

            var rank = 1;
            while (frontier.Any() && rank <= depth)
            {
                var ids = new List<TId>();
                foreach (var traversable in frontier)
                {
                    yield return traversable;

                    _ = visited.Add(traversable.Id);
                    ids.AddRange(traversable.Neighbors.Where(n => !visited.Contains(n)));
                }

                ++rank;

                frontier = await this.GetFrontierAsync(ids);
            }
        }

        public async IAsyncEnumerable<TTraversable> TraverseAsync(TTraversable origin, int depth, Func<TTraversable, bool> predicate)
        {
            var visited = new HashSet<TId>(new[] { origin.Id });
            var frontier = await this.GetFrontierAsync(origin.Neighbors);
            frontier = FilterFrontier(frontier, predicate);

            var rank = 1;
            while (frontier.Any() && rank <= depth)
            {
                var ids = new List<TId>();
                foreach (var traversable in frontier)
                {
                    yield return traversable;

                    _ = visited.Add(traversable.Id);
                    ids.AddRange(traversable.Neighbors.Where(n => !visited.Contains(n)));
                }

                ++rank;

                frontier = await this.GetFrontierAsync(ids);
                frontier = FilterFrontier(frontier, predicate);
            }
        }

        private async Task<IEnumerable<TTraversable>> GetFrontierAsync(IEnumerable<TId> ids)
        {
            var tasks = ids.Select(id => this.source.GetTraversableAsync<TTraversable>(id)).ToArray();
            return await Task.WhenAll(tasks);
        }

        private static IEnumerable<TTraversable> FilterFrontier(
            IEnumerable<TTraversable> frontier,
            Func<TTraversable, bool> predicate)
        {
            return frontier.Where(predicate);
        }
    }
}
