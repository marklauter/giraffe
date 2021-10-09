using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace MemoryMappedFileExperiments
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var nodeMemoryStream = new MemoryStream();
            using var edgeMemoryStream = new MemoryStream();
            using var nodeStream = new NodeStream(nodeMemoryStream);
            using var edgeStream = new EdgeStream(edgeMemoryStream, nodeStream);
            
            var nodes = new List<NodeRecord>(10);
            for (var i = 0; i < 10; ++i)
            {
                nodes.Add(nodeStream.Add());
            }

            edgeStream.Connect(nodes[0].Offset, nodes[1].Offset);
            edgeStream.Connect(nodes[0].Offset, nodes[2].Offset);

            var node1 = nodeStream.Read(nodes[0].Offset);
            var node2 = nodeStream.Read(nodes[1].Offset);
            if (node1.FirstEdgeOffset == NodeStream.DefaultFirstEdgeOffset)
                throw new InvalidOperationException("bad edge");

            if (node2.FirstEdgeOffset == NodeStream.DefaultFirstEdgeOffset)
                throw new InvalidOperationException("bad edge");

            var edge1 = edgeStream.Read(node1.FirstEdgeOffset);
            var edge2 = edgeStream.Read(node2.FirstEdgeOffset);
            var edge3 = edgeStream.Read(edge1.NextRecordOffset);

            // todo: next step would be to run a breadth first traversal down the linked list of edges
        }
    }
}

