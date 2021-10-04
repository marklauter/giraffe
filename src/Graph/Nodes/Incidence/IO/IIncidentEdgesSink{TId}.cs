using Graphs.IO;
using System;

namespace Graphs.Incidence
{
    public interface IIncidentEdgesSink<TId>
        : IComponentSink<IIncidenceList<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
