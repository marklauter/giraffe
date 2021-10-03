using Graphs.Collections;
using System;

namespace Graphs.Connections
{
    public interface IIncidentEdgesCollection<TId>
        : IComponentCollection<IIncidentEdges<TId>, TId>
    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
