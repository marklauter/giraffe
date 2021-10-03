using System.Collections;
using System.Collections.Generic;

namespace Graphs.Classes
{
    public sealed partial class Classifiable<TId>
        : IEnumerable<string>
    {
        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>)this.labels).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
