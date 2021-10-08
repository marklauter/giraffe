using System;
using System.IO;
using System.Text;

namespace MemoryMappedFileExperiments
{
    public struct NodeStream : IDisposable
    {
        private readonly BinaryWriter nodeWriter;
        private readonly BinaryReader nodeReader;
        private readonly Stream nodes;

        public NodeStream(Stream nodes)
        {
            this.nodes = nodes;
            this.nodeWriter = new BinaryWriter(nodes, Encoding.UTF8, true);
            this.nodeReader = new BinaryReader(nodes, Encoding.UTF8, true);
        }

        public long Add()
        {
            var offset = this.nodes.Length;
            this.Write(offset, new NodeRecord(0, -1));
            return offset;
        }

        public void Write(long offset, NodeRecord node)
        {
            _ = this.nodes.Seek(offset, SeekOrigin.Begin);
            this.nodeWriter.Write(node.FirstEdgeOffset);
            this.nodeWriter.Write(node.Degree);
        }

        public NodeRecord Read(long offset)
        {
            _ = this.nodes.Seek(offset, SeekOrigin.Begin);
            var firstNeighbor = this.nodeReader.ReadInt64();
            var degree = this.nodeReader.ReadInt32();
            return new NodeRecord(degree, firstNeighbor);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //private long ReadFirstEdgeOffset(long node)
        //{
        //    _ = this.nodes.Seek(node, SeekOrigin.Begin);
        //    return this.nodeReader.ReadInt64();
        //}
    }
}

