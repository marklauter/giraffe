using System;
using System.Collections;
using System.Collections.Generic;

namespace Graphs.Connections
{
    public sealed partial class Neighbors<TId>
        : IEnumerable<TId>
    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IEnumerator<TId> GetEnumerator()
        {
            foreach (var id in this.neighbors.Keys)
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
