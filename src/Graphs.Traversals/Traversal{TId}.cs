using Graphs.Elements;
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
        private readonly ITraversableElementSource<TId> source;

        public Traversal(ITraversableElementSource<TId> source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public async IAsyncEnumerable<INode<TId>> TraverseAsync(INode<TId> origin, int depth)
        {
            var visited = new HashSet<TId>(new[] { origin.Id });
            var frontier = await this.ReadFrontierAsync(origin.Neighbors);

            var rank = 1;
            while (frontier.Any() && rank <= depth)
            {
                var tasks = new List<Task<IEnumerable<INode<TId>>>>();
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

        public async IAsyncEnumerable<INode<TId>> TraverseAsync(
            INode<TId> origin, int depth, Func<INode<TId>, bool> predicate)
        {
            var visited = new HashSet<TId>(new[] { origin.Id });
            var frontier = await this.ReadFrontierAsync(origin.Neighbors);
            frontier = Filter(frontier, predicate);

            var rank = 1;
            while (frontier.Any() && rank <= depth)
            {
                var tasks = new List<Task<IEnumerable<INode<TId>>>>();
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

        private async Task<IEnumerable<INode<TId>>> ReadFrontierAsync(IEnumerable<TId> ids)
        {
            var tasks = ids.Select(id => this.source.GetNodeAsync(id)).ToArray();
            return await Task.WhenAll(tasks);
        }

        private static IEnumerable<INode<TId>> Filter(
            IEnumerable<INode<TId>> frontier,
            Func<INode<TId>, bool> predicate)
        {
            return frontier.Where(predicate);
        }
    }
}
