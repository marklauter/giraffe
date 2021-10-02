using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Classifiers
{
    public sealed partial class ClassifiedElement<TId>
        : ICloneable
    {
        public ClassifiedElement(TId id)
        {
            this.Id = id;
        }

        public ClassifiedElement(IEnumerable<string> labels)
        {
            this.labels = labels.ToImmutableHashSet();
        }

        private ClassifiedElement(ClassifiedElement<TId> other)
        {
            this.labels = other.labels;
        }

        public object Clone()
        {
            return new ClassifiedElement<TId>(this);
        }
    }
}
