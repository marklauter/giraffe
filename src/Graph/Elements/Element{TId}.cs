using Graphs.Attributes;
using Graphs.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Graphs.Elements
{
    public interface IElementCollection<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {

    }

    public interface IElement<TId>
        : IClassifiable<TId>
        , IClassifiableEventSource<TId>
        , IQualifiable<TId>
        , IQualifiableEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }

    [DebuggerDisplay("{Id}")]
    public abstract class Element<TId>
        : IElement<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IClassifiable<TId> classifiedElement;
        private readonly IQualifiable<TId> qualifiedElement;
        private readonly IClassifiableEventSource<TId> classifiableEventSource;
        private readonly IQualifiableEventSource<TId> qualifiableEventSource;

        protected Element(TId id)
        {

        }

        protected Element(
            Classifiable<TId> classifiedElement,
            Qualifiable<TId> qualifiedElement)
        {
            this.classifiedElement = classifiedElement ?? throw new ArgumentNullException(nameof(classifiedElement));
            this.qualifiedElement = qualifiedElement ?? throw new ArgumentNullException(nameof(qualifiedElement));
            this.classifiableEventSource = classifiedElement;
            this.qualifiableEventSource = qualifiedElement;
        }

        public TId Id => this.classifiedElement.Id;

        public event EventHandler<ClassifiedEventArgs<TId>> Classified
        {
            add
            {
                this.classifiableEventSource.Classified += value;
            }

            remove
            {
                this.classifiableEventSource.Classified -= value;
            }
        }

        public event EventHandler<DeclassifiedEventArgs<TId>> Declassified
        {
            add
            {
                this.classifiableEventSource.Declassified += value;
            }

            remove
            {
                this.classifiableEventSource.Declassified -= value;
            }
        }

        public event EventHandler<QualifiedEventArgs<TId>> Qualified
        {
            add
            {
                this.qualifiableEventSource.Qualified += value;
            }

            remove
            {
                this.qualifiableEventSource.Qualified -= value;
            }
        }

        public event EventHandler<DisqualifiedEventArgs<TId>> Disqualified
        {
            add
            {
                this.qualifiableEventSource.Disqualified += value;
            }

            remove
            {
                this.qualifiableEventSource.Disqualified -= value;
            }
        }

        public void Classify(string label)
        {
            this.classifiedElement.Classify(label);
        }

        public object Clone()
        {
            return this.classifiedElement.Clone();
        }

        public void Declassify(string label)
        {
            this.classifiedElement.Declassify(label);
        }

        public void Disqualify(string name)
        {
            this.qualifiedElement.Disqualify(name);
        }

        public bool Equals(string name, object other)
        {
            return this.qualifiedElement.Equals(name, other);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.classifiedElement.GetEnumerator();
        }

        public bool HasAttribute(string name)
        {
            return this.qualifiedElement.HasAttribute(name);
        }

        public bool Is(string label)
        {
            return this.classifiedElement.Is(label);
        }

        public bool Is(IEnumerable<string> labels)
        {
            return this.classifiedElement.Is(labels);
        }

        public void Qualify(string name, object value)
        {
            this.qualifiedElement.Qualify(name, value);
        }

        public bool TryGetValue(string name, out object value)
        {
            return this.qualifiedElement.TryGetValue(name, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.classifiedElement).GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return this.qualifiedElement.GetEnumerator();
        }
    }
}
