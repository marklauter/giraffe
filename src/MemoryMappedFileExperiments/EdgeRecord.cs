using System.Diagnostics;

namespace MemoryMappedFileExperiments
{
    [DebuggerDisplay("{Offset}, ({Source}, {Target}), Deleted {IsDeleted}, => {NextRecordOffset}")]
    public struct EdgeRecord
    {
        public EdgeRecord(
            long offset,
            long nextRecordOffset, 
            bool isDeleted, 
            long source, 
            long target)
        {
            this.Offset = offset;
            this.NextRecordOffset = nextRecordOffset;
            this.IsDeleted = isDeleted;
            this.Source = source;
            this.Target = target;
        }

        public long Offset { get; }

        // offset 0
        public long NextRecordOffset { get; }

        // offset sizeof(long)
        public bool IsDeleted{ get; }

        // offset sizeof(long) + sizeof(bool)
        public long Source { get; }

        // offset 2 * sizeof(long) + sizeof(bool) 
        public long Target { get; }
    }
}

