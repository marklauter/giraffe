using Graphs.Connections;
using System;

namespace Graphs.Incidence
{
    public interface IMutableIncidenceList<TId>
        : IIncidenceList<TId>
        , IConnectable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
