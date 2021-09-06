using System;

namespace Graph.Elements
{
    public interface IQuantifiable
    {
        event EventHandler<QuantificationChangedEventArgs> QuantificationChanged;
        event EventHandler<QuantificationIgnoredEventArgs> QuantificationIngnored;

        /// <summary>
        /// Removes a quantity attribute.
        /// </summary>
        /// <param name="name">Name of the quantity to remove.</param>
        /// <returns><see cref="IQuantifiable"/></returns>
        /// <remarks>
        /// <seealso cref="Quantify"/>
        /// </remarks>
        IQuantifiable Ignore(string name);

        /// <summary>
        /// Returns true if the instance contains the named quantity.
        /// </summary>
        /// <param name="name">Name of the quantity.</param>
        /// <returns><see cref="Boolean"/></returns>
        bool HasQuantity(string name);

        /// <summary>
        /// Adds a quantity attribute.
        /// </summary>
        /// <param name="name">Name of the quantity.</param>
        /// <param name="quantity"><see cref="IQuantity"/></param>
        /// <returns><see cref="IQuantifiable"/></returns>
        /// <remarks>
        /// <seealso cref="Ignore"/>
        /// </remarks>
        IQuantifiable Quantify(IQuantity quantity);

        /// <summary>
        /// Returns the named quanity.
        /// </summary>
        /// <param name="name">Name of the quantity to return.</param>
        /// <returns><see cref="IQuantity"/></returns>
        /// <remarks>
        /// <seealso cref="Value{T}"/>
        /// </remarks>
        IQuantity Quantity(string name);

        /// <summary>
        /// Retruns the typed value of the named quantity.
        /// </summary>
        /// <typeparam name="T">Type to which the internal value of IQuantity will be converted.</typeparam>
        /// <param name="name">Name of the qantity to return.</param>
        /// <returns>T</returns>
        /// <remarks>
        /// <seealso cref="Quantity(String)"/>
        /// </remarks>
        T Value<T>(string name);
    }
}
