using Graphs.Data;
using Graphs.Elements;
using Graphs.Elements.Traversables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IGraph
        : IElementSource<Guid>
        , ITraversableSource<Guid>
    {
        bool IsEmpty { get; }

        Task<Node> NewNodeAsync();

        Task<Node> NewNodeAsync(Guid nodeId);

        Task AddAsync(Node node);

        Task AddAsync(IEnumerable<Node> nodes);
    }

    public class Graph
        : IGraph
    {
        private readonly GraphDocumentContext documentContext;

        public Graph(GraphDocumentContext documentContext)
        {
            this.documentContext = documentContext ?? throw new ArgumentNullException(nameof(documentContext));
        }

        public bool IsEmpty => this.documentContext.Nodes.IsEmpty;
    }
}
