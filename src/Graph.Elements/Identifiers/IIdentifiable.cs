using System;
using System.ComponentModel.DataAnnotations;

namespace Graph.Elements
{
    public interface IIdentifiable<T>
        where T : struct, IComparable, IComparable<T>, IEquatable<T>, IFormattable
    {
        [Key]
        T Id { get; }
    }
}
