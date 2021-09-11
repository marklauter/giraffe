using System;
using System.ComponentModel.DataAnnotations;

namespace Graph.Identifiers
{
    public interface IIdentifiable<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [Key]
        TId Id { get; }
    }
}
