using System;
using System.ComponentModel.DataAnnotations;

namespace Graphs.Identifiers
{
    public interface IIdentifiable<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [Key]
        TId Id { get; }
    }
}
