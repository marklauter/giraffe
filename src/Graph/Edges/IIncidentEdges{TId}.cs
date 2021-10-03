using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Connections
{
    public interface IIncidentEdges<TId>
        : IIdentifiable<TId>
        , IConnectable<TId>
        , ICloneable
        , IEnumerable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
