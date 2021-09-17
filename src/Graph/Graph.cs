using Graphs.Data;
using System;

namespace Graphs
{
    public interface IGraph
    {
        bool IsEmpty { get; }
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
