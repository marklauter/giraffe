using Graph.ConcurrentCollections;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;

namespace Graph.Elements
{
    internal sealed class NodeIndex
    {
        [JsonProperty]
        private readonly ConcurrentDictionary<string, ConcurrentHashSet<Guid>> index = new();

        public void IndexNode(Node target)
        {
            foreach (var label in target.GetLabels())
            {
                var nodes = this.index.GetOrAdd(label, new ConcurrentHashSet<Guid>());
                nodes.Add(target.Id);
            }
        }

        // todo: add lookup methods
    }
}
