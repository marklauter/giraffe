using System;
using System.Collections;
using System.Collections.Generic;

namespace Graphs.Attributes
{
    public sealed partial class Qualifiable<TId>
        : IEnumerable<KeyValuePair<string, object>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)this.attributes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
