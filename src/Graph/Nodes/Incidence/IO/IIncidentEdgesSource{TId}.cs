using Graphs.IO;
using System;

namespace Graphs.Incidence
{
    public interface IIncidentEdgesSource<TId>
        : IComponentSource<IMutableIncidenceList<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
