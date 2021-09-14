using System;
using System.Threading;


namespace Collections.Concurrent
{
    public sealed class Gate
    {
        private readonly ReaderWriterLockSlim gate = new();

        public void Read(Action action)
        {
            this.gate.EnterReadLock();
            try
            {
                action();
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public T Read<T>(Func<T> func)
        {
            this.gate.EnterReadLock();
            try
            {
                return func();
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public TResult Read<TArg, TResult>(TArg arg, Func<TArg, TResult> func)
        {
            this.gate.EnterReadLock();
            try
            {
                return func(arg);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public TResult Read<TArg1, TArg2, TResult>(TArg1 arg1, TArg2 arg2, Func<TArg1, TArg2, TResult> func)
        {
            this.gate.EnterReadLock();
            try
            {
                return func(arg1, arg2);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public void Write(Action action)
        {
            this.gate.EnterWriteLock();
            try
            {
                action();
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        public void Write<TArg>(TArg arg, Action<TArg> action)
        {
            this.gate.EnterWriteLock();
            try
            {
                action(arg);
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        public void Write<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2> action)
        {
            this.gate.EnterWriteLock();
            try
            {
                action(arg1, arg2);
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        public TResult Write<TArg, TResult>(TArg arg, Func<TArg, TResult> func)
        {
            this.gate.EnterWriteLock();
            try
            {
                return func(arg);
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }
    }
}
