using Graphs.Events;
using System;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IGraph<TId>
        : IElementSource<TId>
        , IGraphEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        bool IsEmpty { get; }

        Task<Node<TId>> AddNodeAsync();

        Task<Edge<TId>> ConnectAsync(Node<TId> source, Node<TId> target);

        Task DisconnectAsync(Edge<TId> edge);

        Task RemoveNodeAsync(Node<TId> node);
    }
}
