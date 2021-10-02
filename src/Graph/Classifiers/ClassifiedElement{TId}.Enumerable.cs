using System;
using System.Collections;
using System.Collections.Generic;

namespace Graphs.Classifiers
{
    public sealed partial class ClassifiedElement<TId>
        : IEnumerable<string>
    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IEnumerator<string> GetEnumerator()
        {
            foreach (var label in this.labels)
            {
                yield return label;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
