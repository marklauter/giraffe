using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Collections
{
    public interface IComponentCollection<TComponent, TId>
        : IComponentSource<TComponent, TId>
        , IComponentSink<TComponent, TId>
        , IEnumerable<TComponent>
        where TComponent : IIdentifiable<TId>, ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
