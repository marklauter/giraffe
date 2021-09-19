using System;
using System.Threading.Tasks;

namespace Graphs.Elements.Queriables
{
    public interface IQueriableSource<TId>
    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<TQueriable> GetQueriableAsync<TQueriable>(TId id) where TQueriable : IQueriable<TId>;
    }
}
