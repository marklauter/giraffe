using System;

namespace Graph.Qualifiers
{
    public sealed class DisqualifiedEventArgs
        : EventArgs
    {
        public DisqualifiedEventArgs(string name, SerializableValue value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            this.Name = name;
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Name { get; }

        public SerializableValue Value { get; }
    }
}
