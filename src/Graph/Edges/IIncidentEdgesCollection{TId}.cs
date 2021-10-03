using Graphs.Collections;
using System;

namespace Graphs.Edges
{
    public interface IIncidentEdgesCollection<TId>
        : IComponentCollection<IncidentEdges<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
