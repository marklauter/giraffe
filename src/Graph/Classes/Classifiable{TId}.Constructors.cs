using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Classes
{
    public sealed partial class Classifiable<TId>
        : ICloneable
    {
        public Classifiable(TId id)
        {
            this.Id = id;
        }

        public Classifiable(IEnumerable<string> labels)
        {
            this.labels = labels.ToImmutableHashSet();
        }

        private Classifiable(Classifiable<TId> other)
        {
            this.labels = other.labels;
        }

        public object Clone()
        {
            return new Classifiable<TId>(this);
        }
    }
}
