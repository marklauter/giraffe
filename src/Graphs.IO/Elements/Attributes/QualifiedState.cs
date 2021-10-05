using Graphs.Attributes;
using System;
using System.Threading.Tasks;

namespace Graphs.IO
{
    public sealed class DocumentQualifiedSink<TId>
        : IQualifiedSink<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public Task RemoveAsync(TId elementId)
        {
            throw new NotImplementedException();
        }

        public Task WriteAsync(IQualified<TId> component)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class DocumentQualifiableSource<TId>
        : IQualifiableSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public Task<IQualifiable<TId>> ReadAsync(TId id)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class QualifiedState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
