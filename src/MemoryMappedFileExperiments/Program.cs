using System;
using System.Collections.Generic;
using System.IO;

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
            
            var nodes = new List<long>(10);
            for (var i = 0; i < 10; ++i)
            {
                nodes.Add(nodeStream.Add());
            }

            edgeStream.Connect(nodes[0], nodes[1]);
            edgeStream.Connect(nodes[0], nodes[2]);

            var node1 = nodeStream.Read(nodes[0]);
            var node2 = nodeStream.Read(nodes[1]);
            if (node1.FirstEdgeOffset == -1)
                throw new InvalidOperationException("bad edge");

            if (node2.FirstEdgeOffset == -1)
                throw new InvalidOperationException("bad edge");

            var edge1 = edgeStream.Read(node1.FirstEdgeOffset);
            var edge2 = edgeStream.Read(node2.FirstEdgeOffset);

            //for (var i = 0; i < 10; ++i)
            //{
            //    node.Write(nodes[i], new NodeRecord(i, null));
            //}

            var records = new List<NodeRecord>(10);
            for (var i = 0; i < 10; ++i)
            {
                records.Add(nodeStream.Read(nodes[i]));
            }

            // todo: next step would be to run a breadth first traversal down the linked list of edges
        }
    }
}

