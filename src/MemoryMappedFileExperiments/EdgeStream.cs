using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MemoryMappedFileExperiments
{

    public class EdgeStream : IDisposable
    {
        private readonly BinaryWriter writer;
        private readonly BinaryReader reader;
        private readonly Stream stream;
        private readonly NodeStream nodes;
        private readonly long recordSize;
        private EdgeRecord current;
        private long offset;

        private const long DefaultNextRecordOffset = -1;

        public EdgeStream(Stream edges, NodeStream nodes)
        {
            this.stream = edges;
            this.nodes = nodes;
            this.writer = new BinaryWriter(edges, Encoding.UTF8, true);
            this.reader = new BinaryReader(edges, Encoding.UTF8, true);
            this.recordSize = Marshal.SizeOf(typeof(EdgeRecord));
        }

        public void Reset()
        {
            this.offset = 0L;
        }

        public void MoveNext()
        {
            this.offset += this.recordSize;
            this.current = this.Read(this.offset);
        }

        public ref readonly EdgeRecord Current()
        {
            return ref this.current;
        }

        public void Connect(long node1, long node2)
        {
            this.AppendEdge(node1, node2);
            this.AppendEdge(node2, node1);
        }

        public void Disconnect(long node1, long node2)
        {
            this.DeleteEdge(node1, node2);
            this.DeleteEdge(node2, node1);
        }

        public EdgeRecord Read(long offset)
        {
            _ = this.stream.Seek(offset, SeekOrigin.Begin);
            var nextRecord = this.reader.ReadInt64();
            var isDeleted = this.reader.ReadBoolean();
            var source = this.reader.ReadInt64();
            var target = this.reader.ReadInt64();
            return new EdgeRecord(offset, nextRecord, isDeleted, source, target);
        }

        public void Write(EdgeRecord edge)
        {
            _ = this.stream.Seek(edge.Offset, SeekOrigin.Begin);
            this.writer.Write(edge.NextRecordOffset);
            this.writer.Write(edge.IsDeleted);
            this.writer.Write(edge.Source);
            this.writer.Write(edge.Target);
        }

        private void AppendEdge(long source, long target)
        {
            var newEdgeOffset = this.stream.Length;
            var newEdge = new EdgeRecord(newEdgeOffset, DefaultNextRecordOffset, false, source, target);
            this.Write(newEdge);

            var node = this.nodes.Read(source);
            this.nodes.WriteDegree(source, node.Degree + 1);
            if (NodeStream.DefaultFirstEdgeOffset == node.FirstEdgeOffset)
            {
                this.nodes.WriteFirstEdgeOffset(source, newEdgeOffset);
            }
            else
            {
                // traverse to the final edge in the linked list
                var lastEdgeOffset = node.FirstEdgeOffset;
                var nextEdgeOffset = this.ReadNextRecordOffset(lastEdgeOffset);
                while (DefaultNextRecordOffset != nextEdgeOffset)
                {
                    lastEdgeOffset = nextEdgeOffset;
                    nextEdgeOffset = this.ReadNextRecordOffset(nextEdgeOffset);
                }

                this.WriteNextRecordOffset(lastEdgeOffset, newEdgeOffset);
                this.nodes.WriteDegree(source, node.Degree + 1);
            }
        }

        private void DeleteEdge(long source, long target)
        {
            var nextEdgeOffset = this.nodes.ReadFirstEdgeOffset(source);
            while (DefaultNextRecordOffset != nextEdgeOffset
                && this.ReadTarget(nextEdgeOffset) != target)
            {
                nextEdgeOffset = this.ReadNextRecordOffset(nextEdgeOffset);
            }

            if (DefaultNextRecordOffset != nextEdgeOffset)
            {
                this.Remove(nextEdgeOffset);
            }
        }

        public long ReadNextRecordOffset(long edgeOffset)
        {
            _ = this.stream.Seek(edgeOffset, SeekOrigin.Begin);
            return this.reader.ReadInt64();
        }

        public long ReadTarget(long edgeOffset)
        {
            var offset = edgeOffset + 2 * sizeof(long) + sizeof(bool);
            _ = this.stream.Seek(offset, SeekOrigin.Begin);
            return this.reader.ReadInt64();
        }

        public void Remove(long edgeOffset)
        {
            var offset = edgeOffset + sizeof(long);
            _ = this.stream.Seek(offset, SeekOrigin.Begin);
            this.writer.Write(true);
        }

        public void WriteNextRecordOffset(long edgeOffset, long nextRecordOffset)
        {
            _ = this.stream.Seek(edgeOffset, SeekOrigin.Begin);
            this.writer.Write(nextRecordOffset);
        }

        public void Dispose()
        {
            this.writer.Dispose();
            this.reader.Dispose();
        }
    }
}

