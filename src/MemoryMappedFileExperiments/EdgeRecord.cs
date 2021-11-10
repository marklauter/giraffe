using System.Diagnostics;

namespace MemoryMappedFileExperiments
{
    [DebuggerDisplay("{Offset}, ({Source}, {Target}), Deleted {IsDeleted}, => {NextRecordOffset}")]
    public readonly struct EdgeRecord
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

        public readonly long Offset;

        // offset 0
        public readonly long NextRecordOffset;

        // offset sizeof(long)
        public readonly bool IsDeleted;

        // offset sizeof(long) + sizeof(bool)
        public readonly long Source;

        // offset 2 * sizeof(long) + sizeof(bool) 
        public readonly long Target;
    }
}

