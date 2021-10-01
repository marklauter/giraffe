using System;

namespace Graphs.Events
{
    public abstract class GraphEventArgs
        : EventArgs
    {
        protected GraphEventArgs()
        {
            this.Timestamp = DateTime.UtcNow;
            this.Id = Guid.NewGuid();
        }

        public DateTime Timestamp { get; }

        public Guid Id { get; }
    }
}
