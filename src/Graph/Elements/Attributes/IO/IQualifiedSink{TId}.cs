using Graphs.IO;
using System;

namespace Graphs.Attributes
{
    public interface IQualifiedSink<TId>
        : IComponentSink<IQualified<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
