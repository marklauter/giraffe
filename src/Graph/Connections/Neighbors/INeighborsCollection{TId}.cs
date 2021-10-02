using Graphs.Collections;
using System;

namespace Graphs.Connections
{
    public interface INeighborsCollection<TId>
        : IElementCollection<Neighbors<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
