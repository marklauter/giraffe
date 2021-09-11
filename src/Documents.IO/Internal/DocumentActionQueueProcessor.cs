using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Threading;

namespace Documents.IO
{
    internal sealed class DocumentActionQueueProcessor<T>
        : IDisposable
        where T : class
    {
        private ImmutableQueue<DocumentActionItem<T>> documentActionQueue = ImmutableQueue<DocumentActionItem<T>>.Empty;
        private readonly CancellationTokenSource cancellationTokenSource = new();
        private readonly Action<string> deleteDocumentAction;
        private readonly Action<Document<T>> writeDocumentAction;
        private bool disposedValue;

        public delegate void WriteDocument(Document<T> document);

        public DocumentActionQueueProcessor(
            Action<string> deleteDocumentAction,
            Action<Document<T>> writeDocumentAction)
        {
            this.StartActionQueueProcessor();
            this.deleteDocumentAction = deleteDocumentAction ?? throw new ArgumentNullException(nameof(deleteDocumentAction));
            this.writeDocumentAction = writeDocumentAction ?? throw new ArgumentNullException(nameof(writeDocumentAction));
        }

        public void EnqueueAddAction(Document<T> document)
        {
            this.documentActionQueue = this.documentActionQueue
                .Enqueue(new DocumentActionItem<T>(DocumentAction.Add, document));
        }

        public void EnqueueRemoveAction(string key)
        {
            this.documentActionQueue = this.documentActionQueue
                .Enqueue(new DocumentActionItem<T>(DocumentAction.Remove, key));
        }

        public void EnqueueUpdateAction(Document<T> document)
        {
            this.documentActionQueue = this.documentActionQueue
                .Enqueue(new DocumentActionItem<T>(DocumentAction.Update, document));
        }

        public void Flush()
        {
            while (!this.documentActionQueue.IsEmpty)
            {
                this.documentActionQueue = this.documentActionQueue
                    .Dequeue(out var actionItem);

                this.ProcessActionItem(actionItem);
            }
        }

        private void ProcessActionItem(DocumentActionItem<T> actionItem)
        {
            switch (actionItem.Action)
            {
                case DocumentAction.Add:
                case DocumentAction.Update:
                    this.writeDocumentAction.Invoke(actionItem.Item as Document<T>);
                    break;
                case DocumentAction.Remove:
                    this.deleteDocumentAction.Invoke(actionItem.Item as string);
                    break;
            }
        }

        private void ProcessActionQueue(CancellationToken cancellationToken)
        {
            var wait = new SpinWait();
            while (!cancellationToken.IsCancellationRequested)
            {
                if (this.documentActionQueue.IsEmpty)
                {
                    wait.SpinOnce();
                    continue;
                }

                this.documentActionQueue = this.documentActionQueue
                    .Dequeue(out var actionItem);

                this.ProcessActionItem(actionItem);
            }

            this.Flush();
        }

        private void StartActionQueueProcessor()
        {
            // todo: make this actually run in a thread: https://www.minatcoding.com/blog/tech-tips/tech-tip-creating-a-long-running-background-task-in-net-core/
            this.ProcessActionQueue(this.cancellationTokenSource.Token);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.cancellationTokenSource.Cancel();
                }

                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
