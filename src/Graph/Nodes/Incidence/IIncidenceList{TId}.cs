using Graphs.Connections;
using System;
using System.Collections.Generic;

namespace Graphs.Incidence
{
    public interface IIncidenceList<TId>
        : IConnected<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IEnumerable<TId> Edges { get; }
    }
}
