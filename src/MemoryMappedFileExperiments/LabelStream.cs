using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MemoryMappedFileExperiments
{
    public readonly struct LabelRecord
    {
        public LabelRecord(
            long offset,
            long nextRecordOffset,
            bool isDeleted,
            Guid id,
            string value)
        {
            this.Offset = offset;
            this.NextRecordOffset = nextRecordOffset;
            this.IsDeleted = isDeleted;
            this.Id = id;
            this.Value = value;
        }

        public readonly long Offset;
        public readonly long NextRecordOffset;
        public readonly bool IsDeleted;
        public readonly Guid Id;
        public readonly string Value;
    }

    public class LabelStream : IDisposable
    {
        private readonly Stream stream;
        private readonly BinaryWriter writer;
        private readonly BinaryReader reader;
        private readonly long recordSize;
        private LabelRecord current;
        private long offset;

        public LabelStream(Stream labels)
        {
            this.stream = labels;
            this.writer = new BinaryWriter(labels, Encoding.UTF8, true);
            this.reader = new BinaryReader(labels, Encoding.UTF8, true);
            this.recordSize = Marshal.SizeOf(typeof(LabelRecord));
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

        public ref readonly LabelRecord Current()
        {
            return ref this.current;
        }

        public LabelRecord Read(long offset)
        {
            _ = this.stream.Seek(offset, SeekOrigin.Begin);
            var nextRecord = this.reader.ReadInt64();
            var isDeleted = this.reader.ReadBoolean();
            var id = new Guid(this.reader.ReadBytes(16));
            var value = this.reader.ReadString();
            return new LabelRecord(offset, nextRecord, isDeleted, id, value);
        }

        public void Update(LabelRecord label)
        {
            // 0. if no changes, then exit
            // 1. delete current record
            // 2. append updated item
            // 3. update index
            var current = this.Read(label.Offset);
            if(current.Id == label.Id && String.Compare(current.Value, label.Value) == 0)
            {
                return;
            }

            this.Delete(label);
            this.Append(label);
        }

        public void Append(LabelRecord label)
        {
            _ = this.stream.Seek(label.Offset, SeekOrigin.Begin);
            this.writer.Write(label.NextRecordOffset);
            this.writer.Write(label.IsDeleted);
            this.writer.Write(label.Id.ToByteArray());
            this.writer.Write(label.Value);
        }

        public void Delete(LabelRecord label)
        {
            var offset = label.Offset + sizeof(long);
            _ = this.stream.Seek(offset, SeekOrigin.Begin);
            this.writer.Write(true);
        }


        public void Dispose()
        {
            this.writer.Dispose();
            this.reader.Dispose();
        }
    }
}

