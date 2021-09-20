using System;
using System.Threading.Tasks;

namespace Graphs.Elements
{
    public interface IQueriableElementSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<TQueriable> GetQueriableAsync<TQueriable>(TId id) where TQueriable : IQueriableElement<TId>;
    }
}
