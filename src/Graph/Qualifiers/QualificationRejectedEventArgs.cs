using System;

namespace Graph.Qualifiers
{
    public sealed class QualificationRejectedEventArgs
        : EventArgs
    {
        public QualificationRejectedEventArgs(string name)
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
