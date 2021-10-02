using Graphs.Identifiers;
using System;
using System.Threading.Tasks;

namespace Graphs.Collections
{
    public interface IElementSource<TElement, TId>
        where TElement : IIdentifiable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<TElement> RetrieveAsync(TId id);
    }
}
