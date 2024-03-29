﻿using System;
using System.Threading.Tasks;

namespace Graphs.Elements
{
    public interface INodeSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<INode<TId>> GetNodeAsync(TId id);
    }
}
