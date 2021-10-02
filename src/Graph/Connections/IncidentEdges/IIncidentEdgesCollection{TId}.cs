using Graphs.Collections;
using System;

namespace Graphs.Connections
{
    public interface IIncidentEdgesCollection<TId>
    : IElementCollection<IncidentEdges<TId>, TId>
    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
