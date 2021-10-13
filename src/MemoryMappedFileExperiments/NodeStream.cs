using System;
using System.IO;
using System.Text;

namespace MemoryMappedFileExperiments
{
    public class NodeStream : IDisposable
    {
        private readonly BinaryWriter writer;
        private readonly BinaryReader reader;
        private readonly Stream stream;

        public const long DefaultFirstEdgeOffset = -1;
        public const int DefaultDegree = 0;

        public NodeStream(Stream nodes)
        {
            this.stream = nodes;
            this.writer = new BinaryWriter(nodes, Encoding.UTF8, true);
            this.reader = new BinaryReader(nodes, Encoding.UTF8, true);
        }

        public NodeRecord Add()
        {
            var node = new NodeRecord(
                this.stream.Length, 
                DefaultFirstEdgeOffset, 
                false, 
                DefaultDegree);
            this.Write(node);
            return node;
        }

        public void Remove(long nodeOffset)
        {
            var offset = nodeOffset + sizeof(long);
            _ = this.stream.Seek(offset, SeekOrigin.Begin);
            this.writer.Write(true);
        }

        public NodeRecord Read(long nodeOffset)
        {
            _ = this.stream.Seek(nodeOffset, SeekOrigin.Begin);
            var firstEdgeOffset = this.reader.ReadInt64();
            var isDeleted = this.reader.ReadBoolean();
            var degree = this.reader.ReadInt32();
            return new NodeRecord(nodeOffset, firstEdgeOffset, isDeleted, degree);
        }

        public long ReadFirstEdgeOffset(long nodeOffset)
        {
            _ = this.stream.Seek(nodeOffset, SeekOrigin.Begin);
            return this.reader.ReadInt64(); 
        }

        public void Write(NodeRecord node)
        {
            _ = this.stream.Seek(node.Offset, SeekOrigin.Begin);
            this.writer.Write(node.FirstEdgeOffset);
            this.writer.Write(node.IsDeleted);
            this.writer.Write(node.Degree);
        }

        public void WriteDegree(long nodeOffset, int degree)
        {
            var offset = nodeOffset + sizeof(long) + sizeof(bool);
            _ = this.stream.Seek(offset, SeekOrigin.Begin);
            this.writer.Write(degree);
        }

        public void WriteFirstEdgeOffset(long nodeOffset, long edgeOffset)
        {
            _ = this.stream.Seek(nodeOffset, SeekOrigin.Begin);
            this.writer.Write(edgeOffset);
        }

        public void Dispose()
        {
            this.writer.Dispose();
            this.reader.Dispose();
        }
    }
}
