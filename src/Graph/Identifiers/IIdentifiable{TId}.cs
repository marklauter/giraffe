using System;

namespace Graphs.Identifiers
{
    public interface IIdentifiable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        TId Id { get; }
    }
}
