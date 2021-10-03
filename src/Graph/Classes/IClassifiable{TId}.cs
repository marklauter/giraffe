using System;

namespace Graphs.Classes
{
    public interface IClassifiable<TId>
        : IClassified<TId>
        , IClassifiableEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        void Classify(string label);

        void Declassify(string label);
    }
}
