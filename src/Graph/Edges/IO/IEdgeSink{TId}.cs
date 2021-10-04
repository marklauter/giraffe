using Graphs.IO;
using System;

namespace Graphs.Edges
{
    public interface IEdgeSink<TId>
        : IComponentSink<IEdge<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
