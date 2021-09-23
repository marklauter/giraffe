using System;

namespace Graphs.Elements.Qualifiers
{
    public sealed class DisqualifiedEventArgs
        : EventArgs
    {
        public DisqualifiedEventArgs(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.Name = name;
        }

        public string Name { get; }
    }
}
