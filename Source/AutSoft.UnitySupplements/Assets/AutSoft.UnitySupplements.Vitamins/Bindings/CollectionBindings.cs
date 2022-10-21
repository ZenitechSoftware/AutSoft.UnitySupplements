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
            this MonoBehaviour lifetimeOwner,
            TBindingSource source,
            Action<CollectionChangedArgs<TItem>> update
        )
            where TBindingSource : INotifyCollectionChanged, IEnumerable<TItem> =>
            lifetimeOwner.gameObject.Bind(source, update);

        /// <summary>
        /// One-way Collection
        /// </summary>
        public static void Bind<TBindingSource, TItem>
        (
            this GameObject lifetimeOwner,
            TBindingSource source,
            Action<CollectionChangedArgs<TItem>> update
        )
            where TBindingSource : INotifyCollectionChanged, IEnumerable<TItem>
        {
            void CollectionChanged(object _, NotifyCollectionChangedEventArgs e) => update(new CollectionChangedArgs<TItem>(e));

            source.CollectionChanged += CollectionChanged;
            lifetimeOwner.GetOrAddComponent<DestroyDetector>().Destroyed += () => source.CollectionChanged -= CollectionChanged;
        }
    }
}
