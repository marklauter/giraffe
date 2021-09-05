using Graph.ConcurrentCollections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Graph.Elements
{
    // todo: add lookup methods
    public sealed class ElementIndex
    {
        private const string DefaultKey = "_ALL_";

        [JsonProperty]
        private readonly Dictionary<string, HashSet<Guid>> index = new();

        private readonly NamedLocks gates = NamedLocks.Empty;

        public event EventHandler<IndexUpdatedEventArgs> Updated;

        private ElementIndex() { }

        public int Count()
        {
            if (this.TryGetElements(DefaultKey, out var elements))
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


        private void RemoveElement(string label, Guid id)
        {
            if (this.TryGetElements(label, out var elements))
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

        private void OnUpdated()
        {
            Updated?.Invoke(this, new IndexUpdatedEventArgs());
        }
    }
}
