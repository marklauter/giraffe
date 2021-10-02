using System;
using System.Threading.Tasks;

namespace Graphs.Nodes
{
    public interface INodeSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<Node<TId>> GetNodeAsync(TId nodeId);
    }
}
