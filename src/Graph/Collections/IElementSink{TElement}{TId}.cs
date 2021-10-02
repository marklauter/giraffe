using Graphs.Identifiers;
using System;
using System.Threading.Tasks;

namespace Graphs.Collections
{
    public interface IElementSink<TElement, TId>
        where TElement : IIdentifiable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task AddAsync(TElement element);

        Task RemoveAsync(TId elementId);
    }
}
