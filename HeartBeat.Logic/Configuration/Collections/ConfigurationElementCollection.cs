using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace HeartBeat.Logic.Configuration.Collections
{
    /// <summary>
    /// Generic Implementation of the Configuration Element Collection
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    public abstract class BaseConfigurationElementCollection<TElement> 
        : ConfigurationElementCollection, IList<TElement> 
        where TElement : ConfigurationElement, new()
    {
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new TElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"/> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
// ReSharper disable RedundantCast
            return ((TElement)element).ToString();
// ReSharper restore RedundantCast
        }

        #region Implementation of IEnumerable<TElement>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
        {
// ReSharper disable LoopCanBeConvertedToQuery
            foreach (TElement type in (IEnumerable)this)
// ReSharper restore LoopCanBeConvertedToQuery
            {
                yield return type;
            }
        }

        #endregion

        #region Implementation of ICollection<TElement>

        /// <summary>
        /// Adds the specified configuration element.
        /// </summary>
        /// <param name="configurationElement">The configuration element.</param>
        public void Add(TElement configurationElement)
        {
            BaseAdd(configurationElement, true);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        ///   </exception>
        public void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// Determines whether [contains] [the specified configuration element].
        /// </summary>
        /// <param name="configurationElement">The configuration element.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified configuration element]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(TElement configurationElement)
        {
            return !(IndexOf(configurationElement) < 0);
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(TElement[] array, int arrayIndex)
        {
// ReSharper disable CoVariantArrayConversion
            base.CopyTo(array, arrayIndex);
// ReSharper restore CoVariantArrayConversion
        }

        /// <summary>
        /// Removes the specified configuration element.
        /// </summary>
        /// <param name="configurationElement">The configuration element.</param>
        /// <returns></returns>
        public bool Remove(TElement configurationElement)
        {
            BaseRemove(GetElementKey(configurationElement));

            return true;
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        ///   </returns>
        bool ICollection<TElement>.IsReadOnly
        {
            get { return IsReadOnly(); }
        }

        #endregion

        #region Implementation of IList<TElement>

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="configurationElement">The configuration element.</param>
        /// <returns></returns>
        public int IndexOf(TElement configurationElement)
        {
            return BaseIndexOf(configurationElement);
        }

        /// <summary>
        /// Inserts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="configurationElement">The configuration element.</param>
        public void Insert(int index, TElement configurationElement)
        {
            BaseAdd(index, configurationElement);
        }

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        ///   </exception>
        ///   
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        ///   </exception>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Gets or sets a property, attribute, or child element of this configuration element.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public TElement this[int index]
        {
            get
            {
                return (TElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        #endregion
    }
}
