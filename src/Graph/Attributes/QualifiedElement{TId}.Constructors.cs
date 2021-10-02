using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Attributes
{
    public sealed partial class QualifiedElement<TId>
        : ICloneable
    {
        public QualifiedElement(TId id)
        {
            this.Id = id;
        }

        public QualifiedElement(IEnumerable<KeyValuePair<string, object>> attributes)
        {
            this.attributes = attributes.ToImmutableDictionary();
        }

        private QualifiedElement(QualifiedElement<TId> other)
        {
            this.attributes = other.attributes;
        }

        public object Clone()
        {
            return new QualifiedElement<TId>(this);
        }
    }
}
