using System;
using System.IO;
using System.Text;

namespace MemoryMappedFileExperiments
{
    public struct EdgeStream : IDisposable
    {
        private readonly BinaryWriter edgeWriter;
        private readonly BinaryReader edgeReader;
        private readonly Stream edges;
        private readonly NodeStream nodes;

        public EdgeStream(Stream edges, NodeStream nodes)
        {
            this.edges = edges;
            this.nodes = nodes;
            this.edgeWriter = new BinaryWriter(edges, Encoding.UTF8, true);
            this.edgeReader = new BinaryReader(edges, Encoding.UTF8, true);
        }

        public void Connect(long node1, long node2)
        {
            this.AppendEdge(node1, node2);
            this.AppendEdge(node2, node1);
        }

        public EdgeRecord Read(long offset)
        {
            _ = this.edges.Seek(offset, SeekOrigin.Begin);
            // next record is first field in record so we can traverse the list efficiently
            var nextRecord = this.edgeReader.ReadInt64();
            var isActive = this.edgeReader.ReadBoolean();
            var source = this.edgeReader.ReadInt64();
            var target = this.edgeReader.ReadInt64();

            return new EdgeRecord(nextRecord, isActive, source, target);
        }

        public void Write(long offset, EdgeRecord edge)
        {
            _ = this.edges.Seek(offset, SeekOrigin.Begin);
            this.edgeWriter.Write(edge.NextRecord);
            this.edgeWriter.Write(edge.IsActive);
            this.edgeWriter.Write(edge.Source);
            this.edgeWriter.Write(edge.Target);
        }

        private void AppendEdge(long source, long target)
        {
            var newEdgeOffset = this.edges.Length;
            this.Write(newEdgeOffset, new EdgeRecord(-1, true, source, target));

            var node = this.nodes.Read(source);
            if (-1 == node.FirstEdgeOffset)
            {
                this.nodes.Write(source, new NodeRecord(node.Degree + 1, newEdgeOffset));
            }
            else
            {
                var lastEdgeOffset = node.FirstEdgeOffset;
                var nextEdgeOffset = this.ReadNextEdgeOffset(node.FirstEdgeOffset);
                while (nextEdgeOffset != -1)
                {
                    lastEdgeOffset = nextEdgeOffset;
                    nextEdgeOffset = this.ReadNextEdgeOffset(nextEdgeOffset);
                }

                var previousEdge = this.Read(lastEdgeOffset);
                if (previousEdge.NextRecord != -1)
                {
                    throw new InvalidOperationException("next must be null");
                }

                // todo: could be refined to only write the exact data field that is changing
                this.Write(lastEdgeOffset, new EdgeRecord(newEdgeOffset, previousEdge.IsActive, previousEdge.Source, previousEdge.Target));

                node = new NodeRecord(node.Degree + 1, node.FirstEdgeOffset);
                this.nodes.Write(source, node);
            }
        }

        private long ReadNextEdgeOffset(long edge)
        {
            _ = this.edges.Seek(edge, SeekOrigin.Begin);
            return this.edgeReader.ReadInt64();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

