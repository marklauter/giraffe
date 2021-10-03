using Graphs.Attributes;
using Graphs.Classes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Graphs.Edges
{
    public sealed partial class Edge<TId>
        : ICloneable
    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }

    public sealed partial class Edge<TId>
        : IClassified<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IClassified<TId> classified;

        public IEnumerator<string> GetEnumerator()
        {
            return this.classified.GetEnumerator();
        }

        public bool Is(string label)
        {
            return this.classified.Is(label);
        }

        public bool Is(IEnumerable<string> labels)
        {
            return this.classified.Is(labels);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.classified).GetEnumerator();
        }
    }

    public sealed partial class Edge<TId>
        : IEdge<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IQualified<TId> qualified;

        public TId Id { get; }

        public bool IsDirected { get; }

        public TId SourceId { get; }

        public TId TargetId { get; }
    }

    //[DebuggerDisplay("{Id}, ({SourceId}, {TargetId}) Dir: {IsDirected}")]
    //public sealed class Edge<TId>
    //    : Element<TId>
    //    , IEquatable<Edge<TId>>
    //    , IEqualityComparer<Edge<TId>>
    //    , ICloneable
    //    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    //{
    //    internal static Edge<TId> NewEdge(TId id, Node<TId> source, Node<TId> target)
    //    {
    //        if (source is null)
    //        {
    //            throw new ArgumentNullException(nameof(source));
    //        }
    //        else if (target is null)
    //        {
    //            throw new ArgumentNullException(nameof(target));
    //        }

    //        return new Edge<TId>(id, source.Id, target.Id, false);
    //    }

    //    internal static Edge<TId> NewEdge(TId id, Node<TId> source, Node<TId> target, bool isDirected)
    //    {
    //        if (source is null)
    //        {
    //            throw new ArgumentNullException(nameof(source));
    //        }
    //        else if (target is null)
    //        {
    //            throw new ArgumentNullException(nameof(target));
    //        }

    //        return new Edge<TId>(id, source.Id, target.Id, isDirected);
    //    }

    //    public bool IsDirected { get; }

    //    public IEnumerable<TId> Nodes
    //    {
    //        get
    //        {
    //            yield return this.SourceId;
    //            yield return this.TargetId;
    //        }
    //    }

    //    public TId SourceId { get; }

    //    public TId TargetId { get; }

    //    private Edge(Edge<TId> other)
    //        : base(other)
    //    {
    //        this.SourceId = other.SourceId;
    //        this.TargetId = other.TargetId;
    //        this.IsDirected = other.IsDirected;
    //    }

    //    private Edge(TId id, TId sourceId, TId targetId, bool isDirected)
    //        : base(id)
    //    {
    //        this.SourceId = sourceId;
    //        this.TargetId = targetId;
    //        this.IsDirected = isDirected;
    //    }

    //    public Edge(
    //        TId id,
    //        IEnumerable<string> labels,
    //        IEnumerable<KeyValuePair<string, object>> attributes,
    //        TId sourceId,
    //        TId targetId,
    //        bool isDirected)
    //        : base(id, labels, attributes)
    //    {
    //        this.SourceId = sourceId;
    //        this.TargetId = targetId;
    //        this.IsDirected = isDirected;
    //    }

    //    public override object Clone()
    //    {
    //        return new Edge<TId>(this);
    //    }

    //    public bool IsIncident(TId nodeId)
    //    {
    //        return this.SourceId.Equals(nodeId) || this.TargetId.Equals(nodeId);
    //    }

    //    public bool IsIncident(Node<TId> node)
    //    {
    //        return node is null
    //            ? throw new ArgumentNullException(nameof(node))
    //            : this.IsIncident(node.Id);
    //    }

    //    public bool Equals(Edge<TId> other)
    //    {
    //        return other != null
    //            && this.Id.Equals(other.Id)
    //            && this.SourceId.Equals(other.SourceId)
    //            && this.TargetId.Equals(other.TargetId)
    //            && this.IsDirected == other.IsDirected;
    //    }

    //    public bool Equals(Edge<TId> x, Edge<TId> y)
    //    {
    //        return x != null && x.Equals(y);
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        return obj is Edge<TId> edge && this.Equals(edge);
    //    }

    //    public int GetHashCode(Edge<TId> obj)
    //    {
    //        return obj is null
    //            ? throw new ArgumentNullException(nameof(obj))
    //            : obj.GetHashCode();
    //    }

    //    public override int GetHashCode()
    //    {
    //        return HashCode.Combine(
    //            this.Id,
    //            this.SourceId,
    //            this.TargetId,
    //            this.IsDirected);
    //    }
    //}
}
