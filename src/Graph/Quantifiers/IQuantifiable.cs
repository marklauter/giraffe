using System;

namespace Graph.Quantifiers
{
    public interface IQuantifiable
        : ICloneable
    {
        event EventHandler<QuantifiedEventArgs> Quantified;
        event EventHandler<QuantityRemovedEventArgs> QuantityRemoved;

        /// <summary>
        /// Returns true if the instance contains the named quantity.
        /// </summary>
        /// <param name="name">Name of the quantity.</param>
        /// <returns><see cref="Boolean"/></returns>
        bool HasQuantity(string name);

        /// <summary>
        /// Removes a quantity attribute.
        /// </summary>
        /// <param name="name">Name of the quantity to remove.</param>
        /// <returns><see cref="IQuantifiable"/></returns>
        /// <remarks>
        /// <seealso cref="Quantify"/>
        /// </remarks>
        IQuantifiable RemoveQuantity(string name);

        /// <summary>
        /// Adds a quantity attribute.
        /// </summary>
        /// <param name="name">Name of the quantity.</param>
        /// <param name="quantity"><see cref="Quantity"/></param>
        /// <returns><see cref="IQuantifiable"/></returns>
        /// <remarks>
        /// <seealso cref="RemoveQuantity"/>
        /// </remarks>
        IQuantifiable Quantify(Quantity quantity);

        /// <summary>
        /// Returns the named quanity.
        /// </summary>
        /// <param name="name">Name of the quantity to return.</param>
        /// <returns><see cref="Quantity"/></returns>
        /// <remarks>
        /// <seealso cref="Value{T}"/>
        /// </remarks>
        Quantity Quantity(string name);

        // todo: maybe change Quantity Quantity(string name) to TryGetQuantity(name, out qty)
    }
}
