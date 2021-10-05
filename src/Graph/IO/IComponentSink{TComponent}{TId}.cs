using Graphs.Identifiers;
using System;
using System.Threading.Tasks;

namespace Graphs.IO
{
    public interface IComponentSink<TComponent, TId>
        where TComponent : IIdentifiable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task RemoveAsync(TId elementId);

        Task WriteAsync(TComponent component);
    }
}
