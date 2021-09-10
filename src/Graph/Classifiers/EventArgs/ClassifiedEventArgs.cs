﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Graph.Classifiers
{
    public class ClassifiedEventArgs
        : EventArgs
    {
        public ClassifiedEventArgs([DisallowNull] string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            this.Label = label;
        }

        public string Label { get; }
    }

    public class ClassifiedEventArgs<TId>
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
