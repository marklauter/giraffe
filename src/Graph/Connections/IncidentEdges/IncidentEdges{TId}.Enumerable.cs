using System;
using System.Collections;
using System.Collections.Generic;

namespace Graphs.Connections
{
    public sealed partial class IncidentEdges<TId>
        : IEnumerable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IEnumerator<TId> GetEnumerator()
        {
            foreach (var id in this.edges)
            {
                yield return id;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
