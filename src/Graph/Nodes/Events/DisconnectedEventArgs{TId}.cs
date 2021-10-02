﻿using Graphs.Elements;
using System;

namespace Graphs.Nodes
{
    public sealed class DisconnectedEventArgs<TId>
        : ConnectedEventArgs<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DisconnectedEventArgs(Node<TId> source, Node<TId> target, Edge<TId> edge) : base(source, target, edge)
        {
        }
    }
}