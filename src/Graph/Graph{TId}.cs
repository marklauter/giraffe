using Graphs.Elements;
using Graphs.Elements.Edges;
using Graphs.Elements.Nodes;
using Graphs.Events;
using System;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IGraph<TId>
        : IElementSource<TId>
        , IGraphEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        bool IsEmpty { get; }

        Task<INode<TId>> NewNode();

        Task<IEdge<TId>> Connect(INode<TId> source, INode<TId> target);
    }

    //public class Graph<TId>
    //    : IGraph<TId>
    //    where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    //{
    //    private readonly IDocumentContext<TId> documentContext;

    //    public Graph(IDocumentContext<TId> documentContext)
    //    {
    //        this.documentContext = documentContext ?? throw new ArgumentNullException(nameof(documentContext));
    //    }

    //    public bool IsEmpty => this.documentContext.Nodes.IsEmpty;

    //    public async Task<Edge<TId>> Couple(Node<TId> source, Node<TId> target)
    //    {
    //        var edge = Edge<TId>.Couple(this.documentContext.IdGenerator, source, target);
    //        await this.documentContext.Edges.AddAsync((Document<Edge<TId>>)edge);
    //        return edge;
    //    }

    //    public async Task<Node<TId>> NewNodeAsync()
    //    {
    //        var node = Node<TId>.New(this.documentContext.IdGenerator);
    //        node.Classified += this.Node_Classified;
    //        node.Declassified += this.Node_Declassified;
    //        node.Disqualified += this.Node_Disqualified;
    //        node.Qualified += this.Node_Qualified;
    //        node.Coupled += this.Node_Coupled;
    //        node.Decoupled += this.Node_Decoupled;
    //        await this.documentContext.Nodes.AddAsync((Document<Node<TId>>)node);
    //        return node;
    //    }

    //    private void Node_Decoupled(object sender, DecoupledEventArgs<TId> e)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private void Node_Coupled(object sender, CoupledEventArgs<TId> e)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private void Node_Qualified(object sender, Elements.Qualifiers.QualifiedEventArgs e)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private void Node_Disqualified(object sender, Elements.Qualifiers.DisqualifiedEventArgs e)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private void Node_Declassified(object sender, Elements.Classifiers.DeclassifiedEventArgs e)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private void Node_Classified(object sender, Elements.Classifiers.ClassifiedEventArgs e)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<INode<TId>> GetNodeAsync(TId id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEdge<TId>> GetEdgeAsync(TId id)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
