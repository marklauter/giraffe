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
            add
            {
                ((IClassifiable)this.classifications).Classified += value;
            }

            remove
            {
                ((IClassifiable)this.classifications).Classified -= value;
            }
        }

        /// <inheritdoc/>
        public event EventHandler<DeclassifiedEventArgs> Declassified
        {
            add
            {
                ((IClassifiable)this.classifications).Declassified += value;
            }

            remove
            {
                ((IClassifiable)this.classifications).Declassified -= value;
            }
        }

        /// <inheritdoc/>
        public event EventHandler<QuantificationChangedEventArgs> QuantificationChanged
        {
            add
            {
                ((IQuantifiable)this.quantities).QuantificationChanged += value;
            }

            remove
            {
                ((IQuantifiable)this.quantities).QuantificationChanged -= value;
            }
        }

        /// <inheritdoc/>
        public event EventHandler<QuantificationIgnoredEventArgs> QuantificationIngnored
        {
            add
            {
                ((IQuantifiable)this.quantities).QuantificationIngnored += value;
            }

            remove
            {
                ((IQuantifiable)this.quantities).QuantificationIngnored -= value;
            }
        }

        /// <inheritdoc/>
        public event EventHandler<QualifiedEventArgs> Qualified
        {
            add
            {
                ((IQualifiable)this.qualifications).Qualified += value;
            }

            remove
            {
                ((IQualifiable)this.qualifications).Qualified -= value;
            }
        }

        /// <inheritdoc/>
        public event EventHandler<DisqualifiedEventArgs> Disqualified
        {
            add
            {
                ((IQualifiable)this.qualifications).Disqualified += value;
            }

            remove
            {
                ((IQualifiable)this.qualifications).Disqualified -= value;
            }
        }

        /// <inheritdoc/>
        public IClassifiable Classify(string label)
        {
            return ((IClassifiable)this.classifications).Classify(label);
        }

        /// <inheritdoc/>
        [Pure]
        public abstract object Clone();

        /// <inheritdoc/>
        public IClassifiable Declassify(string label)
        {
            return ((IClassifiable)this.classifications).Declassify(label);
        }

        /// <inheritdoc/>
        [Pure]
        public bool HasQuality(string name)
        {
            return ((IQualifiable)this.qualifications).HasQuality(name);
        }

        /// <inheritdoc/>
        [Pure]
        public bool HasQuantity(string name)
        {
            return ((IQuantifiable)this.quantities).HasQuantity(name);
        }

        /// <inheritdoc/>
        public IQuantifiable Ignore(string name)
        {
            return ((IQuantifiable)this.quantities).Ignore(name);
        }

        /// <inheritdoc/>
        [Pure]
        public bool Is(string label)
        {
            return ((IClassifiable)this.classifications).Is(label);
        }

        /// <inheritdoc/>
        [Pure]
        public bool Is([DisallowNull] IEnumerable<string> labels)
        {
            return ((IClassifiable)this.classifications).Is(labels);
        }

        /// <inheritdoc/>
        [Pure]
        public IQualifiable Qualify(string name, string value)
        {
            return ((IQualifiable)this.qualifications).Qualify(name, value);
        }

        /// <inheritdoc/>
        [Pure]
        public string Quality(string name)
        {
            return ((IQualifiable)this.qualifications).Quality(name);
        }

        /// <inheritdoc/>
        public IQuantifiable Quantify([DisallowNull, Pure] IQuantity quantity)
        {
            return ((IQuantifiable)this.quantities).Quantify(quantity);
        }

        /// <inheritdoc/>
        [Pure]
        public IQuantity Quantity(string name)
        {
            return ((IQuantifiable)this.quantities).Quantity(name);
        }

        /// <inheritdoc/>
        public IQualifiable Disqualify(string name)
        {
            return ((IQualifiable)this.qualifications).Disqualify(name);
        }

        /// <inheritdoc/>
        [Pure]
        public bool TryGetValue<Tq>(string name, out Tq value) where Tq : struct, IComparable, IComparable<Tq>, IEquatable<Tq>, IFormattable
        {
            return ((IQuantifiable)this.quantities).TryGetValue(name, out value);
        }
    }
}
