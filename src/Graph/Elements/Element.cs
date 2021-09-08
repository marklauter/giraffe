using Graph.Classifiers;
using Graph.Qualifiers;
using Graph.Quantifiers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graph.Elements
{
    [DebuggerDisplay("{Id}")]
    public abstract class Element<TId>
        : IElement<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        [JsonProperty]
        private readonly ClassificationCollection classifications = ClassificationCollection.Empty;

        [JsonProperty]
        private readonly QualificationCollection qualifications = QualificationCollection.Empty;

        [JsonProperty]
        private readonly QuantityCollection quantities = QuantityCollection.Empty;

        protected Element() { }

        protected Element(TId id)
        {
            this.Id = id;
        }

        protected Element([DisallowNull, Pure] Element<TId> other)
        {
            this.Id = other.Id;
        }

        /// <inheritdoc/>
        [Key]
        [Required]
        [JsonProperty("id")]
        public TId Id { get; }

        /// <inheritdoc/>
        public event EventHandler<ClassifiedEventArgs> Classified
        {
            add { this.classifications.Classified += value; }
            remove { this.classifications.Classified -= value; }
        }

        /// <inheritdoc/>
        public event EventHandler<DeclassifiedEventArgs> Declassified
        {
            add { this.classifications.Declassified += value; }
            remove { this.classifications.Declassified -= value; }
        }

        /// <inheritdoc/>
        public event EventHandler<QualifiedEventArgs> Qualified
        {
            add { this.qualifications.Qualified += value; }
            remove { this.qualifications.Qualified -= value; }
        }

        /// <inheritdoc/>
        public event EventHandler<DisqualifiedEventArgs> Disqualified
        {
            add { this.qualifications.Disqualified += value; }
            remove { this.qualifications.Disqualified -= value; }
        }

        /// <inheritdoc/>
        public event EventHandler<QuantifiedEventArgs> Quantified
        {
            add { this.quantities.Quantified += value; }
            remove { this.quantities.Quantified -= value; }
        }

        /// <inheritdoc/>
        public event EventHandler<QuantityRemovedEventArgs> QuantityRemoved
        {
            add { this.quantities.QuantityRemoved += value; }
            remove { this.quantities.QuantityRemoved -= value; }
        }

        /// <inheritdoc/>
        public IElement<TId> Classify(string label)
        {
            _ = this.classifications.Classify(label);
            return this;
        }

        /// <inheritdoc/>
        [Pure]
        public abstract object Clone();

        /// <inheritdoc/>
        public IElement<TId> Declassify(string label)
        {
            _ = this.classifications.Declassify(label);
            return this;
        }

        /// <inheritdoc/>
        public IElement<TId> Disqualify(string name)
        {
            _ = this.qualifications.Disqualify(name);
            return this;
        }

        /// <inheritdoc/>
        public bool HasAttribute(string name)
        {
            return this.qualifications.HasQuality(name)
                || this.quantities.HasQuantity(name);
        }

        /// <inheritdoc/>
        public bool Is(string label)
        {
            return this.classifications.Is(label);
        }

        /// <inheritdoc/>
        public bool Is(IEnumerable<string> labels)
        {
            return this.classifications.Is(labels);
        }

        /// <inheritdoc/>
        public IElement<TId> Qualify(string name, string value)
        {
            this.qualifications.Qualify(name, value);
            return this;
        }

        /// <inheritdoc/>
        public string Quality(string name)
        {
            return this.qualifications.Quality(name);
        }

        /// <inheritdoc/>
        public IElement<TId> Quantify(Quantity quantity)
        {
            this.quantities.Quantify(quantity);
            return this;
        }

        /// <inheritdoc/>
        public Quantity Quantity(string name)
        {
            return this.quantities.Quantity(name);
        }

        /// <inheritdoc/>
        public IElement<TId> RemoveQuantity(string name)
        {
            this.quantities.RemoveQuantity(name);
            return this;
        }
    }
}
