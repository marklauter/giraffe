using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace MemoryMappedFileExperiments
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var mmNodeFile = MemoryMappedFile
                .CreateFromFile("nodes.dat", FileMode.OpenOrCreate, null, 1024 * 1024 * 5);
            using var mmNodeViewStream = mmNodeFile
                .CreateViewStream(0x00, 1024 * 1024);

            using var mmEdgeFile = MemoryMappedFile
                .CreateFromFile("edges.dat", FileMode.OpenOrCreate, null, 1024 * 1024 * 5);
            using var mmEdgeViewStream = mmEdgeFile
                .CreateViewStream(0x00, 1024 * 1024);


            using var nodeAccessor = mmNodeFile.CreateViewAccessor(0, 1024 * 1024);

            var nodeSize = Marshal.SizeOf(typeof(Node));
            var node = new Node(1, false, 1);
            nodeAccessor.Write(0, ref node);
            node = new Node(2, false, 2);
            nodeAccessor.Write(nodeSize, ref node);



            //using var nodeMemoryStream = new MemoryStream();
            //using var edgeMemoryStream = new MemoryStream();
            //using var nodeStream = new NodeStream(mmNodeViewStream);
            //using var edgeStream = new EdgeStream(mmEdgeViewStream, nodeStream);
            
            //var nodes = new List<NodeRecord>(10);
            //for (var i = 0; i < 10; ++i)
            //{
            //    nodes.Add(nodeStream.Add());
            //}

            //edgeStream.Connect(nodes[0].Offset, nodes[1].Offset);
            //edgeStream.Connect(nodes[0].Offset, nodes[2].Offset);

            //var node1 = nodeStream.Read(nodes[0].Offset);
            //var node2 = nodeStream.Read(nodes[1].Offset);
            //if (node1.FirstEdgeOffset == NodeStream.DefaultFirstEdgeOffset)
            //    throw new InvalidOperationException("bad edge");

            //if (node2.FirstEdgeOffset == NodeStream.DefaultFirstEdgeOffset)
            //    throw new InvalidOperationException("bad edge");

            //var edge1 = edgeStream.Read(node1.FirstEdgeOffset);
            //var edge2 = edgeStream.Read(node2.FirstEdgeOffset);
            //var edge3 = edgeStream.Read(edge1.NextRecordOffset);

            // todo: next step would be to run a breadth first traversal down the linked list of edges
        }
    }
}

