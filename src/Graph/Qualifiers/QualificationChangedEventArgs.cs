using System;

namespace Graph.Qualifiers
{
    public sealed class QualificationChangedEventArgs
        : EventArgs
    {
        public QualificationChangedEventArgs(string name, string value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
            }

            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }
        public string Value { get; }
    }
}
