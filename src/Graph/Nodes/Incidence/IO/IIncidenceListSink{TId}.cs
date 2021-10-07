using Graphs.IO;
using System;

namespace Graphs.Incidence
{
    public interface IIncidenceListSink<TId>
        : IComponentSink<IIncidenceList<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
