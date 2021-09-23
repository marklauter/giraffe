//using Graphs.Elements;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Graphs.Data
//{
//    public interface IGraphContext<TElement, TId>
//        where TElement : IElement<TId>
//        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
//    {
//        Task AddAsync(TElement element);
//        Task AddAsync(IEnumerable<TElement> elements);

//        Task<bool> ContainsAsync(TElement element);

//        Task<TElement> ReadAsync(TId id);
//        Task<IEnumerable<TElement>> ReadAsync(IEnumerable<string> ids);

//        Task RemoveAsync(TElement element);
//        Task RemoveAsync(IEnumerable<TElement> elements);

//        Task UpdateAsync(TElement element);
//        Task UpdateAsync(IEnumerable<TElement> elements);
//    }

//    public sealed class GraphDataContext<TId>
//        //: IDocumentContext<TId>
//        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
//    {
//        //public IDocumentCollection<Edge<TId>> Edges { get; }
//        //public IIdGenerator<TId> IdGenerator { get; }
//        //public IDocumentCollection<Node<TId>> Nodes { get; }

//        //public GraphDataContext(
//        //    IIdGenerator<TId> idGenerator,
//        //    IDocumentCollection<Node<TId>> nodes,
//        //    IDocumentCollection<Edge<TId>> edges)
//        //{
//        //    this.Edges = edges ?? throw new ArgumentNullException(nameof(edges));
//        //    this.IdGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
//        //    this.Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
//        //}
//    }
//}
