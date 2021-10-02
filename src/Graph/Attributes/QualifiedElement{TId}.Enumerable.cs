using System;
using System.Collections;
using System.Collections.Generic;

namespace Graphs.Attributes
{
    public sealed partial class QualifiedElement<TId>
        : IEnumerable<KeyValuePair<string, object>>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var kvp in this.attributes)
            {
                yield return kvp;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
