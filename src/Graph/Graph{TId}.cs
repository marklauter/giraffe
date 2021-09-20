using Graphs.Data;
using Graphs.Elements;
using Graphs.Traversals;
using System;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IGraph<TId>
        : IMutableElementSource<TId>
        , ITraversalSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        bool IsEmpty { get; }

        //Task<Node> NewNodeAsync();

        //Task<Node> NewNodeAsync(Guid nodeId);

        //Task AddAsync(Node node);

        //Task AddAsync(IEnumerable<Node> nodes);
    }

    public class Graph<TId>
        : IGraph<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly GraphDocumentContext<TId> documentContext;

        public Graph(GraphDocumentContext<TId> documentContext)
        {
            this.documentContext = documentContext ?? throw new ArgumentNullException(nameof(documentContext));
        }

        public bool IsEmpty => this.documentContext.Nodes.IsEmpty;

        public Task<TElement> GetMutableAsync<TElement>(TId id) where TElement : IMutableElement<TId>
        {
            throw new NotImplementedException();
        }

        public Task<TQueriable> GetQueriableAsync<TQueriable>(TId id) where TQueriable : IQueriableElement<TId>
        {
            throw new NotImplementedException();
        }

        public Task<TTraversable> GetTraversableAsync<TTraversable>(TId id) where TTraversable : ITraversableElement<TId>
        {
            throw new NotImplementedException();
        }
    }
}
