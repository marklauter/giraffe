using Graphs.IO;
using System;

namespace Graphs.Classifications
{
    public interface IClassifiedSink<TId>
        : IComponentSink<IClassified<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
