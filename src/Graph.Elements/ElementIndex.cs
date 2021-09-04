using Graph.ConcurrentCollections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graph.Elements
{
    // todo: add lookup methods
    public sealed class ElementIndex
    {
        [JsonProperty]
        private readonly Dictionary<string, HashSet<Guid>> index = new();

        private readonly NamedLocks gates = NamedLocks.Empty;

        public void Add([DisallowNull] Element element)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.ClassificationChanged += this.Element_ClassificationChanged;

            foreach (var label in element.Labels)
            {
                this.AddElement(label, element.Id);
            }
        }

        private void Element_ClassificationChanged(object sender, ClassificationChangedEventArgs e)
        {
            if (!(sender is Element element))
            {
                throw new ArgumentException($"Type of {nameof(sender)} must be {nameof(Element)}");
            }

            switch (e.Type)
            {
                case ClassificationChangeType.Classifiy:
                    this.AddElement(e.Label, element.Id);
                    break;
                case ClassificationChangeType.Declassify:
                    this.RemoveElement(e.Label, element.Id);
                    break;
            }
        }

        private HashSet<Guid> GetOrAddElements(string label)
        {
            lock (this.gates)
            {
                if (!this.index.TryGetValue(label, out var elements))
                {
                    elements = new HashSet<Guid>();
                    this.index.Add(label, elements);
                }

                return elements;
            }
        }

        private bool TryGetElements(string label, out HashSet<Guid> elements)
        {
            lock (this.gates)
            {
                return this.index.TryGetValue(label, out elements);
            }
        }

        private void RemoveElement(string label, Guid id)
        {
            if (this.TryGetElements(label, out var elements))
            {
                this.gates.Lock(label);
                try
                {
                    elements.Remove(id);
                }
                finally
                {
                    this.gates.Unlock(label);
                }
            }
        }

        private void AddElement(string label, Guid id)
        {
            var elements = this.GetOrAddElements(label);

            this.gates.Lock(label);
            try
            {
                elements.Add(id);
            }
            finally
            {
                this.gates.Unlock(label);
            }
        }
    }
}
