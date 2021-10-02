using System;
using System.Collections.Generic;

namespace Graphs.Connections
{
    public interface IIncidentEdges<TId>
        : IConnectable<TId>
        , IEnumerable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
