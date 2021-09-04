﻿using Graph.ConcurrentCollections;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using System.Threading;

namespace Graph.Elements
{
    [DebuggerDisplay("{Key}")]
    [JsonObject("node")]
    public sealed class Node
        : Element
        , IEquatable<Node>
        , IEqualityComparer<Node>
    {
        private readonly ReaderWriterLockSlim gate = new();

        [JsonProperty]
        private readonly ConcurrentHashSet<Guid> neighbors = new();

        [JsonProperty]
        private readonly ElementIndex index = new();

        // indexed by target node
        [JsonProperty]
        private readonly ConcurrentDictionary<Guid, Guid> edges = new();

        private Node() : base() { }

        private Node([DisallowNull] Node other)
            : base(other)
        {
            this.neighbors = other.neighbors.Clone() as ConcurrentHashSet<Guid>;
        }

        [Pure]
        public bool IsAdjacent(Guid targetId)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.neighbors.Contains(targetId);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        [Pure]
        public bool IsAdjacent(Node target)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.IsAdjacent(target.Id);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        [Pure]
        public override object Clone()
        {
            this.gate.EnterReadLock();
            try
            {
                return new Node(this);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public Edge Couple([DisallowNull] Node target, bool isDirected)
        {
            this.gate.EnterWriteLock();
            try
            {
                Edge edge = null;
                if (this.neighbors.Add(target.Id))
                {
                    edge = new Edge(this, target);
                    _ = this.edges.TryAdd(target.Id, edge.Id);
                    // this.IndexNode(target);

                    if (!isDirected && target.neighbors.Add(target.Id))
                    {
                        _ = target.edges.TryAdd(this.Id, edge.Id);
                        //target.IndexNode(this);
                    }

                    return edge;
                }

                return edge;
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        public bool TryDecouple([DisallowNull] Node target)
        {
            this.gate.EnterWriteLock();
            try
            {
                // todo: need to find the edge and delete it too
                // todo: need to deindex
                return this.neighbors.Remove(target.Id);
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        [Pure]
        public int Degree()
        {
            this.gate.EnterReadLock();
            try
            {
                return this.neighbors.Count;
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        [Pure]
        public bool Equals(Node other)
        {
            return other != null
                && other.Id == this.Id;
        }

        [Pure]
        public bool Equals(Node x, Node y)
        {
            return x != null && x.Equals(y) || y == null;
        }

        [Pure]
        public override bool Equals(object obj)
        {
            return obj is Node node && this.Equals(node);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }

        [Pure]
        public int GetHashCode([DisallowNull] Node obj)
        {
            return obj.GetHashCode();
        }

        [Pure]
        public IEnumerable<Guid> Neighbors()
        {
            this.gate.EnterReadLock();
            try
            {
                foreach (var neighbor in this.neighbors)
                {
                    yield return neighbor;
                }
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
