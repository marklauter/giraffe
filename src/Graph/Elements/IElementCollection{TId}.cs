using System;
using System.Threading.Tasks;

namespace Graphs.Elements
{
    public interface IElementCollection<TId>
        : IElementSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task AddAsync(Edge<TId> edge);
        Task AddAsync(Node<TId> node);

        Task RemoveAsync(Edge<TId> edge);
        Task RemoveAsync(Node<TId> node);
    }
}
