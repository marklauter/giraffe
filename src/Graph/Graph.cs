using Graphs.Data;
using Graphs.Elements.Mutables;
using Graphs.Elements.Queriables;
using Graphs.Elements.Traversables;
using Graphs.Traversals;
using System;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IGraph<TId>
        : IMutableSource<TId>
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
        private readonly GraphDocumentContext documentContext;

        public Graph(GraphDocumentContext documentContext)
        {
            this.documentContext = documentContext ?? throw new ArgumentNullException(nameof(documentContext));
        }

        public bool IsEmpty => this.documentContext.Nodes.IsEmpty;

        public Task<TElement> GetMutableAsync<TElement>(TId id) where TElement : IMutable<TId>
        {
            throw new NotImplementedException();
        }

        public Task<TQueriable> GetQueriableAsync<TQueriable>(TId id) where TQueriable : IQueriable<TId>
        {
            throw new NotImplementedException();
        }

        public Task<TTraversable> GetTraversableAsync<TTraversable>(TId id) where TTraversable : ITraversable<TId>
        {
            throw new NotImplementedException();
        }
    }
}
