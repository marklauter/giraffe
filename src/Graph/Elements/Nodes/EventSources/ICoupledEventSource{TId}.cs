using System;

namespace Graphs.Elements
{
    public interface ICoupledEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        event EventHandler<CoupledEventArgs<TId>> Coupled;
        event EventHandler<DecoupledEventArgs<TId>> Decoupled;
    }
}
