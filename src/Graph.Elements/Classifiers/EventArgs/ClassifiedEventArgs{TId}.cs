﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Elements.Classifiers
{
    public sealed class ClassifiedEventArgs<TId>
        : ClassifiedEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public ClassifiedEventArgs([DisallowNull] string label, TId id)
            : base(label)
        {
            this.Id = id;
        }

        public TId Id { get; }
    }
}
