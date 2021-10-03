using System;
using System.Collections;
using System.Collections.Generic;

namespace Graphs.Nodes
{
    public sealed partial class Node<TId>
        : IEnumerable<KeyValuePair<TId, int>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IEnumerator<KeyValuePair<TId, int>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TId, int>>)this.neighbors).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.neighbors).GetEnumerator();
        }
    }
}
