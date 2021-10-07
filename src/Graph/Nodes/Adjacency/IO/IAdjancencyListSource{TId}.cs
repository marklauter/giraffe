﻿using Graphs.IO;
using System;

namespace Graphs.Adjacency
{
    public interface IAdjancencyListSource<TId>
        : IComponentSource<IMutableAdjancencyList<TId>, TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}