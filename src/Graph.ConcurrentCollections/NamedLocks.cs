using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Graph.ConcurrentCollections
{
    public sealed class NamedLocks
    {
        private class Gate
        {
            public int RefCount = 0;
        }

        private readonly Dictionary<string, Gate> gates = new();

        private NamedLocks() { }

        public static NamedLocks Empty => new NamedLocks();

        public void Lock([DisallowNull] string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            var gate = this.GetOrAddGate(name);
            Monitor.Enter(gate);
            ++gate.RefCount;
        }

        public int LocksHeld([DisallowNull] string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            return this.TryGetGate(name, out var gate)
                ? gate.RefCount
                : 0;
        }

        public void Unlock([DisallowNull] string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            var gate = this.GetOrAddGate(name);

            --gate.RefCount;
            if (gate.RefCount == 0)
            {
                this.ReleaseGate(name);
            }

            if (gate.RefCount >= 0)
            {
                Monitor.Exit(gate);
            }
        }

        private Gate GetOrAddGate(string name)
        {
            Monitor.Enter(this.gates);
            try
            {
                if (!this.gates.TryGetValue(name, out var gate))
                {
                    gate = new Gate();
                    this.gates.Add(name, gate);
                }

                return gate;
            }
            finally
            {
                Monitor.Exit(this.gates);
            }
        }

        private bool TryGetGate(string name, out Gate gate)
        {
            Monitor.Enter(this.gates);
            try
            {
                return this.gates.TryGetValue(name, out gate);
            }
            finally
            {
                Monitor.Exit(this.gates);
            }
        }

        private void ReleaseGate(string name)
        {
            Monitor.Enter(this.gates);
            try
            {
                this.gates.Remove(name);
            }
            finally
            {
                Monitor.Exit(this.gates);
            }
        }
    }
}
