using System;
using System.Threading.Tasks;

namespace Graphs.Elements.Traversals
{
    public interface ITraversableSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<TTraversable> GetTraversableAsync<TTraversable>(TId id) where TTraversable : ITraversable<TId>;
    }
}
