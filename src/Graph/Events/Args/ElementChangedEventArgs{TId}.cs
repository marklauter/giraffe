using Graphs.Elements;
using System;

namespace Graphs.Events
{
    public enum Delta
    {
        Classified,
        Declassified,
        Qualified,
        Disqualified,
    }

    public class ElementChangedEventArgs<TId>
        : GraphEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public ElementChangedEventArgs(Element<TId> element, ClassifiedEventArgs<TId> classifiedEventArgs)
            : base()
        {
            this.Element = element ?? throw new ArgumentNullException(nameof(element));
            this.ClassifiedEventArgs = classifiedEventArgs ?? throw new ArgumentNullException(nameof(classifiedEventArgs));
            this.Change = Delta.Classified;
        }

        public ElementChangedEventArgs(Element<TId> element, DeclassifiedEventArgs<TId> declassifiedEventArgs)
            : base()
        {
            this.Element = element ?? throw new ArgumentNullException(nameof(element));
            this.DeclassifiedEventArgs = declassifiedEventArgs ?? throw new ArgumentNullException(nameof(declassifiedEventArgs));
            this.Change = Delta.Declassified;
        }

        public ElementChangedEventArgs(Element<TId> element, QualifiedEventArgs<TId> qualifiedEventArgs)
            : base()
        {
            this.Element = element ?? throw new ArgumentNullException(nameof(element));
            this.QualifiedEventArgs = qualifiedEventArgs ?? throw new ArgumentNullException(nameof(qualifiedEventArgs));
            this.Change = Delta.Qualified;
        }

        public ElementChangedEventArgs(Element<TId> element, DisqualifiedEventArgs<TId> disqualifiedEventArgs)
            : base()
        {
            this.Element = element ?? throw new ArgumentNullException(nameof(element));
            this.DisqualifiedEventArgs = disqualifiedEventArgs ?? throw new ArgumentNullException(nameof(disqualifiedEventArgs));
            this.Change = Delta.Disqualified;
        }

        public Element<TId> Element { get; }
        public DisqualifiedEventArgs<TId> DisqualifiedEventArgs { get; }
        public QualifiedEventArgs<TId> QualifiedEventArgs { get; }
        public ClassifiedEventArgs<TId> DeclassifiedEventArgs { get; }
        public ClassifiedEventArgs<TId> ClassifiedEventArgs { get; }

        public Delta Change { get; }
    }
}
