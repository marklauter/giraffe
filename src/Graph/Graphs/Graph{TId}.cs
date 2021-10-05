//using Graphs.Events;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Graphs
//{
//    public class Graph<TId>
//        : IGraph<TId>
//        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
//    {
//        private readonly IIdGenerator<TId> idGenerator;
//        private readonly IElementCollection<TId> elements;

//        public bool IsEmpty { get; }

//        public event EventHandler<ElementChangedEventArgs<TId>> ElementChanged;

//        public event EventHandler<ConnectedEventArgs<TId>> Connected;
//        public event EventHandler<DisconnectedEventArgs<TId>> Disconnected;

//        public event EventHandler<NodeAddedEventArgs<TId>> NodeAdded;
//        public event EventHandler<NodeRemovedEventArgs<TId>> NodeRemoved;

//        public Graph(IIdGenerator<TId> idGenerator, IElementCollection<TId> elementCollection)
//        {
//            this.idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
//            this.elements = elementCollection ?? throw new ArgumentNullException(nameof(elementCollection));
//        }

//        public async Task<Node<TId>> AddNodeAsync()
//        {
//            var node = Node<TId>.NewNode(this.idGenerator.NewId());

//            node.Classified += this.Element_Classified;
//            node.Declassified += this.Element_Declassified;
//            node.Qualified += this.Element_Qualified;
//            node.Disqualified += this.Element_Disqualified;

//            await this.elements.AddAsync(node);

//            this.NodeAdded?.Invoke(this, new NodeAddedEventArgs<TId>(node));

//            return node;
//        }

//        public async Task<Edge<TId>> ConnectAsync(Node<TId> source, Node<TId> target)
//        {
//            var edge = Edge<TId>.NewEdge(this.idGenerator.NewId(), source, target);

//            edge.Classified += this.Element_Classified;
//            edge.Declassified += this.Element_Declassified;
//            edge.Qualified += this.Element_Qualified;
//            edge.Disqualified += this.Element_Disqualified;

//            await this.elements.AddAsync(edge);

//            this.Connected?.Invoke(this, new ConnectedEventArgs<TId>(source, target, edge));

//            return edge;
//        }

//        public async Task DisconnectAsync(Edge<TId> edge)
//        {
//            var source = await this.elements.GetNodeAsync(edge.SourceId);
//            await this.DisconnectAsync(edge, source);
//        }

//        public async Task<Edge<TId>> GetEdgeAsync(TId edgeId)
//        {
//            var edge = await this.elements.GetEdgeAsync(edgeId);
//            var clone = edge.Clone() as Edge<TId>;

//            clone.Classified += this.Element_Classified;
//            clone.Declassified += this.Element_Declassified;
//            clone.Qualified += this.Element_Qualified;
//            clone.Disqualified += this.Element_Disqualified;

//            return clone;
//        }

//        public async Task<Node<TId>> GetNodeAsync(TId nodeId)
//        {
//            var node = await this.elements.GetNodeAsync(nodeId);
//            var clone = node.Clone() as Node<TId>;

//            clone.Classified += this.Element_Classified;
//            clone.Declassified += this.Element_Declassified;
//            clone.Qualified += this.Element_Qualified;
//            clone.Disqualified += this.Element_Disqualified;

//            return clone;
//        }

//        public async Task RemoveNodeAsync(TId nodeId)
//        {
//            var node = await this.elements.GetNodeAsync(nodeId);
//            await this.RemoveNodeAsync(node);
//        }

//        public async Task RemoveNodeAsync(Node<TId> node)
//        {
//            var edgeTasks = new List<Task<Edge<TId>>>(node.EdgeCount);
//            foreach (var edgeId in node.Edges)
//            {
//                edgeTasks.Add(this.elements.GetEdgeAsync(edgeId));
//            }

//            var edges = await Task.WhenAll(edgeTasks);

//            var nodeTasks = new List<Task>(node.Degree);
//            foreach (var edge in edges)
//            {
//                nodeTasks.Add(this.DisconnectAsync(edge, node));
//            }

//            await Task.WhenAll(nodeTasks);

//            await this.elements.RemoveAsync(node);
//            this.NodeRemoved?.Invoke(this, new NodeRemovedEventArgs<TId>(node));
//        }

//        private async Task DisconnectAsync(Edge<TId> edge, Node<TId> source)
//        {
//            var target = await this.elements.GetNodeAsync(edge.SourceId);
//            source.Disconnect(edge);
//            target.Disconnect(edge);
//            await this.elements.RemoveAsync(edge);
//            this.Disconnected?.Invoke(this, new DisconnectedEventArgs<TId>(source, target, edge));
//        }

//        private void Element_Disqualified(object sender, DisqualifiedEventArgs<TId> e)
//        {
//            this.ElementChanged?.Invoke(this, new ElementChangedEventArgs<TId>(e.ElementId, e));
//        }

//        private void Element_Qualified(object sender, QualifiedEventArgs<TId> e)
//        {
//            this.ElementChanged?.Invoke(this, new ElementChangedEventArgs<TId>(e.ElementId, e));
//        }

//        private void Element_Declassified(object sender, DeclassifiedEventArgs<TId> e)
//        {
//            this.ElementChanged?.Invoke(this, new ElementChangedEventArgs<TId>(e.ElementId, e));
//        }

//        private void Element_Classified(object sender, ClassifiedEventArgs<TId> e)
//        {
//            this.ElementChanged?.Invoke(this, new ElementChangedEventArgs<TId>(e.ElementId, e));
//        }
//    }
//}
