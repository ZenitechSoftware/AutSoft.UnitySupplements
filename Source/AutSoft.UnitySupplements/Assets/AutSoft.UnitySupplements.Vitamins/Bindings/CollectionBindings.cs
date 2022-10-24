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
        /// <typeparam name="TBindingSource">Type of the class we are bindig to</typeparam>
        /// <typeparam name="TItem">The collection's type</typeparam>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="source">The binding source</param>
        /// <param name="sourceToTarget"></param>
        public static BindingLifetime Bind<TBindingSource, TItem>
        (
            this MonoBehaviour lifetimeOwner,
            TBindingSource source,
            Action<CollectionChangedArgs<TItem>> sourceToTarget
        )
            where TBindingSource : INotifyCollectionChanged, IEnumerable<TItem> =>
            lifetimeOwner.gameObject.Bind(source, sourceToTarget);

        /// <summary>
        /// One-way Collection
        /// </summary>
        /// <typeparam name="TBindingSource">Type of the class we are bindig to</typeparam>
        /// <typeparam name="TItem">The collection's type</typeparam>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="source">The binding source</param>
        /// <param name="sourceToTarget"></param>
        public static BindingLifetime Bind<TBindingSource, TItem>
        (
            this GameObject lifetimeOwner,
            TBindingSource source,
            Action<CollectionChangedArgs<TItem>> sourceToTarget
        )
            where TBindingSource : INotifyCollectionChanged, IEnumerable<TItem>
        {
            void CollectionChanged(object _, NotifyCollectionChangedEventArgs e) => sourceToTarget(new CollectionChangedArgs<TItem>(e));

            source.CollectionChanged += CollectionChanged;

            void Unsubscribe() => source.CollectionChanged -= CollectionChanged;

            lifetimeOwner.GetOrAddComponent<DestroyDetector>().Destroyed += Unsubscribe;
            var lifetime = new BindingLifetime();
            lifetime.AddUnsubscribe(Unsubscribe);

            return lifetime;
        }
    }
}
