﻿using System;
using System.Threading.Tasks;

namespace Graphs.Elements.Edges
{
    public interface IEdgeSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<Edge<TId>> GetEdgeAsync(TId id);
    }
}