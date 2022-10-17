using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins.Bindings
{
    public static partial class Binder
    {
        /// <summary>
        /// One-way Collection
        /// </summary>
        public static void Bind<TBindingSource, TItem>
        (
            this TBindingSource source,
            GameObject gameObject,
            Action<CollectionChangedArgs<TItem>> update
        )
            where TBindingSource : INotifyCollectionChanged, IEnumerable<TItem>
        {
            void CollectionChanged(object _, NotifyCollectionChangedEventArgs e) => update(new CollectionChangedArgs<TItem>(e));

            source.CollectionChanged += CollectionChanged;
            gameObject.GetOrAddComponent<DestroyDetector>().Destroyed += () => source.CollectionChanged -= CollectionChanged;
        }
    }
}