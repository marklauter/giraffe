using System;

namespace Graph.Elements
{
    public interface IElement<T>
        : IIdentifiable<T>
        , IClassifiable
        , IQuantifiable
        , IQualifiable
        , ICloneable
        where T : struct, IComparable, IComparable<T>, IEquatable<T>, IFormattable
    {
    }
}
