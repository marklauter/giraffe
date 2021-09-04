using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graph.Elements.Test
{
    public sealed class ConcreteElement
        : Element
        , IEquatable<ConcreteElement>
        , IEqualityComparer<ConcreteElement>
    {
        public ConcreteElement() : base() { }

        private ConcreteElement(ConcreteElement other)
            : base(other)
        {
        }

        public override object Clone()
        {
            return new ConcreteElement(this);
        }

        public override bool Equals(object obj)
        {
            return obj is ConcreteElement element
                && this.Equals(element);
        }

        public bool Equals(ConcreteElement other)
        {
            return other != null
                && other.Id == this.Id;
        }

        public bool Equals(ConcreteElement x, ConcreteElement y)
        {
            return x != null && x.Equals(y) || y == null;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }

        public int GetHashCode([DisallowNull] ConcreteElement obj)
        {
            return obj.GetHashCode();
        }
    }
}
