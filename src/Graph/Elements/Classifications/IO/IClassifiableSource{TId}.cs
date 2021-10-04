using Graphs.IO;
using System;

namespace Graphs.Classifications
{
    public interface IClassifiableSource<TId>
        : IComponentSource<IClassifiable<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
