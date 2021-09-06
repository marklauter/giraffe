using System;
using System.ComponentModel.DataAnnotations;

namespace Graph.Identifiers
{
    public interface IIdentifiable<T>
        : ICloneable
        where T : struct, IComparable, IComparable<T>, IEquatable<T>, IFormattable
    {
        [Key]
        T Id { get; }
    }
}
