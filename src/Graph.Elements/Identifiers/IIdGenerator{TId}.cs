using System;

namespace Graphs.Elements.Identifiers
{
    public interface IIdGenerator<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        TId New();
    }
}
