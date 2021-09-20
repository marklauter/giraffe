using System;
using System.Threading.Tasks;

namespace Graphs.Elements
{
    public interface IMutableSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<TMutable> GetMutableAsync<TMutable>(TId id) where TMutable : IMutable<TId>;
    }
}
