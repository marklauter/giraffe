using Graph.Classifiers;
using Graph.Identifiers;
using Graph.Qualifiers;
using Graph.Quantifiers;
using System;

namespace Graph.Elements
{
    public interface IElement<T>
        : IIdentifiable<T>
        , IClassifiable
        , IQuantifiable
        , IQualifiable
        where T : struct, IComparable, IComparable<T>, IEquatable<T>, IFormattable
    {
    }
}
