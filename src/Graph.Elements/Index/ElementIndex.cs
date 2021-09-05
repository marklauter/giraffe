using Graph.ConcurrentCollections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Graph.Elements
{
    public sealed class ElementIndex
        : ICloneable
    {
        private const string DefaultKey = "_ALL_";

        [JsonProperty("elementIndex")]
        private readonly Dictionary<string, HashSet<Guid>> index = new();

        private readonly NamedLocks gates = NamedLocks.Empty;

        public event EventHandler<IndexUpdatedEventArgs> Updated;

        private ElementIndex() { }

        private ElementIndex(ElementIndex other)
        {
            lock (other.gates)
            {
                this.index = new Dictionary<string, HashSet<Guid>>(other.index);
            }
        }

        public int Count()
        {
            if (this.TryGetElements(DefaultKey, out HashSet<Guid> elements))
            {
                this.gates.Lock(DefaultKey);
                try
                {
                    return elements.Count;
                }
                finally
                {
                    this.gates.Unlock(DefaultKey);
                }
            }

            return 0;
        }

        public static ElementIndex Empty => new ElementIndex();

        public void Add([DisallowNull] IElement element)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.ClassificationChanged += this.Element_ClassificationChanged;

            this.AddElement(DefaultKey, element.Id);
            foreach (var label in element.Labels)
            {
                this.AddElement(label, element.Id);
            }
        }

        public object Clone()
        {
            return new ElementIndex(this);
        }

        public bool TryGetElements(string label, out ImmutableHashSet<Guid> elements)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
            }

            elements = null;

            if (this.TryGetElements(label, out HashSet<Guid> e))
            {
                this.gates.Lock(label);
                try
                {
                    elements = e.ToImmutableHashSet();
                    return true;
                }
                finally
                {
                    this.gates.Unlock(label);
                }
            }

            return false;
        }

        private void Element_ClassificationChanged(object sender, ClassificationChangedEventArgs e)
        {
            if (sender is not Element element)
            {
                throw new ArgumentException($"Type of {nameof(sender)} must be {nameof(Element)}");
            }

            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
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

        private void AddElement(string label, Guid id)
        {
            var elements = this.GetOrAddElements(label);

            this.gates.Lock(label);
            try
            {
                elements.Add(id);
                this.OnUpdated();
            }
            finally
            {
                this.gates.Unlock(label);
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

        private void RemoveElement(string label, Guid id)
        {
            if (this.TryGetElements(label, out HashSet<Guid> elements))
            {
                this.gates.Lock(label);
                try
                {
                    elements.Remove(id);
                    this.OnUpdated();
                }
                finally
                {
                    this.gates.Unlock(label);
                }
            }
        }

        private bool TryGetElements(string label, out HashSet<Guid> elements)
        {
            lock (this.gates)
            {
                return this.index.TryGetValue(label, out elements);
            }
        }

        private void OnUpdated()
        {
            Updated?.Invoke(this, new IndexUpdatedEventArgs());
        }
    }
}
