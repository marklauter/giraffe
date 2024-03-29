﻿using System;

namespace Graphs.Elements.Qualifiers
{
    public sealed class QualifiedEventArgs
        : EventArgs
    {
        public QualifiedEventArgs(string name, object value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }

        public object Value { get; }
    }
}
