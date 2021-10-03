using Graphs.Collections;
using System;

namespace Graphs.Attributes
{
    public interface IQualifiableComponentCollection<TId>
        : IComponentCollection<Qualifiable<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
