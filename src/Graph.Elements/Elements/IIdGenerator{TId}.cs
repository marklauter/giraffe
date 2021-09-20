using System;

namespace Graphs.Elements
{
    public interface IIdGenerator<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        TId New();
    }
}
