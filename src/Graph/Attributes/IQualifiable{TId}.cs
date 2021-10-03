using System;

namespace Graphs.Attributes
{
    public interface IQualifiable<TId>
        : IQualified<TId>
        , IQualifiableEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        void Disqualify(string name);

        void Qualify(string name, object value);
    }
}
