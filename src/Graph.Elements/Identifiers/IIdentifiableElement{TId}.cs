using System;
using System.ComponentModel.DataAnnotations;

namespace Graphs.Elements
{
    public interface IIdentifiableElement<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [Key]
        TId Id { get; }
    }
}
