//using System;

//namespace Graphs.Events
//{
//    public class NodeAddedEventArgs<TId>
//        : GraphEventArgs
//    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
//    {
//        public NodeAddedEventArgs(Node<TId> node)
//            : base()
//        {
//            this.Node = node ?? throw new ArgumentNullException(nameof(node));
//        }

//        public Node<TId> Node { get; }
//    }
//}
