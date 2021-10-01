using System;
using System.Threading.Tasks;

namespace Graphs.Elements
{
    public interface IEdgeSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<Edge<TId>> GetEdgeAsync(TId edgeId);
    }
}
