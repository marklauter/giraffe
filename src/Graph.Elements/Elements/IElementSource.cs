using System;
using System.Threading.Tasks;

namespace Graphs.Elements
{
    public interface IElementSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<TElement> GetElementAsync<TElement>(TId id) where TElement : IMutableElement<TId>;
    }
}
