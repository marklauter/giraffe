using Graphs.IO;
using System;

namespace Graphs.Attributes
{
    public interface IQualifiableSource<TId>
        : IComponentSource<IQualifiable<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
