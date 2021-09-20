﻿using System;
using System.Threading.Tasks;

namespace Graphs.Elements
{
    public interface IMutableElementSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Task<TMutable> GetMutableAsync<TMutable>(TId id) where TMutable : IMutableElement<TId>;
    }
}
