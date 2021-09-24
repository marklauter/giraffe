using Graphs.Elements.Classifiers;
using Graphs.Elements.Nodes;
using Graphs.Elements.Qualifiers;
using System;

namespace Graphs.Events
{
    public interface IGraphEventSource<TId>
        : IClassifierEventSource<TId>
        , IQualifierEventSource<TId>
        , IConnectionsEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        //todo: create event specific args classes
        event EventHandler<EventArgs> NodeAdded;
        event EventHandler<EventArgs> NodeRemoved;
    }
}
