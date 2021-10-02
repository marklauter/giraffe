using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Attributes
{
    public sealed partial class Qualifiable<TId>
        : ICloneable
    {
        public Qualifiable(TId id)
        {
            this.Id = id;
        }

        public Qualifiable(IEnumerable<KeyValuePair<string, object>> attributes)
        {
            this.attributes = attributes.ToImmutableDictionary();
        }

        private Qualifiable(Qualifiable<TId> other)
        {
            this.attributes = other.attributes;
        }

        public object Clone()
        {
            return new Qualifiable<TId>(this);
        }
    }
}
