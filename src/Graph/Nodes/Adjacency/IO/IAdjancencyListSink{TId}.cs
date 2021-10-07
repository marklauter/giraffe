using Graphs.IO;
using System;

namespace Graphs.Adjacency
{
    public interface IAdjancencyListSink<TId>
        : IComponentSink<IAdjancencyList<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
