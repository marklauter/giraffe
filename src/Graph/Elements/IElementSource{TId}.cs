using System;

namespace Graphs.Elements
{
    public interface IElementSource<TId>
        : NodeSource<TId>
        , EdgeSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
