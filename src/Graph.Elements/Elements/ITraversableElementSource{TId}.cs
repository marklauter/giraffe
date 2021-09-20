using System;
using System.Threading.Tasks;

namespace Graphs.Elements
{
    public interface ITraversableElementSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<TTraversable> GetTraversableAsync<TTraversable>(TId id) where TTraversable : ITraversableElement<TId>;
    }
}
