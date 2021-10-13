using System;
using System.Buffers;
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
            // https://github.com/dotnet/runtime/issues/24805
            // https://github.com/GoldenCrystal/MemoryLookupBenchmark/blob/master/MemoryLookupBenchmark/MemoryMappedFileMemory.cs
            // https://gist.github.com/GrabYourPitchforks/8efb15abbd90bc5b128f64981766e834#:~:text=Memory%3CT%3E%20is%20the%20basic%20type%20that%20represents%20a,and%20System.String%20%28in%20the%20case%20of%20ReadOnlyMemory%3Cchar%3E%20%29.
            // https://gist.github.com/GrabYourPitchforks/4c3e1935fd4d9fa2831dbfcab35dffc6
            using var nodeAccessor = mmNodeFile.CreateViewAccessor(0, 1024 * 1024);

            var memoryOwner = 
                MemoryPool<MemoryMappedNode>.Shared.Rent();
            var memory = memoryOwner.Memory;

            var nodeSize = Marshal.SizeOf(typeof(MemoryMappedNode));
            
            for(var offset = 0; offset < 10; ++offset)
            {
                var node = new MemoryMappedNode(offset + 1, false, offset + 1);
                nodeAccessor.Write(offset * nodeSize, ref node);
            }


            var nodeSpan = new Span<NodeRecord>()

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

