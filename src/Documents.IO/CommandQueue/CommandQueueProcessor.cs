using System;
using System.Collections.Immutable;
using System.Threading;

namespace Documents.IO.CommandQueue
{
    public interface ICommandQueue<TCommand, TMember>
        where TCommand : DocumentCommand<TMember>
        where TMember : class
    {
        void Enqueue(TCommand command);
    }

    public interface IQueueProcessor<TCommand, TMember>
        : IDisposable
        where TCommand : DocumentCommand<TMember>
        where TMember : class
    {
        void Flush();
        void ProcessCommandQueue(ImmutableQueue<TCommand> queue);
    }

    public sealed class QueueProcessor<TCommand, TMember>
        : IQueueProcessor<TCommand, TMember>
        where TCommand : DocumentCommand<TMember>
        where TMember : class
    {
        private readonly CancellationTokenSource cancellationTokenSource = new();
        private readonly ICommandProcessor<TCommand, TMember> commandProcessor;
        private readonly CancellationToken cancelationToken;
        private bool disposedValue;

        public QueueProcessor(ICommandProcessor<TCommand, TMember> commandProcessor)
        {
            this.commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            this.cancelationToken = this.cancellationTokenSource.Token;
        }

        public async void ProcessCommandQueue(ImmutableQueue<TCommand> queue)
        {
            var wait = new SpinWait();
            while (!this.cancelationToken.IsCancellationRequested)
            {
                if (queue.IsEmpty)
                {
                    wait.SpinOnce();
                    continue;
                }

                queue = queue.Dequeue(out var command);
                await this.commandProcessor.ExecuteAsyc(command);
            }
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

        public void Flush()
        {
            throw new NotImplementedException();
            // while not empty, process commands
        }
    }

    public sealed class CommandQueue<TCommand, TMember>
        : ICommandQueue<TCommand, TMember>
        , IDisposable
        where TCommand : DocumentCommand<TMember>
        where TMember : class
    {
        private readonly IQueueProcessor<TCommand, TMember> processor;
        private ImmutableQueue<TCommand> queue = ImmutableQueue<TCommand>.Empty;
        private bool disposedValue;

        public CommandQueue(IQueueProcessor<TCommand, TMember> queueProcessor)
        {
            this.processor = queueProcessor ?? throw new ArgumentNullException(nameof(queueProcessor));
            this.processor.ProcessCommandQueue(this.queue);
        }

        public void Enqueue(TCommand command)
        {
            this.queue = this.queue.Enqueue(command);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.processor.Dispose();
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

    internal sealed class CommandQueueProcessorx<T>
        : IDisposable
        where T : class
    {
        private ImmutableQueue<DocumentCommand<T>> documentActionQueue = ImmutableQueue<DocumentCommand<T>>.Empty;
        private readonly CancellationTokenSource cancellationTokenSource = new();
        private bool disposedValue;

        public delegate void WriteDocument(Document<T> document);

        public CommandQueueProcessorx()
        {
            this.StartActionQueueProcessor();
        }

        public void EnqueueAddAction(Document<T> document)
        {
            this.documentActionQueue = this.documentActionQueue
                .Enqueue(new DocumentCommand<T>(CommandType.Add, document));
        }

        public void EnqueueRemoveAction(string key)
        {
            this.documentActionQueue = this.documentActionQueue
                .Enqueue(new DocumentCommand<T>(CommandType.Remove, key));
        }

        public void EnqueueUpdateAction(Document<T> document)
        {
            this.documentActionQueue = this.documentActionQueue
                .Enqueue(new DocumentCommand<T>(CommandType.Update, document));
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

        private void ProcessActionItem(DocumentCommand<T> actionItem)
        {
            switch (actionItem.Command)
            {
                case CommandType.Add:
                case CommandType.Update:
                    this.writeDocumentAction.Invoke(actionItem.Item as Document<T>);
                    break;
                case CommandType.Remove:
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
