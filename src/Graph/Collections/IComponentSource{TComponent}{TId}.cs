using Graphs.Identifiers;
using System;
using System.Threading.Tasks;

namespace Graphs.Collections
{
    public interface IComponentSource<TComponent, TId>
        where TComponent : IIdentifiable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<TComponent> RetrieveAsync(TId id);
    }
}
