using System;

namespace Graphs.Identifiers
{
    public interface IIdGenerator<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        TId NewId();
    }
}
