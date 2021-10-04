using Graphs.Identifiers;
using System;
using System.Threading.Tasks;

namespace Graphs.IO
{
    public interface IComponentSink<TComponent, TId>
        where TComponent : IIdentifiable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task WriteAsync(TComponent element);

        Task RemoveAsync(TId elementId);
    }
}
