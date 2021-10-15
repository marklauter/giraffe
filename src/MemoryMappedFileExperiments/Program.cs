using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace MemoryMappedFileExperiments
{
    internal class Program
    {
        /*
        figured out that everything is a linked list in files
        1. can create a binary hash list of the guids and certain offsets in a file 
        2. new items are "inserted" in order
        3. binary traverse when an ID is used for access
        4. edges can be modeled as they are in this stream experiment - an interspersed linked list
        4.a node primary key data contains offset of first edge in list
        5. node primary key looks like guid, next offset, prev offset, first label offset, first edge offset, first property offset
         */

        private static void Main(string[] args)
        {
            BinaryInsertLinkedListExperiment();
        }

        [DebuggerDisplay("{Data} ({Left}, {Right})")]
        private readonly struct ListItem
        {
            public ListItem(long data)
                : this(-1, -1, data)
            {
                this.Data = data;
            }

            public ListItem(long left, long right, long data)
            {
                this.Left = left;
                this.Right = right;
                this.Data = data;
            }

            internal readonly long Left;
            internal readonly long Right;
            internal readonly long Data;
        }

        private const long RootOffset = 0;
        private const int MaxRng = 100;

        private static void BinaryInsertLinkedListExperiment()
        {
            var rng = new Random(DateTime.UtcNow.Millisecond);
            var list = new ListItem[10];

            for (var i = 0; i < 10; ++i)
            {
                list[i] = new ListItem(rng.Next(MaxRng));
                Insert(i, RootOffset, list);
                Console.WriteLine();
            }

            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            Traverse(RootOffset, "root", list);
        }

        private static void Traverse(long offset, string direction, ListItem[] items)
        {
            if (offset < 0)
            {
                return;
            }

            var item = items[offset];
            Console.WriteLine($"{direction}: {offset} : {item.Data}, ({item.Left}, {item.Right})");
            Traverse(item.Left, "left", items);
            Traverse(item.Right, "right", items);
        }

        private static void Insert(long newOffset, long currentOffset, ListItem[] items)
        {
            if (newOffset == currentOffset)
            {
                return;
            }

            var currentItem = items[currentOffset];
            var newItem = items[newOffset];

            Console.WriteLine($"item data: new ({newOffset}, {newItem.Data}), current:  ({currentOffset}, {currentItem.Data}, {currentItem.Left}, {currentItem.Right})");

            if (newItem.Data < currentItem.Data)
            {
                if (currentItem.Left > -1)
                {
                    Console.WriteLine("move left");
                    Insert(newOffset, currentItem.Left, items);
                }
                else
                {
                    Console.WriteLine("insert left");
                    items[currentOffset] = new ListItem(newOffset, currentItem.Right, currentItem.Data);
                }
            }
            else
            {
                if (currentItem.Right > -1)
                {
                    Console.WriteLine("move right");
                    Insert(newOffset, currentItem.Right, items);
                }
                else
                {
                    Console.WriteLine("insert right");
                    items[currentOffset] = new ListItem(currentItem.Left, newOffset, currentItem.Data);
                }
            }
        }

        private static void StreamAndStructExperiment()
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
            {
                throw new InvalidOperationException("bad edge");
            }

            if (node2.FirstEdgeOffset == NodeStream.DefaultFirstEdgeOffset)
            {
                throw new InvalidOperationException("bad edge");
            }

            var edge1 = edgeStream.Read(node1.FirstEdgeOffset);
            var edge2 = edgeStream.Read(node2.FirstEdgeOffset);
            var edge3 = edgeStream.Read(edge1.NextRecordOffset);

            //todo:next step would be to run a breadth first traversal down the linked list of edges
        }

        private static void MMFExperiment()
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
            // https://docs.microsoft.com/en-us/dotnet/standard/memory-and-spans/memory-t-usage-guidelines
            using var nodeAccessor = mmNodeFile.CreateViewAccessor(0, 1024 * 1024);

            var memoryOwner =
                MemoryPool<MemoryMappedNode>.Shared.Rent();
            var memory = memoryOwner.Memory;

            var nodeSize = Marshal.SizeOf(typeof(MemoryMappedNode));

            for (var offset = 0; offset < 10; ++offset)
            {
                var node = new MemoryMappedNode(offset + 1, false, offset + 1);
                nodeAccessor.Write(offset * nodeSize, ref node);
            }
        }
    }
}

