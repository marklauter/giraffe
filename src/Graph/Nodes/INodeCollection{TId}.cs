using Graphs.Collections;
using System;

namespace Graphs.Nodes
{
    public interface INodeCollection<TId>
        : IComponentCollection<Node<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
